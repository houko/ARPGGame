//-----------------------------------------------------------------------
// <copyright file="FsmComponentInspector.cs" company="Hutong Games LLC">
// Copyright (c) Hutong Games LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using UnityEditor;
using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMakerEditor
{
    /// <summary>
    /// Custom inspector for PlayMakerFSM
    /// </summary>
    [CustomEditor(typeof(PlayMakerFSM))]
    public class FsmComponentInspector : UnityEditor.Editor
    {
        private PlayMakerFSM fsmComponent;
        private FsmInspector fsmInspector;

        public void OnEnable()
        {
            fsmComponent = (PlayMakerFSM) target;
            fsmInspector = new FsmInspector(fsmComponent.Fsm) {UnityInspectorMode = true};
            fsmInspector.EditButtonAction += () => { FsmEditorWindow.OpenInEditor(fsmComponent); };

            FsmEditor.OnFsmChanged += CheckRefresh;
        }

        public void OnDisable()
        {
            FsmEditor.OnFsmChanged -= CheckRefresh;
        }

        public override void OnInspectorGUI()
        {
            fsmInspector.OnGUI();
        }

        private void CheckRefresh(Fsm fsm)
        {
            if (fsm == fsmComponent.Fsm)
            {
                fsmInspector.Reset();
            }
        }

        /// <summary>
        /// Actions can use OnSceneGUI to display interactive gizmos
        /// </summary>
        public void OnSceneGUI()
        {
            FsmEditor.OnSceneGUI();
        }

        // These should be in FsmEditor, but keeping here for backward compatibility
        // Some third party tools may be using them...

        /// <summary>
        /// Open the specified FSM in the Playmaker Editor
        /// </summary>
        [Obsolete("Use FsmEditorWindow.OpenInEditor instead")]
        public static void OpenInEditor(PlayMakerFSM fsmComponent)
        {
            FsmEditorWindow.OpenInEditor(fsmComponent);
        }

        /// <summary>
        /// Open the specified FSM in the Playmaker Editor
        /// </summary>
        [Obsolete("Use FsmEditorWindow.OpenInEditor instead")]
        public static void OpenInEditor(Fsm fsm)
        {
            FsmEditorWindow.OpenInEditor(fsm.Owner as PlayMakerFSM);
        }

        /// <summary>
        /// Open the first PlayMakerFSM on a GameObject in the Playmaker Editor
        /// </summary>
        [Obsolete("Use FsmEditorWindow.OpenInEditor instead")]
        public static void OpenInEditor(GameObject go)
        {
            FsmEditorWindow.OpenInEditor(FsmSelection.FindFsmOnGameObject(go));
        }

    }
}


