using System.Threading.Tasks;

namespace Winslop
{
    public sealed class CodeFeatureProvider : IFeatureProvider
    {
        private readonly FeatureBase feature;

        public CodeFeatureProvider(FeatureBase feature)
        {
            this.feature = feature;
        }

        public string ID() => feature.ID();
        public string GetFeatureDetails() => feature.GetFeatureDetails();
        public Task<bool> CheckFeature() => feature.CheckFeature();
        public Task<bool> DoFeature() => feature.DoFeature();
        public bool UndoFeature() => feature.UndoFeature();
        public bool IsApplicable() => feature.IsApplicable();
        public string InapplicableReason() => feature.InapplicableReason();
        public string HelpAnchorId() => feature.HelpAnchorId();
    }
}
