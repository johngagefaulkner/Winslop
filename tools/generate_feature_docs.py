#!/usr/bin/env python3
import argparse, json, re
from pathlib import Path

GEN_START = "<!-- generated:features:start -->"
GEN_END = "<!-- generated:features:end -->"
MANUAL_START_RE = re.compile(r"<!--\s*manual:start\s+([^\s]+)\s*-->")
MANUAL_END_TEMPLATE = "<!-- manual:end {anchor} -->"


def slug(value: str) -> str:
    value = value.strip().lower()
    out = []
    last_dash = False
    for ch in value:
        if ch in "'’´":
            continue
        if ch.isalnum():
            out.append(ch)
            last_dash = False
        elif not last_dash:
            out.append('-')
            last_dash = True
    return ''.join(out).strip('-')


def read_manual_blocks(doc_text: str):
    blocks = {}
    lines = doc_text.splitlines()
    i = 0
    while i < len(lines):
        m = MANUAL_START_RE.match(lines[i].strip())
        if not m:
            i += 1
            continue
        anchor = m.group(1)
        end_marker = MANUAL_END_TEMPLATE.format(anchor=anchor)
        j = i + 1
        body = []
        while j < len(lines) and lines[j].strip() != end_marker:
            body.append(lines[j])
            j += 1
        blocks[anchor] = '\n'.join(body).rstrip('\n')
        i = j + 1
    return blocks


def extract_existing_feature_bodies(doc_text: str):
    before_plugins = doc_text.split('\n## Plugins', 1)[0]
    sections = {}
    current_section = None
    current_feature = None
    feature_lines = []

    def flush_feature():
        nonlocal current_feature, feature_lines
        if current_feature and current_section:
            sec = sections.setdefault(current_section, {})
            sec[current_feature] = '\n'.join(feature_lines).strip('\n')
        current_feature = None
        feature_lines = []

    for line in before_plugins.splitlines():
        if line.startswith('## '):
            flush_feature()
            title = line[3:].strip()
            if title in {'Table of contents'}:
                current_section = None
            else:
                current_section = title
        elif line.startswith('### '):
            flush_feature()
            current_feature = line[4:].split('<a')[0].strip()
        elif current_feature:
            feature_lines.append(line)

    flush_feature()
    return sections


def render_generated(catalog, manual_blocks, old_bodies):
    lines = [GEN_START, '']
    for section in catalog['sections']:
        lines.append(f"## {section['title']}")
        lines.append('')
        for feature in section['features']:
            anchor = slug(feature['id'])
            lines.append(f"### {feature['displayName']} <a id=\"{anchor}\"></a>")
            lines.append(f"<!-- manual:start {anchor} -->")
            body = manual_blocks.get(anchor)
            if body is None:
                body = old_bodies.get(section['title'], {}).get(feature['displayName'], '').strip('\n')
            if body:
                lines.append(body)
            lines.append(MANUAL_END_TEMPLATE.format(anchor=anchor))
            lines.append('')
        lines.append('---')
        lines.append('')
    # trim trailing separator
    while lines and lines[-1] == '':
        lines.pop()
    if lines and lines[-1] == '---':
        lines.pop()
    lines.append('')
    lines.append(GEN_END)
    return '\n'.join(lines) + '\n'


def main():
    p = argparse.ArgumentParser()
    p.add_argument('--catalog', default='feature-catalog.json')
    p.add_argument('--docs', default='docs/features.md')
    args = p.parse_args()

    doc_path = Path(args.docs)
    catalog = json.loads(Path(args.catalog).read_text())
    doc_text = doc_path.read_text()

    manual_blocks = read_manual_blocks(doc_text)
    old_bodies = extract_existing_feature_bodies(doc_text)
    generated = render_generated(catalog, manual_blocks, old_bodies)

    if GEN_START in doc_text and GEN_END in doc_text:
        prefix, rest = doc_text.split(GEN_START, 1)
        _, suffix = rest.split(GEN_END, 1)
        new_doc = prefix.rstrip() + '\n\n' + generated + suffix.lstrip('\n')
    else:
        plugins_idx = doc_text.find('\n## Plugins')
        first_section_idx = doc_text.find('\n## Issues & Maintenance')
        if plugins_idx == -1:
            raise SystemExit('Could not find "## Plugins" section in docs/features.md')
        if first_section_idx == -1:
            raise SystemExit('Could not find "## Issues & Maintenance" section in docs/features.md')
        prefix = doc_text[:first_section_idx].rstrip() + '\n\n'
        suffix = doc_text[plugins_idx:].lstrip('\n')
        new_doc = prefix + generated + '\n' + suffix

    doc_path.write_text(new_doc)


if __name__ == '__main__':
    main()
