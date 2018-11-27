#if (UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3)
#define UNITY_PRE_5_4
#endif

#if UNITY_5_3_OR_NEWER || UNITY_5 || UNITY_5_0
#define UNITY_5_OR_NEWER
#endif

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

#if UNITY_5_OR_NEWER

namespace HutongGames.PlayMakerEditor
{
    /// <summary>
    /// Try to fix common update problems automatically
    /// Update Tasks:
    /// -- Move Playmaker.dll from Assets\PlayMaker to Assets\Plugins\PlayMaker
    /// -- Set plugin import settings
    /// </summary>
    [InitializeOnLoad]
    public class PlayMakerAutoUpdater
    {
        public const string PlaymakerGUID = "e743331561ef77147ae48cda9bcb8209";
        public const string PlaymakerPluginDirectory = "Assets/Plugins/PlayMaker";
        public const string PlaymakerPluginPath = PlaymakerPluginDirectory + "/PlayMaker.dll";
        public const string PlaymakerMetroPluginPath = PlaymakerPluginDirectory + "/Metro/PlayMaker.dll";

        // list of updates the updater would like to perform
        static List<string> updateList = new List<string>();

        // list of changes the updater made
        static List<string> changeList = new List<string>();

        private static readonly BuildTarget[] standardPlatforms =
        {
            BuildTarget.Android,
#if UNITY_PRE_5_4
            BuildTarget.BlackBerry,
            BuildTarget.WebPlayer,
            BuildTarget.WebPlayerStreamed,
#endif

#if !UNITY_2017_3_OR_NEWER
            BuildTarget.StandaloneOSXIntel,
            BuildTarget.StandaloneOSXIntel64,
#else
            BuildTarget.StandaloneOSX,
#endif

            BuildTarget.StandaloneLinux,
            BuildTarget.StandaloneLinux64,
            BuildTarget.StandaloneLinuxUniversal,
            BuildTarget.StandaloneWindows,
            BuildTarget.StandaloneWindows64,
            BuildTarget.iOS
        };

        // static constructor called on load
        static PlayMakerAutoUpdater()
        {
            if (EditorStartupPrefs.AutoUpdateProject)
            {
                // Can't call assetdatabase here, so use update callback
                EditorApplication.update -= RunAutoUpdate;
                EditorApplication.update += RunAutoUpdate;
            }
        }

        // Check pre-requisites for auto updating
        // e.g., Unity 5 version of Playmaker is imported
        static bool CheckRequirements()
        {
            // If project doesn't have this folder user hasn't updated Playmaker for Unity5
            if (!EditorApp.IsSourceCodeVersion && !AssetDatabase.IsValidFolder(PlaymakerPluginDirectory))
            {
                EditorUtility.DisplayDialog("PlayMaker AutoUpdater",
                    "Please import Playmaker for Unity 5." +
                    "\n\nTo get the latest version, update in the Unity Asset Store " +
                    "or download from Hutong Games Store.", "OK");
                Debug.Log("PlayMaker AutoUpdater: Please import Playmaker for Unity 5.");
                EditorPrefs.DeleteKey("PlayMaker.LastAutoUpdate");
                return false;
            }
            return true;
        }

        // Called from menu, so we want No Updates Needed dialog.
        public static void OpenAutoUpdater()
        {
            if (NeedsUpdate())
            {
                RunAutoUpdate();
            }
            else
            {
                EditorUtility.DisplayDialog("PlayMaker", "AutoUpdater:\n\nNo updates needed...", "OK");
            }
        }

        public static void RunAutoUpdate()
        {
            //Debug.Log("PlayMaker AutoUpdater " + version);
            EditorApplication.update -= RunAutoUpdate;

            if (!CheckRequirements())
            {
                //Debug.Log("PlayMaker AutoUpdate: Could not auto-update.");
                return;
            }

            if (NeedsUpdate())
            {
                var updateMessage = "NOTE: ALWAYS BACKUP your project before updating PlayMaker or Unity!\n\nPlayMaker AutoUpdater would like to make these changes to the project:\n\n";
                foreach (var update in updateList)
                {
                    updateMessage += "- " + update + "\n";
                }
                updateMessage += "\n\nNOTE: You can run the AutoUpdater later from PlayMaker > Tools > Run AutoUpdater";
                if (EditorUtility.DisplayDialog("PlayMaker", updateMessage, "OK", "Cancel"))
                {
                    DoUpdate();
                }
            }

            // Fail silently so we don't spam user with "No Update Needed" dialogs
        }

        static bool NeedsUpdate()
        {
            updateList.Clear();

            if (PlaymakerDllsNeedMoving())
            {
                updateList.Add("Move Playmaker dlls to Plugin folders.");
            }

            if (DuplicatePlaymakerDllExists())
            {
                updateList.Add("Fix duplicate Playmaker dlls from previous install.");
            }

            if (HasOldEditorLanguageResources())
            {
                updateList.Add("Rename old Playmaker language resource files.");
            }

            return updateList.Count > 0;
        }

        /// <summary>
        /// Check if PlayMaker.dll is not it Assets/Plugins/PlayMaker
        /// </summary>
        static bool PlaymakerDllsNeedMoving()
        {
            var playmakerPath = AssetDatabase.GUIDToAssetPath(PlaymakerGUID);
            if (string.IsNullOrEmpty(playmakerPath))
                return false;
            var playmakerDirectory = Path.GetDirectoryName(playmakerPath);
            return !playmakerDirectory.Equals(PlaymakerPluginDirectory, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Unity5.0-5.2 could make duplicate files on import.
        /// E.g., PlayMaker.dll could be imported as PlayMaker 1.dll
        /// This checks for this duplicate dll.
        /// </summary>
        /// <returns></returns>
        static bool DuplicatePlaymakerDllExists()
        {
            var playmakerPath = AssetDatabase.GUIDToAssetPath(PlaymakerGUID);
            if (string.IsNullOrEmpty(playmakerPath))
                return false;
            var playmakerFilename = Path.GetFileName(playmakerPath);
            return playmakerFilename != "PlayMaker.dll"; // E.g. PlayMaker 1.dll
        }

        /// <summary>
        /// Unity 5+ changed the way language resource dlls were loaded.
        /// Resource dlls formatted for Unity 4.x create duplicate resource errors in Unity 5.x
        /// So if a user upgrades a Unity 4.x project to Unity 5.x these language resources need to be fixed.
        /// Unfortunately the resulting error blocks this auto-update script from running! 
        /// Including this anyway, in case this is "fixed" in a future Unity version...
        /// </summary>
        /// <returns></returns>
        static bool HasOldEditorLanguageResources()
        {
            var languageCodes = new[] {"de"}; // add other languages here
            foreach (var languageCode in languageCodes)
            {
                if (OldLanguageResourceExists(languageCode)) return true;
            }
            return false;
        }

        static bool OldLanguageResourceExists(string languageCode)
        {
            var resourceFile = AssetDatabase.FindAssets("PlayMakerEditor."+ languageCode +".resources.dll");
            return resourceFile.Length > 0;
        }

        static void DoUpdate()
        {
            FixDuplicatePlaymakerDlls();

            MovePlayMakerPlugin();
            FixPlayMakerPluginSettings(PlaymakerPluginPath); //(note doing this before move doesn't seem to take)

            MovePlayMakerMetroPlugin();
            FixPlayMakerMetroPluginSettings(PlaymakerMetroPluginPath);

            ReportChanges();
        }

        static PluginImporter GetPluginImporter(string pluginPath)
        {
            var pluginImporter = (PluginImporter)AssetImporter.GetAtPath(pluginPath);
            if (pluginImporter != null)
            {
                return pluginImporter;
            }

            Debug.LogWarning("Couldn't find plugin: " + pluginPath);
            return null;
        }

        static void FixPlayMakerPluginSettings(string pluginPath)
        {
            var pluginImporter = GetPluginImporter(pluginPath);
            if (pluginImporter != null)
            {
                FixPlayMakerPluginSettings(pluginImporter);
            }
        }

        static void FixPlayMakerPluginSettings(PluginImporter pluginImporter)
        {
            LogChange("Fixed Plugin Settings: " + pluginImporter.assetPath);

            pluginImporter.SetCompatibleWithAnyPlatform(false);
            pluginImporter.SetCompatibleWithEditor(true);
            SetCompatiblePlatforms(pluginImporter, standardPlatforms);
            pluginImporter.SaveAndReimport();
            //AssetDatabase.Refresh();
        }

        static void FixPlayMakerMetroPluginSettings(string pluginPath)
        {
            var pluginImporter = GetPluginImporter(pluginPath);
            if (pluginImporter != null)
            {
                FixPlayMakerMetroPluginSettings(pluginImporter);
            }
        }

        static void FixPlayMakerMetroPluginSettings(PluginImporter pluginImporter)
        {
            LogChange("Fixed Plugin Settings: " + pluginImporter.assetPath);

            pluginImporter.SetCompatibleWithAnyPlatform(false);
            pluginImporter.SetCompatibleWithPlatform(BuildTarget.WSAPlayer, true);
            pluginImporter.SaveAndReimport();
            //AssetDatabase.Refresh();
        }

        static void SetCompatiblePlatforms(PluginImporter pluginImporter, IEnumerable<BuildTarget> platforms)
        {
            foreach (var platform in platforms)
            {
                pluginImporter.SetCompatibleWithPlatform(platform, true);
            }
        }

        static void MovePlayMakerPlugin()
        {
            var playmakerPath = AssetDatabase.GUIDToAssetPath(PlaymakerGUID);
            MoveAsset(playmakerPath, PlaymakerPluginPath);
        }

        static void MovePlayMakerMetroPlugin()
        {
            MoveAsset("Assets/Plugins/Metro/PlayMaker.dll", PlaymakerMetroPluginPath);
        }

        static void MoveAsset(string from, string to)
        {
            if (from == to || string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(from)))
                return;

            LogChange("Moving " + from + " to: " + to);
            AssetDatabase.DeleteAsset(to);
            AssetDatabase.Refresh();
            var error = AssetDatabase.MoveAsset(from, to);
            if (!string.IsNullOrEmpty(error))
            {
                LogChange(error);
            }
            AssetDatabase.Refresh();
        }

        static void FixDuplicatePlaymakerDlls()
        {
            if (!DuplicatePlaymakerDllExists()) return;

            var playmakerPath = AssetDatabase.GUIDToAssetPath("e743331561ef77147ae48cda9bcb8209");
            MoveAsset(playmakerPath, PlaymakerPluginPath);
        }

        static void LogChange(string change)
        {
            //Debug.Log("PlayMaker AutoUpdate: " + change);
            changeList.Add(change);
        }

        static void ReportChanges()
        {
            if (changeList.Count > 0)
            {
                var changeLog = "PlayMaker AutoUpdater Changes:";
                foreach (var change in changeList)
                {
                    changeLog += "\n" + change;
                }
                Debug.Log(changeLog);
            }
            else
            {
                Debug.Log("PlayMaker AutoUpdater: No changes made");
            }
        }

    }
}

#endif