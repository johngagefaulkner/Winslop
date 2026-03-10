using Settings.Ads;
using Settings.AI;
using Settings.Edge;
using Settings.Gaming;
using Settings.Issues;
using Settings.Personalization;
using Settings.Privacy;
using Settings.System;
using Settings.UI;
using System;
using System.Collections.Generic;
using Winslop;

namespace Features
{
    public static class FeatureLoader
    {
        private sealed class FeatureCatalogEntry
        {
            public string Category { get; set; }
            public Func<FeatureBase> Factory { get; set; }
            public bool DefaultChecked { get; set; } = true;
        }

        private sealed class CodeFeatureRegistration
        {
            public string Category { get; set; }
            public Func<FeatureBase> Factory { get; set; }
            public bool? DefaultCheckedOverride { get; set; }
        }

        public static List<FeatureNode> Load()
        {
            var categories = new List<FeatureNode>();
            var categoryLookup = new Dictionary<string, FeatureNode>(StringComparer.OrdinalIgnoreCase);

            void AddFeature(string categoryName, FeatureNode featureNode)
            {
                if (!categoryLookup.TryGetValue(categoryName, out var categoryNode))
                {
                    categoryNode = new FeatureNode(categoryName);
                    categoryLookup[categoryName] = categoryNode;
                    categories.Add(categoryNode);
                }

                categoryNode.Children.Add(featureNode);
            }

            // Explicit code-backed registry for features that are not fully declarative.
            foreach (var registration in GetCodeFeatureRegistry())
            {
                var node = new FeatureNode(registration.Factory());
                if (registration.DefaultCheckedOverride.HasValue)
                    node.DefaultChecked = registration.DefaultCheckedOverride.Value;

                AddFeature(registration.Category, node);
            }

            foreach (var entry in GetFeatureCatalog())
            {
                AddFeature(entry.Category, new FeatureNode(entry.Factory())
                {
                    DefaultChecked = entry.DefaultChecked,
                });
            }

            return categories;
        }

        private static IEnumerable<CodeFeatureRegistration> GetCodeFeatureRegistry()
        {
            // Keep this list small and explicit. Use only when a feature cannot be described by catalog metadata alone.
            yield return new CodeFeatureRegistration
            {
                Category = "Issues",
                Factory = () => new BasicCleanup(),
            };
        }

        private static IEnumerable<FeatureCatalogEntry> GetFeatureCatalog()
        {
            yield return new FeatureCatalogEntry { Category = "System", Factory = () => new BSODDetails() };
            yield return new FeatureCatalogEntry { Category = "System", Factory = () => new VerboseStatus() };
            yield return new FeatureCatalogEntry { Category = "System", Factory = () => new SpeedUpShutdown() };
            yield return new FeatureCatalogEntry { Category = "System", Factory = () => new NetworkThrottling() };
            yield return new FeatureCatalogEntry { Category = "System", Factory = () => new SystemResponsiveness() };
            yield return new FeatureCatalogEntry { Category = "System", Factory = () => new MenuShowDelay() };
            yield return new FeatureCatalogEntry { Category = "System", Factory = () => new DisableHibernation() };

            yield return new FeatureCatalogEntry { Category = "MS Edge", Factory = () => new BrowserSignin() };
            yield return new FeatureCatalogEntry { Category = "MS Edge", Factory = () => new DefaultTopSites() };
            yield return new FeatureCatalogEntry { Category = "MS Edge", Factory = () => new DefautBrowserSetting() };
            yield return new FeatureCatalogEntry { Category = "MS Edge", Factory = () => new EdgeCollections() };
            yield return new FeatureCatalogEntry { Category = "MS Edge", Factory = () => new EdgeShoppingAssistant() };
            yield return new FeatureCatalogEntry { Category = "MS Edge", Factory = () => new FirstRunExperience() };
            yield return new FeatureCatalogEntry { Category = "MS Edge", Factory = () => new GamerMode() };
            yield return new FeatureCatalogEntry { Category = "MS Edge", Factory = () => new HubsSidebar() };
            yield return new FeatureCatalogEntry { Category = "MS Edge", Factory = () => new ImportOnEachLaunch() };
            yield return new FeatureCatalogEntry { Category = "MS Edge", Factory = () => new StartupBoost() };
            yield return new FeatureCatalogEntry { Category = "MS Edge", Factory = () => new TabPageQuickLinks() };
            yield return new FeatureCatalogEntry { Category = "MS Edge", Factory = () => new UserFeedback() };

            yield return new FeatureCatalogEntry { Category = "UI", Factory = () => new FullContextMenus() };
            yield return new FeatureCatalogEntry { Category = "UI", Factory = () => new LockScreen() };
            yield return new FeatureCatalogEntry { Category = "UI", Factory = () => new ShowOrHideMostUsedApps() };
            yield return new FeatureCatalogEntry { Category = "UI", Factory = () => new DisableBingSearch() };
            yield return new FeatureCatalogEntry { Category = "UI", Factory = () => new StartLayout() };
            yield return new FeatureCatalogEntry { Category = "UI", Factory = () => new Transparency() };
            yield return new FeatureCatalogEntry { Category = "UI", Factory = () => new AppDarkMode(), DefaultChecked = false };
            yield return new FeatureCatalogEntry { Category = "UI", Factory = () => new SystemDarkMode(), DefaultChecked = false };
            yield return new FeatureCatalogEntry { Category = "UI", Factory = () => new DisableSnapAssistFlyout() };

            yield return new FeatureCatalogEntry { Category = "Taskbar", Factory = () => new AlwaysShowTrayIcons() };
            yield return new FeatureCatalogEntry { Category = "Taskbar", Factory = () => new RemoveMeetNowButton() };
            yield return new FeatureCatalogEntry { Category = "Taskbar", Factory = () => new DisableNewsAndInterests() };
            yield return new FeatureCatalogEntry { Category = "Taskbar", Factory = () => new DisableWidgets() };
            yield return new FeatureCatalogEntry { Category = "Taskbar", Factory = () => new TaskbarEndTask() };
            yield return new FeatureCatalogEntry { Category = "Taskbar", Factory = () => new TaskbarSmallIcons() };
            yield return new FeatureCatalogEntry { Category = "Taskbar", Factory = () => new SearchboxTaskbarMode() };
            yield return new FeatureCatalogEntry { Category = "Taskbar", Factory = () => new ShowTaskViewButton() };
            yield return new FeatureCatalogEntry { Category = "Taskbar", Factory = () => new TaskbarAlignment() };
            yield return new FeatureCatalogEntry { Category = "Taskbar", Factory = () => new CleanTaskbar(), DefaultChecked = false };

            yield return new FeatureCatalogEntry { Category = "Gaming", Factory = () => new GameDVR() };
            yield return new FeatureCatalogEntry { Category = "Gaming", Factory = () => new PowerThrottling() };
            yield return new FeatureCatalogEntry { Category = "Gaming", Factory = () => new VisualFX() };

            yield return new FeatureCatalogEntry { Category = "Privacy", Factory = () => new ActivityHistory() };
            yield return new FeatureCatalogEntry { Category = "Privacy", Factory = () => new LocationTracking() };
            yield return new FeatureCatalogEntry { Category = "Privacy", Factory = () => new PrivacyExperience() };
            yield return new FeatureCatalogEntry { Category = "Privacy", Factory = () => new DiagnosticData() };
            yield return new FeatureCatalogEntry { Category = "Privacy", Factory = () => new SilentAppInstallation() };
            yield return new FeatureCatalogEntry { Category = "Privacy", Factory = () => new WindowsSpotlightLockScreen() };
            yield return new FeatureCatalogEntry { Category = "Privacy", Factory = () => new LockScreenSlideshow() };
            yield return new FeatureCatalogEntry { Category = "Privacy", Factory = () => new AppLaunchTracking() };
            yield return new FeatureCatalogEntry { Category = "Privacy", Factory = () => new OnlineSpeechRecognition() };
            yield return new FeatureCatalogEntry { Category = "Privacy", Factory = () => new NarratorOnlineServices() };

            yield return new FeatureCatalogEntry { Category = "Ads", Factory = () => new FileExplorerAds() };
            yield return new FeatureCatalogEntry { Category = "Ads", Factory = () => new FinishSetupAds() };
            yield return new FeatureCatalogEntry { Category = "Ads", Factory = () => new LockScreenAds() };
            yield return new FeatureCatalogEntry { Category = "Ads", Factory = () => new PersonalizedAds() };
            yield return new FeatureCatalogEntry { Category = "Ads", Factory = () => new SettingsAds() };
            yield return new FeatureCatalogEntry { Category = "Ads", Factory = () => new StartmenuAds() };
            yield return new FeatureCatalogEntry { Category = "Ads", Factory = () => new TailoredExperiences() };
            yield return new FeatureCatalogEntry { Category = "Ads", Factory = () => new TipsAndSuggestions() };
            yield return new FeatureCatalogEntry { Category = "Ads", Factory = () => new WelcomeExperienceAds() };

            yield return new FeatureCatalogEntry { Category = "AI", Factory = () => new CopilotTaskbar() };
            yield return new FeatureCatalogEntry { Category = "AI", Factory = () => new Recall() };
            yield return new FeatureCatalogEntry { Category = "AI", Factory = () => new ClickToDo(), DefaultChecked = false };
            yield return new FeatureCatalogEntry { Category = "AI", Factory = () => new DisableSearchBoxSuggestions() };
        }
    }
}
