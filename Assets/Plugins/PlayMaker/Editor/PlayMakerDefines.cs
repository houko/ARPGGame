using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace HutongGames.PlayMakerEditor
{
    /// <summary>
    /// Adds Playmaker defines to project
    /// Other tools can now use #if PLAYMAKER
    /// Package as source code so user can remove or modify
    /// </summary>
    [InitializeOnLoad]
    public class PlayMakerDefines
    {
        static PlayMakerDefines()
        {
            DefinesHelper.AddSymbolToAllTargets("PLAYMAKER");

            DefinesHelper.AddSymbolToAllTargets("PLAYMAKER_1_9");
            DefinesHelper.AddSymbolToAllTargets("PLAYMAKER_1_9_0");
            DefinesHelper.AddSymbolToAllTargets("PLAYMAKER_1_8_OR_NEWER");
            DefinesHelper.AddSymbolToAllTargets("PLAYMAKER_1_8_5_OR_NEWER");
            DefinesHelper.AddSymbolToAllTargets("PLAYMAKER_1_9_OR_NEWER");
            
            DefinesHelper.RemoveSymbolFromAllTargets("PLAYMAKER_1_8");
            DefinesHelper.RemoveSymbolFromAllTargets("PLAYMAKER_1_8_0");
            DefinesHelper.RemoveSymbolFromAllTargets("PLAYMAKER_1_8_1");
            DefinesHelper.RemoveSymbolFromAllTargets("PLAYMAKER_1_8_2");
            DefinesHelper.RemoveSymbolFromAllTargets("PLAYMAKER_1_8_3");
            DefinesHelper.RemoveSymbolFromAllTargets("PLAYMAKER_1_8_4");
            DefinesHelper.RemoveSymbolFromAllTargets("PLAYMAKER_1_8_5");
            DefinesHelper.RemoveSymbolFromAllTargets("PLAYMAKER_1_8_6");
            DefinesHelper.RemoveSymbolFromAllTargets("PLAYMAKER_1_8_7");
            DefinesHelper.RemoveSymbolFromAllTargets("PLAYMAKER_1_8_8");
            DefinesHelper.RemoveSymbolFromAllTargets("PLAYMAKER_1_8_9");
        }

        public static void AddScriptingDefineSymbolToAllTargets(string defineSymbol)
        {
            DefinesHelper.AddSymbolToAllTargets(defineSymbol);
        }

        public static void RemoveScriptingDefineSymbolFromAllTargets(string defineSymbol)
        {
            DefinesHelper.RemoveSymbolFromAllTargets(defineSymbol);
        }

    }
}

