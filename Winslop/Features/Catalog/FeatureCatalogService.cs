using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Winslop.Features.Catalog
{
    public static class FeatureCatalogService
    {
        private static readonly HashSet<string> AllowedApplicability = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "any", "win10", "win11+"
        };

        private static readonly HashSet<string> AllowedAssertionTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "int", "string", "exists"
        };

        private static readonly HashSet<string> AllowedActionTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "write", "delete"
        };

        private static readonly HashSet<string> AllowedValueKinds = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "int", "string"
        };

        public static IReadOnlyList<FeatureDefinition> LoadedFeatures { get; private set; } = new List<FeatureDefinition>();

        public static void Initialize()
        {
            string catalogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Features", "feature-catalog.json");
            Logger.Log($"Loading feature catalog from '{catalogPath}'...");

            if (!File.Exists(catalogPath))
            {
                Logger.Log("Feature catalog file not found. Skipping catalog initialization.", LogLevel.Warning);
                return;
            }

            FeatureCatalogDocument document;
            try
            {
                using (var stream = File.OpenRead(catalogPath))
                {
                    var serializer = new DataContractJsonSerializer(typeof(FeatureCatalogDocument));
                    document = serializer.ReadObject(stream) as FeatureCatalogDocument;
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to parse feature catalog JSON: {ex.Message}", LogLevel.Error);
                return;
            }

            var features = document?.Features ?? new List<FeatureDefinition>();
            var valid = new List<FeatureDefinition>();
            var ids = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < features.Count; i++)
            {
                FeatureDefinition feature = features[i];
                string label = $"entry #{i + 1}";
                var errors = ValidateFeature(feature, ids);

                if (feature != null && !string.IsNullOrWhiteSpace(feature.Id))
                {
                    label = $"'{feature.Id}'";
                }

                if (errors.Count == 0)
                {
                    valid.Add(feature);
                    Logger.Log($"Catalog feature {label} loaded.");
                }
                else
                {
                    Logger.Log($"Catalog feature {label} skipped: {string.Join("; ", errors)}", LogLevel.Warning);
                }
            }

            LoadedFeatures = valid;
            Logger.Log($"Feature catalog initialization complete. Valid entries: {valid.Count}/{features.Count}.");
        }

        private static List<string> ValidateFeature(FeatureDefinition feature, HashSet<string> ids)
        {
            var errors = new List<string>();
            if (feature == null)
            {
                errors.Add("feature object is null");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(feature.Id))
            {
                errors.Add("missing id");
            }
            else if (!ids.Add(feature.Id))
            {
                errors.Add("duplicate id");
            }

            if (string.IsNullOrWhiteSpace(feature.DisplayName)) errors.Add("missing displayName");
            if (string.IsNullOrWhiteSpace(feature.Category)) errors.Add("missing category");
            if (string.IsNullOrWhiteSpace(feature.Description)) errors.Add("missing description");

            if (string.IsNullOrWhiteSpace(feature.Applicability) || !AllowedApplicability.Contains(feature.Applicability))
            {
                errors.Add("invalid applicability");
            }

            if (feature.CheckRules == null || feature.CheckRules.Count == 0)
            {
                errors.Add("checkRules must contain at least one rule");
            }
            else
            {
                for (int i = 0; i < feature.CheckRules.Count; i++)
                {
                    string checkError = ValidateRule(feature.CheckRules[i]);
                    if (checkError != null)
                    {
                        errors.Add($"checkRules[{i}] {checkError}");
                    }
                }
            }

            if (feature.ApplyActions == null || feature.ApplyActions.Count == 0)
            {
                errors.Add("applyActions must contain at least one action");
            }
            else
            {
                for (int i = 0; i < feature.ApplyActions.Count; i++)
                {
                    string actionError = ValidateAction(feature.ApplyActions[i]);
                    if (actionError != null)
                    {
                        errors.Add($"applyActions[{i}] {actionError}");
                    }
                }
            }

            if (feature.SupportsUndo)
            {
                if (feature.UndoActions == null || feature.UndoActions.Count == 0)
                {
                    errors.Add("supportsUndo=true requires undoActions");
                }
                else
                {
                    for (int i = 0; i < feature.UndoActions.Count; i++)
                    {
                        string actionError = ValidateAction(feature.UndoActions[i]);
                        if (actionError != null)
                        {
                            errors.Add($"undoActions[{i}] {actionError}");
                        }
                    }
                }
            }

            return errors;
        }

        private static string ValidateRule(RegistryRule rule)
        {
            if (rule == null) return "is null";
            if (string.IsNullOrWhiteSpace(rule.KeyPath)) return "is missing keyPath";

            if (string.IsNullOrWhiteSpace(rule.AssertionType) || !AllowedAssertionTypes.Contains(rule.AssertionType))
            {
                return "has invalid assertionType";
            }

            if (rule.AssertionType.Equals("exists", StringComparison.OrdinalIgnoreCase))
            {
                if (!rule.ShouldExist.HasValue)
                {
                    return "assertionType=exists requires shouldExist";
                }

                return null;
            }

            if (string.IsNullOrWhiteSpace(rule.ValueName))
            {
                return "is missing valueName";
            }

            if (rule.AssertionType.Equals("int", StringComparison.OrdinalIgnoreCase) && !rule.ExpectedInt.HasValue)
            {
                return "assertionType=int requires expectedInt";
            }

            if (rule.AssertionType.Equals("string", StringComparison.OrdinalIgnoreCase) && rule.ExpectedString == null)
            {
                return "assertionType=string requires expectedString";
            }

            return null;
        }

        private static string ValidateAction(FeatureAction action)
        {
            if (action == null) return "is null";
            if (string.IsNullOrWhiteSpace(action.ActionType) || !AllowedActionTypes.Contains(action.ActionType))
            {
                return "has invalid actionType";
            }

            if (string.IsNullOrWhiteSpace(action.KeyPath)) return "is missing keyPath";

            if (action.ActionType.Equals("delete", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(action.ValueName))
                {
                    return "actionType=delete requires valueName";
                }

                return null;
            }

            if (string.IsNullOrWhiteSpace(action.ValueName)) return "actionType=write requires valueName";

            if (string.IsNullOrWhiteSpace(action.ValueKind) || !AllowedValueKinds.Contains(action.ValueKind))
            {
                return "actionType=write requires valueKind of int or string";
            }

            if (action.ValueKind.Equals("int", StringComparison.OrdinalIgnoreCase) && !action.IntValue.HasValue)
            {
                return "valueKind=int requires intValue";
            }

            if (action.ValueKind.Equals("string", StringComparison.OrdinalIgnoreCase) && action.StringValue == null)
            {
                return "valueKind=string requires stringValue";
            }

            return null;
        }
    }
}
