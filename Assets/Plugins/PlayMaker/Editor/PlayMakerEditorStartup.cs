using System;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace HutongGames.PlayMakerEditor
{
    /// <summary>
    /// Manages initialization of PlayMaker Editor classes
    /// Before Unity 5.4 a lot of this was done in EditorWindow constructors.
    /// In Unity 5.4+ this is not allowed and throws an error.
    /// Unity API calls are also not allowed in constructors of Serializable classes.
    /// So instead we do it all here in a non-Serializable class.
    /// </summary>
    [InitializeOnLoad]
    public class PlayMakerEditorStartup
    {
        static PlayMakerEditorStartup()
        {
            //Debug.Log(EditorApplication.timeSinceStartup);
            //Debug.Log(GetPlayMakerVersionInfo());
            //Debug.Log(GetPlayMakerVersion());

#if PLAYMAKER_1_8

            // Define might be left over from old installation so use reflection instead of direct call
            // Could also use GetPlayMakerVersion()
            //PlayMakerGlobals.InitApplicationFlags();

            if (!InitApplicationFlags())
            {
                Debug.LogWarning("PLAYMAKER_1_8 is defined in Scripting Define Symbols, but Playmaker is not installed!"+
                    "\nPlease remove PLAYMAKER defines from Player Settings.");
            }    
#endif

            // Resources.Load fails in static constructor (unity bug?)
            // So we delay some work that needs PlayMakerEditorPrefs until next update

            EditorApplication.update -= DoWelcomeScreen;
            EditorApplication.update += DoWelcomeScreen;

            // Constructor is also called on Playmode change
            // So we need to handle that case (e.g., don't show welcome window)
            // NOTE: This only matters during the startupTime. 
            // If we find a better way to do that we can remove this.
#if UNITY_2017_2_OR_NEWER
            EditorApplication.playModeStateChanged -= PlayModeChanged;
            EditorApplication.playModeStateChanged += PlayModeChanged;
#else
            EditorApplication.playmodeStateChanged -= PlayModeChanged;
            EditorApplication.playmodeStateChanged += PlayModeChanged;
#endif
        }

        // Used to happen in PlayMakerGlobals constructor, 
        // but 5.4 made that illegal...?
        private static bool InitApplicationFlags()
        {
#if PLAYMAKER_SOURCE
            PlayMakerGlobals.InitApplicationFlags();
            return true;
#else
            var playMakerGlobals = GetType("PlayMakerGlobals");
            if (playMakerGlobals != null)
            {
                var init = playMakerGlobals.GetMethod("InitApplicationFlags");
                if (init != null)
                {
                    init.Invoke(null, null);
                    return true;
                }
            }
            return false;
#endif
        }

        private static void DoWelcomeScreen()
        {
            //Debug.Log("ShowWelcomeScreen: " + PlayMakerEditorPrefs.ShowWelcomeScreen);
            //Debug.Log("WelcomeScreenVersion: " + PlayMakerEditorPrefs.WelcomeScreenVersion);

            const float startupTime = 30f; // time window to filter startup events from re-compiles. TODO: Is there a better way?
            var showAtStartup = EditorStartupPrefs.ShowWelcomeScreen && EditorApplication.timeSinceStartup < startupTime;
            var newVersionImported = EditorStartupPrefs.WelcomeScreenVersion != PlayMakerWelcomeWindow.Version;

            if (showAtStartup || newVersionImported)
            {
                PlayMakerWelcomeWindow.Open();
            }

            // record the welcome screen version
            EditorStartupPrefs.WelcomeScreenVersion = PlayMakerWelcomeWindow.Version;

            EditorApplication.update -= DoWelcomeScreen;
        }

#if UNITY_2017_2_OR_NEWER
        private static void PlayModeChanged(PlayModeStateChange playMode)
        {
            EditorApplication.update -= DoWelcomeScreen;
        }
#else
        private static void PlayModeChanged()
        {
            EditorApplication.update -= DoWelcomeScreen;
        }
#endif

        // Normally we would use ReflectionUtils.GetGlobalType but this code needs to be standalone
        // Instead of copy/pasting ReflectionUtils, decided to try this code from UnityAnswers:
        // http://answers.unity3d.com/questions/206665/typegettypestring-does-not-work-in-unity.html
        public static Type GetType(string typeName)
        {
            // Try Type.GetType() first. This will work with types defined
            // by the Mono runtime, in the same assembly as the caller, etc.
            var type = Type.GetType(typeName);

            // If it worked, then we're done here
            if (type != null)
                return type;

            // otherwise look in loaded assemblies
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(typeName);
                if (type != null)
                {
                    break;
                }
            }

            return type;
        }

        public static Type FindTypeInLoadedAssemblies(string typeName)
        {
            Type _type = null;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                _type = assembly.GetType(typeName);
                if (_type != null)
                    break;
            }

            return _type;
        }
    }
}
