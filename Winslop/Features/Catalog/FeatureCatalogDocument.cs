using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Winslop.Features.Catalog
{
    [DataContract]
    public class FeatureCatalogDocument
    {
        [DataMember(Name = "features")]
        public List<FeatureDefinition> Features { get; set; }
    }
}
