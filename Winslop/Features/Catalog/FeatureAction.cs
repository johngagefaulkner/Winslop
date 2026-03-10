using System.Runtime.Serialization;

namespace Winslop.Features.Catalog
{
    [DataContract]
    public class FeatureAction
    {
        [DataMember(Name = "actionType")]
        public string ActionType { get; set; }

        [DataMember(Name = "keyPath")]
        public string KeyPath { get; set; }

        [DataMember(Name = "valueName")]
        public string ValueName { get; set; }

        [DataMember(Name = "valueKind")]
        public string ValueKind { get; set; }

        [DataMember(Name = "intValue")]
        public int? IntValue { get; set; }

        [DataMember(Name = "stringValue")]
        public string StringValue { get; set; }

        [DataMember(Name = "restartExplorer")]
        public bool RestartExplorer { get; set; }
    }
}
