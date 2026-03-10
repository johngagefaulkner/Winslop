using Settings.Ads;
using Settings.AI;
using Settings.Edge;
using Settings.Gaming;
using Settings.Issues;
using Settings.Personalization;
using Settings.Privacy;
using Settings.System;
using Settings.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Winslop;

namespace Features
{
    public static class FeatureLoader
    {
        public static List<FeatureNode> Load()
        {
            string catalogPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "features.catalog.json");
            var catalog = CatalogFeatureProvider.LoadCatalog(catalogPath);

            var systemChildren = NodesForCategory(catalog, "System");
            systemChildren.Add(new FeatureNode(new DisableHibernation()));

            return new List<FeatureNode>
            {
               new FeatureNode("Issues")
                {
                    Children =
                    {
                        new FeatureNode(new BasicCleanup()),
                    }
                },

              new FeatureNode("System")
                {
                    Children = systemChildren
                },

                new FeatureNode("MS Edge")
                {
                    Children =
                    {
                       new FeatureNode(new BrowserSignin()),
                       new FeatureNode(new DefaultTopSites()),
                       new FeatureNode(new DefautBrowserSetting()),
                       new FeatureNode(new EdgeCollections()),
                       new FeatureNode(new EdgeShoppingAssistant()),
                       new FeatureNode(new FirstRunExperience()),
                       new FeatureNode(new GamerMode()),
                       new FeatureNode(new HubsSidebar()),
                       new FeatureNode(new ImportOnEachLaunch()),
                       new FeatureNode(new StartupBoost()),
                       new FeatureNode(new TabPageQuickLinks()),
                       new FeatureNode(new UserFeedback()),
                    }
                },

               new FeatureNode("UI")
                {
                    Children =
                    {
                       new FeatureNode(new FullContextMenus()),
                       new FeatureNode(new LockScreen()),
                       new FeatureNode(new ShowOrHideMostUsedApps()),
                       new FeatureNode(new DisableBingSearch()),
                       new FeatureNode(new StartLayout()),
                       new FeatureNode(new Transparency()),
                       new FeatureNode(new AppDarkMode()) { DefaultChecked = false },
                       new FeatureNode(new SystemDarkMode()) { DefaultChecked = false },
                       new FeatureNode(new DisableSnapAssistFlyout()),
                    }
                },

               new FeatureNode("Taskbar")
                {
                    Children =
                    {
                       new FeatureNode(new AlwaysShowTrayIcons()),
                       new FeatureNode(new RemoveMeetNowButton()),
                       new FeatureNode(new DisableNewsAndInterests()),
                       new FeatureNode(new DisableWidgets()),
                       new FeatureNode(new TaskbarEndTask()),
                       new FeatureNode(new TaskbarSmallIcons()),
                       new FeatureNode(new SearchboxTaskbarMode()),
                       new FeatureNode(new ShowTaskViewButton()),
                       new FeatureNode(new TaskbarAlignment()),
                       new FeatureNode(new CleanTaskbar()) { DefaultChecked = false },
                    }
                },

               new FeatureNode("Gaming")
                {
                    Children =
                    {
                       new FeatureNode(new GameDVR()),
                       new FeatureNode(new PowerThrottling()),
                       new FeatureNode(new VisualFX()),
                    }
                },

                new FeatureNode("Privacy")
                {
                    Children =
                    {
                       new FeatureNode(new ActivityHistory()),
                       new FeatureNode(new LocationTracking()),
                       new FeatureNode(new PrivacyExperience()),
                       new FeatureNode(new DiagnosticData()),
                       new FeatureNode(new SilentAppInstallation()),
                       new FeatureNode(new WindowsSpotlightLockScreen()),
                       new FeatureNode(new LockScreenSlideshow()),
                       new FeatureNode(new AppLaunchTracking()),
                       new FeatureNode(new OnlineSpeechRecognition()),
                       new FeatureNode(new NarratorOnlineServices()),

                    }
                },

                new FeatureNode("Ads")
                {
                    Children =
                    {
                        new FeatureNode(new FileExplorerAds()),
                        new FeatureNode(new FinishSetupAds()),
                        new FeatureNode(new LockScreenAds()),
                        new FeatureNode(new PersonalizedAds()),
                        new FeatureNode(new SettingsAds()),
                        new FeatureNode(new StartmenuAds()),
                        new FeatureNode(new TailoredExperiences()),
                        new FeatureNode(new TipsAndSuggestions()),
                        new FeatureNode(new WelcomeExperienceAds()),
                    }
                },

                new FeatureNode("AI")
                {
                    Children =
                    {
                       new FeatureNode(new CopilotTaskbar()),
                       new FeatureNode(new Recall()),
                       new FeatureNode(new ClickToDo()) { DefaultChecked = false },
                       new FeatureNode(new DisableSearchBoxSuggestions()),
                    }
                },
            };
        }

        private static List<FeatureNode> NodesForCategory(List<CatalogFeatureItem> catalog, string category)
        {
            return catalog
                .Where(item => item.Category == category)
                .Select(item => new FeatureNode(new CatalogFeatureProvider(item))
                {
                    DefaultChecked = item.DefaultChecked,
                })
                .ToList();
        }
    }
}
