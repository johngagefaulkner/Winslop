using System.Runtime.Serialization;

namespace Winslop.Features.Catalog
{
    [DataContract]
    public class RegistryRule
    {
        [DataMember(Name = "keyPath")]
        public string KeyPath { get; set; }

        [DataMember(Name = "valueName")]
        public string ValueName { get; set; }

        [DataMember(Name = "assertionType")]
        public string AssertionType { get; set; }

        [DataMember(Name = "expectedInt")]
        public int? ExpectedInt { get; set; }

        [DataMember(Name = "expectedString")]
        public string ExpectedString { get; set; }

        [DataMember(Name = "shouldExist")]
        public bool? ShouldExist { get; set; }
    }
}
