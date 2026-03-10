# Features

## Table of contents
- [Issues & Maintenance](#issues)
- [System](#system)
- [Microsoft Edge](#microsoft-edge)
- [UI & Personalization](#ui--personalization)
- [Taskbar](#taskbar)
- [Gaming](#gaming)
- [Privacy & Telemetry](#privacy--telemetry)
- [Ads & Recommendations](#ads--recommendations)
- [AI (Copilot & Recall)](#ai-copilot--recall)
- [Plugins](#plugins)

> Want to create your own plugin?  
> See: **[How to create plugins](https://github.com/builtbybel/Winslop/blob/main/docs/plugins.md)**.

---

<!-- generated:features:start -->

## Issues & Maintenance

### Basic Disk Cleanup <a id="basic-disk-cleanup"></a>
<!-- manual:start basic-disk-cleanup -->
**Info:** Deletes all temporary files from the user's Temp folder. Then, the built-in Disk Cleanup utility (cleanmgr) is run.  
**Actions:**
- Deletes files in: `%LOCALAPPDATA%\Temp`
- Runs: `cleanmgr.exe /sageset:1`
- Runs: `cleanmgr.exe /sagerun:1` and `cleanmgr.exe /verylowdisk`

**Undo:** Not supported (cleanup cannot be undone)
<!-- manual:end basic-disk-cleanup -->

### Winget App Updates <a id="winget-app-updates"></a>
<!-- manual:start winget-app-updates -->
**Info:** Automatically searches for available app updates using the Windows package manager 'winget' and installs them in a new Windows Terminal window. It runs `winget upgrade --include-unknown` to list all available updates, including manually installed apps, and then `winget upgrade --all --include-unknown` to install them. No manual interaction is required.  
**Commands:**
- Check: `winget upgrade --include-unknown`
- Install: `winget upgrade --all --include-unknown`

**Undo:** Not supported (winget upgrades cannot be undone)
<!-- manual:end winget-app-updates -->

---

## System

### Show BSOD details instead of sad smiley <a id="show-bsod-details-instead-of-sad-smiley"></a>
<!-- manual:start show-bsod-details-instead-of-sad-smiley -->
**Info:** This method displays the full classic BSOD with technical error details instead of the simplified sad face version.  
**Registry:** `HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\CrashControl`  
**Values:** `DisplayParameters = 1`, `DisableEmoticon = 1`  
**Undo:** `DisplayParameters = 0`, `DisableEmoticon = 0`
<!-- manual:end show-bsod-details-instead-of-sad-smiley -->

### Enable Verbose Logon status messages <a id="enable-verbose-logon-status-messages"></a>
<!-- manual:start enable-verbose-logon-status-messages -->
**Info:** This method allows you to see what processes are hanging when shutting down and turning on the machine.  
**Registry:** `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System`  
**Value:** `VerboseStatus`  
**Recommended:** `1`  
**Undo:** `0`
<!-- manual:end enable-verbose-logon-status-messages -->

### Speed Up Shutdown Time <a id="speed-up-shutdown-time"></a>
<!-- manual:start speed-up-shutdown-time -->
**Info:** This feature reduces the WaitToKillServiceTimeout value...  
**Registry:** `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control`  
**Value:** `WaitToKillServiceTimeout`  
**Recommended:** `"1000"`  
**Undo:** `"5000"`
<!-- manual:end speed-up-shutdown-time -->

### Disable Network Throttling <a id="disable-network-throttling"></a>
<!-- manual:start disable-network-throttling -->
**Info:** Disables the Windows network throttling mechanism...  
**Registry:** `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile`  
**Value:** `NetworkThrottlingIndex`  
**Recommended:** `0xFFFFFFFF` (decimal 4294967295)  
**Undo:** `10`
<!-- manual:end disable-network-throttling -->

### Optimize System Responsiveness <a id="optimize-system-responsiveness"></a>
<!-- manual:start optimize-system-responsiveness -->
**Info:** Enhances system responsiveness by prioritizing CPU resources for foreground tasks...  
**Registry:** `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile`  
**Value:** `SystemResponsiveness`  
**Recommended:** `10`  
**Undo:** `20`
<!-- manual:end optimize-system-responsiveness -->

### Speed Up Menu Show Delay <a id="speed-up-menu-show-delay"></a>
<!-- manual:start speed-up-menu-show-delay -->
**Info:** Speeds up the appearance of menus and submenus...  
**Registry:** `HKEY_CURRENT_USER\Control Panel\Desktop`  
**Value:** `MenuShowDelay`  
**Recommended:** `"10"`  
**Undo:** `"400"`
<!-- manual:end speed-up-menu-show-delay -->

### Disable Hibernation <a id="disable-hibernation"></a>
<!-- manual:start disable-hibernation -->
**Info:** Hibernation is mostly useful for laptops... Disabling it frees resources and avoids confusion by hiding the Hibernate option...  
**Registry 1:** `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power` → `HibernateEnabled = 0`  
**Registry 2:** `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FlyoutMenuSettings` → `ShowHibernateOption = 0`  
**Command:** `powercfg /hibernate off`  
**Undo:** `HibernateEnabled = 1`, `ShowHibernateOption = 1`, `powercfg /hibernate on`

---
<!-- manual:end disable-hibernation -->

---

## Microsoft Edge

### Disable Browser sign in and sync services <a id="disable-browser-sign-in-and-sync-services"></a>
<!-- manual:start disable-browser-sign-in-and-sync-services -->
**Info:** Disables Microsoft Edge browser sign-in and sync services via policy.  
**Registry:** `HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Edge`  
**Value:** `BrowserSignin`  
**Recommended:** `0`  
**Undo:** `1`  
<!-- manual:end disable-browser-sign-in-and-sync-services -->

### Don't Show Sponsored links in new tab page <a id="dont-show-sponsored-links-in-new-tab-page"></a>
<!-- manual:start dont-show-sponsored-links-in-new-tab-page -->
**Info:** Hides sponsored/default top sites on the Edge new tab page.  
**Registry:** `HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Edge`  
**Value:** `NewTabPageHideDefaultTopSites`  
**Recommended:** `1`  
**Undo:** `0`  
<!-- manual:end dont-show-sponsored-links-in-new-tab-page -->

### Disable Microsoft Edge as default browser <a id="disable-microsoft-edge-as-default-browser"></a>
<!-- manual:start disable-microsoft-edge-as-default-browser -->
**Info:** Prevents Microsoft Edge from being set as the default browser (policy controlled).  
**Registry:** `HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Edge`  
**Value:** `DefaultBrowserSettingEnabled`  
**Recommended:** `0`  
**Undo:** `1`  
<!-- manual:end disable-microsoft-edge-as-default-browser -->

### Disable Access to Collections feature <a id="disable-access-to-collections-feature"></a>
<!-- manual:start disable-access-to-collections-feature -->
**Info:** Disables the Edge Collections feature.  
**Registry:** `HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Edge`  
**Value:** `EdgeCollectionsEnabled`  
**Recommended:** `0`  
**Undo:** `1`  
<!-- manual:end disable-access-to-collections-feature -->

### Disable Shopping assistant <a id="disable-shopping-assistant"></a>
<!-- manual:start disable-shopping-assistant -->
**Info:** Disables the Edge Shopping Assistant feature.  
**Registry:** `HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Edge`  
**Value:** `EdgeShoppingAssistantEnabled`  
**Recommended:** `0`  
**Undo:** `1`  
<!-- manual:end disable-shopping-assistant -->

### Don't Show First Run Experience <a id="dont-show-first-run-experience"></a>
<!-- manual:start dont-show-first-run-experience -->
**Info:** Hides the Edge first run experience on launch.  
**Registry:** `HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Edge`  
**Value:** `HideFirstRunExperience`  
**Recommended:** `1`  
**Undo:** `0`  
<!-- manual:end dont-show-first-run-experience -->

### Disable Gamer Mode <a id="disable-gamer-mode"></a>
<!-- manual:start disable-gamer-mode -->
**Info:** Disables Edge Gamer Mode.  
**Registry:** `HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Edge`  
**Value:** `GamerModeEnabled`  
**Recommended:** `0`  
**Undo:** `1`  
<!-- manual:end disable-gamer-mode -->

### Disable Copilot Symbol in Edge <a id="disable-copilot-symbol-in-edge"></a>
<!-- manual:start disable-copilot-symbol-in-edge -->
**Info:** Disables the Copilot/Hub sidebar symbol in Microsoft Edge.  
**Registry:** `HKEY_CURRENT_USER\Software\Policies\Microsoft\Edge`  
**Value:** `HubsSidebarEnabled`  
**Recommended:** `0`  
**Undo:** `1`  
<!-- manual:end disable-copilot-symbol-in-edge -->

### Don't import data from other browsers at startup <a id="dont-import-data-from-other-browsers-at-startup"></a>
<!-- manual:start dont-import-data-from-other-browsers-at-startup -->
**Info:** Disables importing of browser data from other browsers on each launch.  
**Registry:** `HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Edge`  
**Value:** `ImportOnEachLaunch`  
**Recommended:** `0`  
**Undo:** `1`  
<!-- manual:end dont-import-data-from-other-browsers-at-startup -->

### Disable Start Boost <a id="disable-start-boost"></a>
<!-- manual:start disable-start-boost -->
**Info:** Disables Microsoft Edge Startup Boost.  
**Registry:** `HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Edge`  
**Value:** `StartupBoostEnabled`  
**Recommended:** `0`  
**Undo:** `1`  
<!-- manual:end disable-start-boost -->

### Don't Show Quick links in new tab page <a id="dont-show-quick-links-in-new-tab-page"></a>
<!-- manual:start dont-show-quick-links-in-new-tab-page -->
**Info:** Disables “Quick links” on the Microsoft Edge new tab page.  
**Registry:** `HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Edge`  
**Value:** `NewTabPageQuickLinksEnabled`  
**Recommended:** `0`  
**Undo:** `1`  
<!-- manual:end dont-show-quick-links-in-new-tab-page -->

### Don't Submit user feedback option <a id="dont-submit-user-feedback-option"></a>
<!-- manual:start dont-submit-user-feedback-option -->
**Info:** Disables the “Submit feedback” option in Microsoft Edge.  
**Registry:** `HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Edge`  
**Value:** `UserFeedbackAllowed`  
**Recommended:** `0`  
**Undo:** `1`  
<!-- manual:end dont-submit-user-feedback-option -->

---

## UI & Personalization

### Show Full context menus in Windows 11 <a id="show-full-context-menus-in-windows-11"></a>
<!-- manual:start show-full-context-menus-in-windows-11 -->
**Info:** This feature will enable full context menus  
**Registry:** `HKEY_CURRENT_USER\SOFTWARE\CLASSES\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32`  
**Value:** `(Default)` set to empty string  
**Undo:** Deletes `Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}`
<!-- manual:end show-full-context-menus-in-windows-11 -->

### Don't use personalized lock screen <a id="dont-use-personalized-lock-screen"></a>
<!-- manual:start dont-use-personalized-lock-screen -->
**Info:** This feature will disable the personalized lock screen.  
**Registry:** `HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization`  
**Value:** `NoLockScreen`  
**DoFeature sets:** `1`  
**Undo:** `0`
<!-- manual:end dont-use-personalized-lock-screen -->

### Hide Most used apps in start menu <a id="hide-most-used-apps-in-start-menu"></a>
<!-- manual:start hide-most-used-apps-in-start-menu -->
**Info:** This feature will hide Most used apps in start menu for all users  
**Registry:** `HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Explorer`  
**Value:** `ShowOrHideMostUsedApps`  
**Recommended:** `2`  
**Undo:** `1`
<!-- manual:end hide-most-used-apps-in-start-menu -->

### Disable Bing Search <a id="disable-bing-search"></a>
<!-- manual:start disable-bing-search -->
**Info:** This feature disables Bing integration in Windows Search.  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search`  
**Value:** `BingSearchEnabled`  
**Recommended:** `0`  
**Undo:** `1`
<!-- manual:end disable-bing-search -->

### Pin more Apps on start menu <a id="pin-more-apps-on-start-menu"></a>
<!-- manual:start pin-more-apps-on-start-menu -->
**Info:** This feature will allow pinning more Apps on start menu  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced`  
**Value:** `Start_Layout`  
**Recommended:** `1`  
**Undo:** `0`
<!-- manual:end pin-more-apps-on-start-menu -->

### Enable Dark Mode for Apps <a id="enable-dark-mode-for-apps"></a>
<!-- manual:start enable-dark-mode-for-apps -->
**Info:** This feature enables Dark Mode for apps in Windows 11.  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize`  
**Value:** `AppsUseLightTheme`  
**Recommended:** `0`  
**Undo:** `1`
<!-- manual:end enable-dark-mode-for-apps -->

### Enable Dark Mode for System <a id="enable-dark-mode-for-system"></a>
<!-- manual:start enable-dark-mode-for-system -->
**Info:** This feature enables Dark Mode for Windows system UI (e.g., taskbar, start menu).  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize`  
**Value:** `SystemUsesLightTheme`  
**Recommended:** `0`  
**Undo:** `1`
<!-- manual:end enable-dark-mode-for-system -->

### Disable Transparency Effects <a id="disable-transparency-effects"></a>
<!-- manual:start disable-transparency-effects -->
**Info:** This feature disables transparency effects for Start menu, taskbar, and other surfaces.  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize`  
**Value:** `EnableTransparency`  
**Recommended:** `0`  
**Undo:** `1`
<!-- manual:end disable-transparency-effects -->

### Disable Snap Assist Flyout <a id="disable-snap-assist-flyout"></a>
<!-- manual:start disable-snap-assist-flyout -->
**Info:** This feature disables the Snap Assist flyout, which appears when you snap a window.  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced`  
**Value:** `EnableSnapAssistFlyout`  
**Recommended:** `0`  
**Undo:** `1`

---
<!-- manual:end disable-snap-assist-flyout -->

---

## Taskbar

### Align Start button to left <a id="align-start-button-to-left"></a>
<!-- manual:start align-start-button-to-left -->
**Supported on:** Windows 11  
**Info:** Aligns the Start button to the left side of the taskbar (Windows 11).  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced`  
**Value:** `TaskbarAl`  
**Recommended:** `0`  
**Undo:** `1`
<!-- manual:end align-start-button-to-left -->

### Hide search box on taskbar <a id="hide-search-box-on-taskbar"></a>
<!-- manual:start hide-search-box-on-taskbar -->
**Supported on:** Windows 11  
**Info:** Hides the search box / search entry on the taskbar.  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search`  
**Value:** `SearchboxTaskbarMode`  
**Recommended:** `0`  
**Undo:** `2`
<!-- manual:end hide-search-box-on-taskbar -->

### Hide Task view button on taskbar <a id="hide-task-view-button-on-taskbar"></a>
<!-- manual:start hide-task-view-button-on-taskbar -->
**Supported on:** Windows 11  
**Info:** Hides the Task View button on the taskbar.  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced`  
**Value:** `ShowTaskViewButton`  
**Recommended:** `0`  
**Undo:** `1`
<!-- manual:end hide-task-view-button-on-taskbar -->

### Enable End Task <a id="enable-end-task"></a>
<!-- manual:start enable-end-task -->
**Supported on:** Windows 11 (newer builds; not available on Windows 10)  
**Info:** Adds “End Task” to the Windows 11 taskbar context menu to quickly kill unresponsive apps.  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\TaskbarDeveloperSettings`  
**Value:** `TaskbarEndTask`  
**Recommended:** `1`  
**Undo:** `0`
<!-- manual:end enable-end-task -->

### Make taskbar small <a id="make-taskbar-small"></a>
<!-- manual:start make-taskbar-small -->
**Supported on:** Windows 10 (native). Windows 11: limited/depends on build; not always supported.  
**Info:** Enables small taskbar icons (more compact taskbar).  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced`  
**Value:** `TaskbarSmallIcons`  
**Recommended:** `1`  
**Undo:** `0`
<!-- manual:end make-taskbar-small -->

### Always show all system tray icons <a id="always-show-all-system-tray-icons"></a>
<!-- manual:start always-show-all-system-tray-icons -->
**Supported on:** Windows 10
**Info:** Shows all notification area (system tray) icons instead of hiding some behind the overflow menu.  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer`  
**Value:** `EnableAutoTray`  
**Recommended:** `0`  
**Undo:** `1`
<!-- manual:end always-show-all-system-tray-icons -->

### Remove 'Meet Now' button from system tray <a id="remove-meet-now-button-from-system-tray"></a>
<!-- manual:start remove-meet-now-button-from-system-tray -->
**Supported on:** Windows 10 (Meet Now). Not applicable on Windows 11.  
**Info:** Removes the “Meet Now” button from the notification area / system tray.  
**Registry:** `HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer`  
**Value:** `HideSCAMeetNow`  
**Recommended:** `1`  
**Undo:** `0`
<!-- manual:end remove-meet-now-button-from-system-tray -->

### Disable Widgets <a id="disable-widgets"></a>
<!-- manual:start disable-widgets -->
**Supported on:** Windows 11  
**Info:** Enables the Widgets button / Widgets experience on the taskbar (policy based) that displays personalized news, weather, calendar, and other information.  
**Registry (CU):** `HKEY_CURRENT_USER\Software\Policies\Microsoft\Dsh`  
**Registry (LM):** `HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Dsh`  
**Value:** `AllowNewsAndInterests`  
**Recommended:** `0`  
**Undo:** `1`
<!-- manual:end disable-widgets -->

### Disable News and Interests <a id="disable-news-and-interests"></a>
<!-- manual:start disable-news-and-interests -->
**Supported on:** Windows 10 (News & Interests / Feeds). Not applicable on Windows 11.  
**Info:** Disables “News and Interests” / Feeds integration via policy (may affect feeds depending on Windows version).  
**Registry (CU):** `HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Windows Feeds`  
**Registry (LM):** `HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Windows Feeds`  
**Value:** `EnableFeeds`  
**Recommended:** `0`  
**Undo:** `1`
<!-- manual:end disable-news-and-interests -->

### Clean Taskbar <a id="clean-taskbar"></a>
<!-- manual:start clean-taskbar -->
**Supported on:** Windows 10, Windows 11  
**Info:** Clears pinned taskbar items by emptying the Taskband “Favorites” value and restarts Explorer.  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Taskband`  
**Value:** `Favorites` (REG_BINARY)  
**Action:** Set to empty byte array, then restart Explorer  
**Undo:** Not supported (pin layout cannot be reliably restored)

---
<!-- manual:end clean-taskbar -->

---

## Gaming

### Disable Game DVR <a id="disable-game-dvr"></a>
<!-- manual:start disable-game-dvr -->
**Info:** This feature will disable Game DVR.  
**Registry 1:** `HKEY_CURRENT_USER\System\GameConfigStore`
- `GameDVR_Enabled = 0`
- `GameDVR_FSEBehaviorMode = 2`
**Registry 2:** `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PolicyManager\default\ApplicationManagement\AllowGameDVR`
- `value = 0`
**Undo:**
- `GameDVR_Enabled = 1`
- `GameDVR_FSEBehaviorMode = 0`
- `value = 1`
<!-- manual:end disable-game-dvr -->

### Disable Power Throttling <a id="disable-power-throttling"></a>
<!-- manual:start disable-power-throttling -->
**Info:** This feature will disable Power Throttling.  
**Registry:** `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power\PowerThrottling`  
**Value:** `PowerThrottlingOff`  
**Recommended:** `1`  
**Undo:** `0`
<!-- manual:end disable-power-throttling -->

### Disable Visual Effects <a id="disable-visual-effects"></a>
<!-- manual:start disable-visual-effects -->
**Info:** Turns off visual effects like animations and shadows in Windows to boost performance.  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\VisualEffects`  
**Value:** `VisualFXSetting`  
**Recommended:** `0`  
**Undo:** `2`

---
<!-- manual:end disable-visual-effects -->

---

## Privacy & Telemetry

### Disable activity history <a id="disable-activity-history"></a>
<!-- manual:start disable-activity-history -->
**Supported on:** Windows 10, Windows 11  
**Info:** Disables activity history (prevents Windows from tracking and storing your activity).  
**Registry:** `HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System`  
**Value:** `PublishUserActivities`  
**Recommended:** `0`  
**Undo:** `1`
<!-- manual:end disable-activity-history -->

### Disable App Launch Tracking <a id="disable-app-launch-tracking"></a>
<!-- manual:start disable-app-launch-tracking -->
**Supported on:** Windows 10, Windows 11  
**Info:** Disables tracking of app launches (reduces Start/Search personalization like “most used apps”).  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced`  
**Value:** `Start_TrackProgs`  
**Recommended:** `0`  
**Undo:** Deletes `Start_TrackProgs` (returns to Windows default behavior)
<!-- manual:end disable-app-launch-tracking -->

### Reduce Diagnostic Data (Basic) <a id="reduce-diagnostic-data-basic"></a>
<!-- manual:start reduce-diagnostic-data-basic -->
**Supported on:** Windows 10, Windows 11  
**Info:** Sets diagnostic data level to **Basic/Required (1)** across multiple registry/policy locations (where applicable).  
**Recommended:** `1`  
**Undo:** Restores a more permissive/default-ish state (sets some values to `3` and removes policy values where possible)

**Registry changes (Do):**
- `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Diagnostics\DiagTrack`  
  - `ShowedToastAtLevel = 1`
- `HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\Windows\DataCollection`  
  - `AllowTelemetry = 1`
- `HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\DataCollection` *(may require Admin)*  
  - `AllowTelemetry = 1`
- `HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection`  
  - `AllowTelemetry = 1`  
  - `MaxTelemetryAllowed = 1`
- `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection` *(may require Admin)*  
  - `AllowTelemetry = 1`  
  - `MaxTelemetryAllowed = 1`

**Undo (details):**
- Sets `ShowedToastAtLevel = 3`
- Deletes policy values:  
  - `HKCU\SOFTWARE\Policies\Microsoft\Windows\DataCollection\AllowTelemetry`  
  - `HKLM\SOFTWARE\Policies\Microsoft\Windows\DataCollection\AllowTelemetry` *(may require Admin)*
- Sets non-policy keys to `3` (`AllowTelemetry`, `MaxTelemetryAllowed`) where possible
<!-- manual:end reduce-diagnostic-data-basic -->

### Disable location tracking <a id="disable-location-tracking"></a>
<!-- manual:start disable-location-tracking -->
**Supported on:** Windows 10, Windows 11  
**Info:** Prevents Windows from accessing your location.  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\LocationAndSensors`  
**Value:** `LocationEnabled`  
**Recommended:** `0`  
**Undo:** `1`
<!-- manual:end disable-location-tracking -->

### Disable Lock Screen Slideshow <a id="disable-lock-screen-slideshow"></a>
<!-- manual:start disable-lock-screen-slideshow -->
**Supported on:** Windows 10, Windows 11  
**Info:** Disables the lock screen slideshow option.  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager`  
**Value:** `SlideshowEnabled`  
**Recommended:** `0`  
**Undo:** Deletes `SlideshowEnabled` (returns to Windows default behavior)
<!-- manual:end disable-lock-screen-slideshow -->

### Disable Narrator Online Services <a id="disable-narrator-online-services"></a>
<!-- manual:start disable-narrator-online-services -->
**Supported on:** Windows 10, Windows 11  
**Info:** Disables Narrator online services (cloud-backed features).  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Narrator\NoRoam`  
**Value:** `OnlineServicesEnabled`  
**Recommended:** `0`  
**Undo:** Deletes `OnlineServicesEnabled` (returns to Windows default behavior)
<!-- manual:end disable-narrator-online-services -->

### Disable Online Speech Recognition <a id="disable-online-speech-recognition"></a>
<!-- manual:start disable-online-speech-recognition -->
**Supported on:** Windows 10, Windows 11  
**Info:** Disables online speech recognition and disables input personalization via policy.  
**Recommended:** `0`  
**Undo:** Restores speech setting to `1` and removes policy values where possible

**Registry changes (Do):**
- `HKEY_CURRENT_USER\Software\Microsoft\Speech_OneCore\Settings\OnlineSpeechPrivacy`  
  - `HasAccepted = 0`
- `HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\InputPersonalization`  
  - `AllowInputPersonalization = 0`
- `HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\InputPersonalization` *(may require Admin)*  
  - `AllowInputPersonalization = 0`

**Undo (details):**
- Sets `HasAccepted = 1`
- Deletes policy values:  
  - `HKCU\SOFTWARE\Policies\Microsoft\InputPersonalization\AllowInputPersonalization`  
  - `HKLM\SOFTWARE\Policies\Microsoft\InputPersonalization\AllowInputPersonalization` *(may require Admin)*
<!-- manual:end disable-online-speech-recognition -->

### Disable Privacy Settings Experience at sign-in <a id="disable-privacy-settings-experience-at-sign-in"></a>
<!-- manual:start disable-privacy-settings-experience-at-sign-in -->
**Supported on:** Windows 10, Windows 11  
**Info:** Disables Privacy Settings Experience at sign-in (OOBE privacy prompts).  
**Registry:** `HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\OOBE`  
**Value:** `DisablePrivacyExperience`  
**Recommended:** `1`  
**Undo:** `0`
<!-- manual:end disable-privacy-settings-experience-at-sign-in -->

### Prevent Silent App Installation <a id="prevent-silent-app-installation"></a>
<!-- manual:start prevent-silent-app-installation -->
**Supported on:** Windows 10, Windows 11  
**Info:** Prevents silent background installation of suggested/promoted apps.  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager`  
**Value:** `SilentInstalledAppsEnabled`  
**Recommended:** `0`  
**Undo:** `1`
<!-- manual:end prevent-silent-app-installation -->

### Disable Spotlight on Lock Screen <a id="disable-spotlight-on-lock-screen"></a>
<!-- manual:start disable-spotlight-on-lock-screen -->
**Supported on:** Windows 10, Windows 11  
**Info:** Disables rotating Windows Spotlight images on the lock screen.  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager`  
**Value:** `RotatingLockScreenEnabled`  
**Recommended:** `0`  
**Undo:** `1`

---
<!-- manual:end disable-spotlight-on-lock-screen -->

---

## AI (Copilot & Recall)

### Don't Show Copilot in Taskbar <a id="dont-show-copilot-in-taskbar"></a>
<!-- manual:start dont-show-copilot-in-taskbar -->
**Info:** This feature will disable Copilot in Taskbar.  
**Registry:** `HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\WindowsCopilot`  
**Value:** `TurnOffWindowsCopilot`  
**Recommended:** `1`  
**Undo:** `0`
<!-- manual:end dont-show-copilot-in-taskbar -->

### Turn off Recall in Windows 11 <a id="turn-off-recall-in-windows-11"></a>
<!-- manual:start turn-off-recall-in-windows-11 -->
**Info:** This will remove Recall from Windows 11 24H2  
**Registry:** `HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsAI`  
**Value:** `AllowRecallEnablement`  
**Recommended:** `0`  
**Undo:** `1`
<!-- manual:end turn-off-recall-in-windows-11 -->

### Disable Click to Do (Only Copilot+ PCs) <a id="disable-click-to-do-only-copilot-pcs"></a>
<!-- manual:start disable-click-to-do-only-copilot-pcs -->
**Info:** Disables Click to Do entirely, including its context menu entry which uses on-device AI to suggest actions based on screen content. Only available on Copilot+ PCs with Windows 11 24H2 or newer.  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\ClickToDo`  
**Value:** `DisableClickToDo`  
**Recommended:** `1`  
**Undo:** `0`
**Notes:** Only available on Copilot+ PCs (Windows 11 24H2 or newer, requires NPU).
<!-- manual:end disable-click-to-do-only-copilot-pcs -->

### Disable Bing search results <a id="disable-bing-search-results"></a>
<!-- manual:start disable-bing-search-results -->
**Info:** Windows Search is cluttered mess with suggestions from Microsoft, the day’s highlights, Top apps, AI Tools, Trending searches, Games for you, Trending news from the web, and, to make matters worse, there’s the Copilot logo on the top left. 
**Registry:** `HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer`  
**Value:** `DisableSearchBoxSuggestions`  
**Recommended:** `1`  
**Undo:** `0`

---
<!-- manual:end disable-bing-search-results -->

---

## Ads & Recommendations

### Disable File Explorer Ads <a id="disable-file-explorer-ads"></a>
<!-- manual:start disable-file-explorer-ads -->
**Info:** Disables File Explorer ads (sync provider notifications).  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced`  
**Value:** `ShowSyncProviderNotifications`  
**Recommended:** `0`  
**Undo:** `1`  
<!-- manual:end disable-file-explorer-ads -->

### Disable Finish Setup Ads <a id="disable-finish-setup-ads"></a>
<!-- manual:start disable-finish-setup-ads -->
**Info:** Disables “Finish setting up your device” suggestions (SCOOBE prompts).  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\UserProfileEngagement`  
**Value:** `ScoobeSystemSettingEnabled`  
**Recommended:** `0`  
**Undo:** `1`  
<!-- manual:end disable-finish-setup-ads -->

### Disable Lock Screen Tips and Ads <a id="disable-lock-screen-tips-and-ads"></a>
<!-- manual:start disable-lock-screen-tips-and-ads -->
**Info:** Disables lock screen tips / overlays and related content delivery entries.  
**Registry:** `HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager`  
**Values:** `RotatingLockScreenOverlayEnabled`, `SubscribedContent-338387Enabled`  
**Recommended:** `0`  
**Undo:** `1`  
<!-- manual:end disable-lock-screen-tips-and-ads -->

### Disable Personalized Ads <a id="disable-personalized-ads"></a>
<!-- manual:start disable-personalized-ads -->
**Info:** Disables personalized ads by turning off the advertising ID.  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo`  
**Value:** `Enabled`  
**Recommended:** `0`  
**Undo:** `1`  
<!-- manual:end disable-personalized-ads -->

### Disable Settings Ads <a id="disable-settings-ads"></a>
<!-- manual:start disable-settings-ads -->
**Info:** Disables suggestions/ads inside the Settings app.  
**Registry:** `HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager`  
**Values:** `SubscribedContent-338393Enabled`, `SubscribedContent-353694Enabled`, `SubscribedContent-353696Enabled`  
**Recommended:** `0`  
**Undo:** `1`  
<!-- manual:end disable-settings-ads -->

### Disable Start menu Ads <a id="disable-start-menu-ads"></a>
<!-- manual:start disable-start-menu-ads -->
**Info:** Disables Start menu recommendations/ads.  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced`  
**Value:** `Start_IrisRecommendations`  
**Recommended:** `0`  
**Undo:** `1`  
<!-- manual:end disable-start-menu-ads -->

### Disable Tailored experiences <a id="disable-tailored-experiences"></a>
<!-- manual:start disable-tailored-experiences -->
**Info:** Disables tailored experiences (personalized tips, ads, and recommendations using diagnostic data).  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Privacy`  
**Value:** `TailoredExperiencesWithDiagnosticDataEnabled`  
**Recommended:** `0`  
**Undo:** `1`  
<!-- manual:end disable-tailored-experiences -->

### Disable General Tips and Ads <a id="disable-general-tips-and-ads"></a>
<!-- manual:start disable-general-tips-and-ads -->
**Info:** Disables general tips, suggestions, and content delivery notifications.  
**Registry:** `HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager`  
**Value:** `SubscribedContent-338389Enabled`  
**Recommended:** `0`  
**Undo:** `1`  
<!-- manual:end disable-general-tips-and-ads -->

### Disable Welcome Experience Ads <a id="disable-welcome-experience-ads"></a>
<!-- manual:start disable-welcome-experience-ads -->
**Info:** Disables “Welcome experience” suggestions/ads.  
**Registry:** `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager`  
**Value:** `SubscribedContent-310093Enabled`  
**Recommended:** `0`  
**Undo:** `1`  

---
<!-- manual:end disable-welcome-experience-ads -->


<!-- generated:features:end -->
## Plugins <a name="plugins"></a>


> Plugins are PowerShell scripts that run additional tweaks or external tools.
> Some plugins require Administrator privileges.

### ChrisTitusApp
**Info:** Downloads and executes the Chris Titus Tech Windows utility script.  
**Command:** `irm christitus.com/win | iex`  
**Requirements:** Internet connection.  
**Notes:** This runs remote code directly in PowerShell. Only use if you trust the source.

### Create Restore Point
**Info:** Creates a System Restore Point named `Bloatynosy NueEx Restore Point`.  
**Requirements:** Must be run as Administrator.  
**How it works:** Uses WMI (`Win32_SystemRestore`) to create the restore point and shows progress + a completion dialog.  

### Disable Snap Assist Flyout (NX)
**Info:** Disables the Snap Assist flyout in Windows 11.  
**Check:** `HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced` → `EnableSnapAssistFlyout`  
**Do:** Sets `EnableSnapAssistFlyout = 0` and restarts Explorer.  
**Undo:** Sets `EnableSnapAssistFlyout = 1` and restarts Explorer.  
**Expected (enabled state):** `EnableSnapAssistFlyout = 0x0`

### File Extensions Visibility (NX)
**Info:** Hides known file extensions and disables viewing super hidden files.  
**Check:**  
- `HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced` → `HideFileExt`  
- `HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced` → `ShowSuperHidden`  
**Do:** Sets `HideFileExt = 1` and `ShowSuperHidden = 0`, then restarts Explorer.  
**Undo:** Sets `HideFileExt = 0` and `ShowSuperHidden = 1`, then restarts Explorer.  
**Expected (enabled state):** `HideFileExt = 0x1`, `ShowSuperHidden = 0x0`

### Remove Ask Copilot (NX)
**Info:** Removes the “Ask Copilot” context menu entry.  
**Check/Do/Undo:** Uses the Shell Extensions “Blocked” list.  
**Registry:** `HKCU\Software\Microsoft\Windows\CurrentVersion\Shell Extensions\Blocked`  
**Do:** Adds CLSID `{CB3B0003-8088-4EDE-8769-8B354AB2FF8C}`  
**Undo:** Deletes that CLSID value.

### Remove Edit with Clipchamp (NX)
**Info:** Removes the “Edit with Clipchamp” context menu entry.  
**Registry:** `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Shell Extensions\Blocked`  
**Do:** Adds CLSID `{8AB635F8-9A67-4698-AB99-784AD929F3B4}`  
**Undo:** Deletes that CLSID value.

### Remove Edit with Notepad (NX)
**Info:** Removes the “Edit with Notepad” context menu entry.  
**Registry:** `HKCU\Software\Microsoft\Windows\CurrentVersion\Shell Extensions\Blocked`  
**Do:** Adds CLSID `{CA6CC9F1-867A-481E-951E-A28C5E4F01EA}`  
**Undo:** Deletes that CLSID value.

### Remove Edit with Photos (NX)
**Info:** Removes the “Edit with Photos” context menu entry.  
**Registry:** `HKCU\Software\Microsoft\Windows\CurrentVersion\Shell Extensions\Blocked`  
**Do:** Adds CLSID `{BFE0E2A4-C70C-4AD7-AC3D-10D1ECEBB5B4}`  
**Undo:** Deletes that CLSID value.

### Remove default apps
**Info:** Uninstalls a large list of built-in Windows apps (Appx) for all users and removes provisioned packages, then disables re-install/suggested app behavior.  
**Requirements:** Elevated privileges (Admin) and PowerShell execution allowed.  
**Actions:**
- Removes packages via `Get-AppxPackage ... | Remove-AppxPackage -AllUsers`
- Removes provisioned packages via `Remove-AppxProvisionedPackage -Online`
- Sets multiple `ContentDeliveryManager` values to `0`
- Sets `HKLM:\SOFTWARE\Policies\Microsoft\WindowsStore` → `AutoDownload = 2`
- Sets `HKLM:\SOFTWARE\Policies\Microsoft\Windows\CloudContent` → `DisableWindowsConsumerFeatures = 1`  
**Undo:** Not provided (you would need to reinstall apps manually).

### Remove Windows AI
This script is no longer available as a plugin. You can now find it as an extension with its own interface in the tools section.

### Restart Explorer
**Info:** Restarts Windows Explorer (explorer.exe). Useful after registry tweaks so changes apply immediately.  
**Actions:**
- Stops Explorer (`Stop-Process -Name explorer -Force`)
- Starts Explorer (`Start-Process explorer.exe`)
**Undo:** Not required.

### Restore all built-in apps
**Info:** Attempts to restore/re-register built-in Windows apps for all users.  
**Actions:**
- Re-registers AppX packages using `Add-AppxPackage -Register`
- Targets packages for all users where possible
**Notes:** Depending on Windows version, some apps might require Microsoft Store or additional components.
**Undo:** Not required.

### Shutdown Time (NX)
**Info:** Sets a lower `WaitToKillServiceTimeout` value to speed up shutdown.  
**Registry:** `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control`  
**Value:** `WaitToKillServiceTimeout`  
**Do:** Sets to a faster value (e.g., `1000`) and applies immediately.  
**Undo:** Restores the default/previous value (commonly `5000`).  

### Uninstall OneDrive
**Info:** Uninstalls Microsoft OneDrive and removes leftover OneDrive integration where possible.  
**Requirements:** Administrator privileges recommended.  
**Actions:**
- Runs the OneDrive installer with `/uninstall` (per-user / per-machine depending on system)
- Removes remaining OneDrive folders (if present)
- Attempts to remove OneDrive from Explorer integration (where applicable)
**Undo:** Reinstall OneDrive manually (e.g., via Microsoft installer).

### User Account Control (NX)
**Info:** Toggles User Account Control (UAC) behavior.  
**Registry:** `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System`  
**Values (common):**
- `EnableLUA`
- `ConsentPromptBehaviorAdmin`
- `PromptOnSecureDesktop`
**Notes:** Changing UAC may require a reboot to fully apply.
**Undo:** Restores the previous/default UAC settings.
