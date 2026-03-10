using System.Threading.Tasks;

namespace Winslop
{
    public interface IFeatureProvider
    {
        string ID();
        string GetFeatureDetails();
        Task<bool> CheckFeature();
        Task<bool> DoFeature();
        bool UndoFeature();
        bool IsApplicable();
        string InapplicableReason();
        string HelpAnchorId();
    }
}
