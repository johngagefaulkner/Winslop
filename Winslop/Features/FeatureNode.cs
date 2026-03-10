using System.Collections.Generic;
using Winslop;

public class FeatureNode
{
    public string Name { get; set; }
    public bool IsCategory => Provider == null;
    public IFeatureProvider Provider { get; }
    public List<FeatureNode> Children { get; set; } = new List<FeatureNode>();


    // Property to control default checked state
    public bool DefaultChecked { get; set; } = true;

    // Constructor for categories
    public FeatureNode(string name)
    {
        Name = name;
    }

    // Constructor for provider-backed features
    public FeatureNode(IFeatureProvider provider)
    {
        Provider = provider;
        Name = provider.ID();
    }

    // Backward-compatible constructor for existing code feature classes.
    public FeatureNode(FeatureBase feature) : this(new CodeFeatureProvider(feature))
    {
    }
}
