//-----------------------------------------------------------------------
// <copyright file="PlayMakerGlobalsInspector.cs" company="Hutong Games LLC">
// Copyright (c) Hutong Games LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEditor;
using UnityEngine;

namespace HutongGames.PlayMakerEditor
{
    [CustomEditor(typeof(PlayMakerGlobals))]
    internal class PlayMakerGlobalsInspector : UnityEditor.Editor
    {
	    private PlayMakerGlobals globals;
	    private List<FsmVariable> variableList;

        public void OnEnable()
	    {
		    globals = target as PlayMakerGlobals;
		    BuildVariableList();
	    }

	    public override void OnInspectorGUI()
	    {
            FsmEditorStyles.Init();

            DoGlobalVariablesGUI();
	        DoGlobalEventsGUI();

	        GUILayout.Space(5);

	        if (GUILayout.Button("Refresh"))
	            Refresh();

            GUILayout.Space(10);

            DoImportExportGUI();
	    }

        private void DoGlobalVariablesGUI()
        {
            EditorGUILayout.HelpBox(Strings.Hint_GlobalsInspector_Shows_DEFAULT_Values, MessageType.Info);

            GUILayout.Label(Strings.Command_Global_Variables, EditorStyles.boldLabel);

            if (variableList.Count > 0)
            {
                FsmVariable.DoVariableListGUI(variableList);
            }
            else
            {
                GUILayout.Label(Strings.Label_None_In_Table);
            }
        }

        private void DoGlobalEventsGUI()
        {
            GUILayout.Label(Strings.Label_Global_Events, EditorStyles.boldLabel);

            if (globals.Events.Count > 0)
            {
                foreach (var eventName in globals.Events)
                {
                    GUILayout.Label(eventName);
                }
            }
            else
            {
                GUILayout.Label(Strings.Label_None_In_Table);
            }
        }

        private static void DoImportExportGUI()
        {
            if (GUILayout.Button(Strings.Command_Export_Globals))
            {
                GlobalsAsset.Export();
            }

            if (GUILayout.Button(Strings.Command_Import_Globals))
            {
                GlobalsAsset.Import();
            }

            EditorGUILayout.HelpBox(Strings.Hint_Export_Globals_Notes, MessageType.None);
        }

        private void Refresh()
	    {
		    BuildVariableList();
		    Repaint();
	    }

        private void BuildVariableList()
	    {
		    variableList = FsmVariable.GetFsmVariableList(globals);
	    }
    }
}

