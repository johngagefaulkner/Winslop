using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Winslop.Features.Catalog
{
    [DataContract]
    public class FeatureDefinition
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }

        [DataMember(Name = "category")]
        public string Category { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "applicability")]
        public string Applicability { get; set; }

        [DataMember(Name = "checkRules")]
        public List<RegistryRule> CheckRules { get; set; }

        [DataMember(Name = "applyActions")]
        public List<FeatureAction> ApplyActions { get; set; }

        [DataMember(Name = "undoActions")]
        public List<FeatureAction> UndoActions { get; set; }

        [DataMember(Name = "supportsUndo")]
        public bool SupportsUndo { get; set; }
    }
}
