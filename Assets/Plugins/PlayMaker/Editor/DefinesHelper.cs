using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace HutongGames.PlayMakerEditor
{
    /// <summary>
    /// Helper class for adding/removing define symbols
    /// </summary>
    public class DefinesHelper
    {
        public static void AddSymbolToAllTargets(string defineSymbol)
        {
            foreach (BuildTargetGroup group in Enum.GetValues(typeof(BuildTargetGroup)))
            {
                if (!IsValidBuildTargetGroup(group)) continue;

                var defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(group).Split(';').Select(d => d.Trim()).ToList();
                if (!defineSymbols.Contains(defineSymbol))
                {
                    defineSymbols.Add(defineSymbol);
                    try
                    {
                        PlayerSettings.SetScriptingDefineSymbolsForGroup(group, string.Join(";", defineSymbols.ToArray()));
                    }
                    catch (Exception)
                    {
                        Debug.Log("Could not set PLAYMAKER defines for build target group: " + group);
                        throw;
                    }                  
                }
            }
        }

        public static void RemoveSymbolFromAllTargets(string defineSymbol)
        {
            foreach (BuildTargetGroup group in Enum.GetValues(typeof(BuildTargetGroup)))
            {
                if (!IsValidBuildTargetGroup(group)) continue;

                var defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(group).Split(';').Select(d => d.Trim()).ToList();
                if (defineSymbols.Contains(defineSymbol))
                {
                    defineSymbols.Remove(defineSymbol);
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(group, string.Join(";", defineSymbols.ToArray()));
                }
            }
        }

        private static bool IsValidBuildTargetGroup(BuildTargetGroup group)
        {
            if (group == BuildTargetGroup.Unknown || IsObsolete(group)) return false;

            // Checking Obsolete attribute should be enough, 
            // but sometimes Unity versions are missing attributes
            // so keeping these checks around just in case:

#if UNITY_5_3_0 || UNITY_5_3 // Unity 5.3.0 had tvOS in enum but throws error if used
            if ((int)(object)group == 25) return false;
#endif

#if UNITY_5_4 || UNITY_5_5 // Unity 5.4+ doesn't like Wp8 and Blackberry any more
            if ((int)(object)group == 15) return false;
            if ((int)(object)group == 16) return false;
#endif

/*
#if UNITY_5_6 // Unity 5.6 bug
            if ((int)(object)group == 27) return false;
#endif
*/

            // Not making a build with Unity 5.6
            // So check unityVersion string instead of symbol
            if (Application.unityVersion.StartsWith("5.6"))
            {
                if ((int)(object)group == 27) return false;
            }

            return true;
        }

        private static bool IsObsolete(Enum value)
        {
            var enumInt = (int)(object)value;
            if (enumInt == 4 || enumInt == 14) return false;

            var field = value.GetType().GetField(value.ToString());
            var attributes = (ObsoleteAttribute[])field.GetCustomAttributes(typeof(ObsoleteAttribute), false);
            return attributes.Length > 0;
        }

        /* NOTE: IsObsolete is complicated by the definition of BuildTargetGroup enum. 
         * E.g., in Unity 5.4:
         * 
          public enum BuildTargetGroup
          {
            Unknown = 0,
            Standalone = 1,
            [Obsolete("WebPlayer was removed in 5.4, consider using WebGL")] WebPlayer = 2,
            iOS = 4,
            [Obsolete("Use iOS instead (UnityUpgradable) -> iOS", true)] iPhone = 4,
            PS3 = 5,
            XBOX360 = 6,
            Android = 7,
            WebGL = 13,
            [Obsolete("Use WSA instead")] Metro = 14,
            WSA = 14,
            [Obsolete("Use WSA instead")] WP8 = 15,
            [Obsolete("BlackBerry has been removed as of 5.4")] BlackBerry = 16,
            Tizen = 17,
            PSP2 = 18,
            PS4 = 19,
            PSM = 20,
            XboxOne = 21,
            SamsungTV = 22,
            Nintendo3DS = 23,
            WiiU = 24,
            tvOS = 25,
          }
  
         */

    }
}

