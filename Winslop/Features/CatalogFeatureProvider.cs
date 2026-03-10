using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Winslop
{
    public sealed class CatalogFeatureProvider : IFeatureProvider
    {
        private readonly CatalogFeatureItem definition;

        public CatalogFeatureProvider(CatalogFeatureItem definition)
        {
            this.definition = definition;
        }

        public string ID() => definition.Id;

        public string GetFeatureDetails() => definition.Details;

        public Task<bool> CheckFeature()
        {
            if (definition.RegistryOperations == null || definition.RegistryOperations.Count == 0)
                return Task.FromResult(false);

            foreach (var op in definition.RegistryOperations)
            {
                if (!RegistryValueMatches(op, op.RecommendedValue))
                    return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> DoFeature()
        {
            return Task.FromResult(ApplyRegistryValues(useRecommendedValues: true));
        }

        public bool UndoFeature()
        {
            return ApplyRegistryValues(useRecommendedValues: false);
        }

        public bool IsApplicable()
        {
            if (string.IsNullOrWhiteSpace(definition.Applicability) || definition.Applicability.Equals("any", StringComparison.OrdinalIgnoreCase))
                return true;

            if (definition.Applicability.Equals("windows11", StringComparison.OrdinalIgnoreCase))
                return WindowsVersion.IsWindows11OrLater();

            if (definition.Applicability.Equals("windows10", StringComparison.OrdinalIgnoreCase))
                return WindowsVersion.IsWindows10();

            return true;
        }

        public string InapplicableReason() => definition.InapplicableReason;

        public string HelpAnchorId() => string.IsNullOrWhiteSpace(definition.HelpAnchorId) ? ID() : definition.HelpAnchorId;

        private bool ApplyRegistryValues(bool useRecommendedValues)
        {
            try
            {
                if (definition.RegistryOperations == null || definition.RegistryOperations.Count == 0)
                    return false;

                foreach (var op in definition.RegistryOperations)
                {
                    object value = ParseValue(op, useRecommendedValues ? op.RecommendedValue : op.UndoValue);
                    Registry.SetValue(op.KeyName, op.ValueName, value, ParseKind(op.Kind));
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Log("Code red in " + ex.Message, LogLevel.Error);
                return false;
            }
        }

        private static bool RegistryValueMatches(CatalogRegistryOperation op, string expected)
        {
            object expectedValue = ParseValue(op, expected);
            object current = Registry.GetValue(op.KeyName, op.ValueName, null);

            if (current == null)
                return false;

            if (expectedValue is int intValue)
                return current is int currentInt && currentInt == intValue;

            return string.Equals(current.ToString(), expectedValue.ToString(), StringComparison.Ordinal);
        }

        private static object ParseValue(CatalogRegistryOperation op, string value)
        {
            if (string.Equals(op.Kind, "dword", StringComparison.OrdinalIgnoreCase))
                return int.Parse(value);

            return value;
        }

        private static RegistryValueKind ParseKind(string kind)
        {
            if (string.Equals(kind, "dword", StringComparison.OrdinalIgnoreCase))
                return RegistryValueKind.DWord;

            return RegistryValueKind.String;
        }

        public static List<CatalogFeatureItem> LoadCatalog(string path)
        {
            if (!File.Exists(path))
                return new List<CatalogFeatureItem>();

            using (var stream = File.OpenRead(path))
            {
                var serializer = new DataContractJsonSerializer(typeof(CatalogFeatureCollection));
                var root = serializer.ReadObject(stream) as CatalogFeatureCollection;
                return root?.Features ?? new List<CatalogFeatureItem>();
            }
        }
    }

    [DataContract]
    public class CatalogFeatureCollection
    {
        [DataMember(Name = "features")]
        public List<CatalogFeatureItem> Features { get; set; }
    }

    [DataContract]
    public class CatalogFeatureItem
    {
        [DataMember(Name = "category")]
        public string Category { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "details")]
        public string Details { get; set; }

        [DataMember(Name = "helpAnchorId")]
        public string HelpAnchorId { get; set; }

        [DataMember(Name = "applicability")]
        public string Applicability { get; set; }

        [DataMember(Name = "inapplicableReason")]
        public string InapplicableReason { get; set; }

        [DataMember(Name = "registryOperations")]
        public List<CatalogRegistryOperation> RegistryOperations { get; set; }

        [DataMember(Name = "defaultChecked")]
        public bool DefaultChecked { get; set; } = true;
    }

    [DataContract]
    public class CatalogRegistryOperation
    {
        [DataMember(Name = "keyName")]
        public string KeyName { get; set; }

        [DataMember(Name = "valueName")]
        public string ValueName { get; set; }

        [DataMember(Name = "kind")]
        public string Kind { get; set; }

        [DataMember(Name = "recommendedValue")]
        public string RecommendedValue { get; set; }

        [DataMember(Name = "undoValue")]
        public string UndoValue { get; set; }
    }
}
