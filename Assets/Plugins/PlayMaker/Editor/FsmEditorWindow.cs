// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using HutongGames.Editor;
using HutongGames.PlayMaker;
using UnityEditor;
using UnityEngine;

/* NOTE: Wrapper no longer needed in Unity 4.x
 * BUT changing it breaks saved layouts
 * SO wrap in namespace instead (which is also now supported in 4.x)
 */

// EditorWindow classes can't be called from a dll 
// so create a thin wrapper class as a workaround

namespace HutongGames.PlayMakerEditor
{
    [System.Serializable]
    internal class FsmEditorWindow : BaseEditorWindow
    {
        /// <summary>
        /// Open the Fsm Editor and optionally show the Welcome Screen
        /// </summary>
        public static void OpenWindow()
        {
            OpenWindow<FsmEditorWindow>();
        }

        private static void OpenWindow<T>(string id = "Window") where T : EditorWindow
        {
            if (FsmEditorSettings.PingOpenEditorWindows)
            {
                var window = Resources.FindObjectsOfTypeAll<T>();
                if (window.Length > 0)
                {
                    HighlighterHelper.PingHighlight(typeof(T), id);
                }
            }

            GetWindow<T>();
        }

        /// <summary>
        /// Open the Fsm Editor and select an Fsm Component
        /// </summary>
        public static void OpenWindow(PlayMakerFSM fsmComponent)
        {
            OpenWindow();

            FsmEditor.SelectFsm(fsmComponent.Fsm);
        }

        /// <summary>
        /// Open the Fsm Editor and select an Fsm Component
        /// </summary>
        public static void OpenWindow(FsmTemplate fsmTemplate)
        {
            OpenWindow();

            FsmEditor.SelectFsm(fsmTemplate.fsm);
        }

        /// <summary>
        /// Is the Fsm Editor open?
        /// </summary>
        public static bool IsOpen()
        {
            return instance != null;
        }

        public static void OpenInEditor(PlayMakerFSM fsmComponent)
        {
            if (!IsOpen())
            {
                OpenWindow(fsmComponent);
            }
            else
            {
                FocusWindowIfItsOpen<FsmEditorWindow>();
                FsmEditor.SelectFsm(fsmComponent.FsmTemplate == null ? fsmComponent.Fsm : fsmComponent.FsmTemplate.fsm);
            }
        }

        public static void OpenInEditor(Fsm fsm)
        {
            if (fsm.Owner != null)
            {
                OpenInEditor(fsm.Owner as PlayMakerFSM);
            }
        }

        public static void OpenInEditor(GameObject go)
        {
            if (go != null)
            {
                OpenInEditor(FsmSelection.FindFsmOnGameObject(go));
            }
        }

        private static FsmEditorWindow instance;

        [SerializeField]
        private FsmEditor fsmEditor;

        // ReSharper disable UnusedMember.Local

        /// <summary>
        /// Delay initialization until first OnGUI to avoid interfering with runtime system initialization.
        /// </summary>
        public override void Initialize()
        {
            instance = this;

            if (fsmEditor == null)
            {
                fsmEditor = new FsmEditor();
            }

            fsmEditor.InitWindow(this);
            fsmEditor.OnEnable();
        }

        public override void InitWindowTitle()
        {
            SetTitle(Strings.ProductName);
        }

        protected override void DoUpdateHighlightIdentifiers()
        {
            // Not called? Need to investigate further...
            //fsmEditor.DoUpdateHighlightIdentifiers();
        }

        public override void DoGUI()
        {
            fsmEditor.OnGUI();

            /* Debug Repaint events
            if (Event.current.type == EventType.Repaint)
            {
                Debug.Log("Repaint");
            }*/

            if (Event.current.type == EventType.ValidateCommand)
            {
                switch (Event.current.commandName)
                {
                    case "UndoRedoPerformed":
                    case "Cut":
                    case "Copy":
                    case "Paste":
                    case "SelectAll":
                        Event.current.Use();
                        break;
                }
            }

            if (Event.current.type == EventType.ExecuteCommand)
            {
                switch (Event.current.commandName)
                {
                    /* replaced with Undo.undoRedoPerformed callback added in Unity 4.3
                    case "UndoRedoPerformed":
                        FsmEditor.UndoRedoPerformed();
                        break;
                    */

                    case "Cut":
                        FsmEditor.Cut();
                        break;

                    case "Copy":
                        FsmEditor.Copy();
                        break;

                    case "Paste":
                        FsmEditor.Paste();
                        break;

                    case "SelectAll":
                        FsmEditor.SelectAll();
                        break;

                    case "OpenWelcomeWindow":
                        OpenWindow<PlayMakerWelcomeWindow>();
                        break;

                    case "OpenToolWindow":
                        OpenWindow<ContextToolWindow>();
                        break;

                    case "OpenFsmSelectorWindow":
                        OpenWindow<FsmSelectorWindow>();
                        break;

                    case "OpenFsmTemplateWindow":
                        OpenWindow<FsmTemplateWindow>();
                        break;

                    case "OpenStateSelectorWindow":
                        OpenWindow<FsmStateWindow>();
                        break;

                    case "OpenActionWindow":
                        OpenWindow<FsmActionWindow>();
                        break;

                    case "OpenGlobalEventsWindow":
                        OpenWindow<FsmEventsWindow>();
                        break;

                    case "OpenGlobalVariablesWindow":
                        OpenWindow<FsmGlobalsWindow>();
                        break;

                    case "OpenErrorWindow":
                        OpenWindow<FsmErrorWindow>();
                        break;

                    case "OpenTimelineWindow":
                        OpenWindow<FsmTimelineWindow>();
                        break;

                    case "OpenFsmLogWindow":
                        OpenWindow<FsmLogWindow>();
                        break;

                    case "OpenAboutWindow":
                        OpenWindow<AboutWindow>();
                        break;

                    case "OpenReportWindow":
                        OpenWindow<ReportWindow>();
                        break;

                    case "AddFsmComponent":
                        PlayMakerMainMenu.AddFsmToSelected();
                        break;

                    case "RepaintAll":
                        RepaintAllWindows();
                        break;

                    case "ChangeLanguage":
                        ResetWindowTitles();
                        break;
                }

                GUIUtility.ExitGUI();
            }
        }

        // called when you change editor language
        public void ResetWindowTitles()
        {
            var windows = Resources.FindObjectsOfTypeAll<BaseEditorWindow>();
            foreach (var window in windows)
            {
                window.InitWindowTitle();
            }
        }

        public void RepaintAllWindows()
        {
            var windows = Resources.FindObjectsOfTypeAll<BaseEditorWindow>();
            foreach (var window in windows)
            {
                window.Repaint();
            }
        }

        private void Update()
        {
            if (Initialized && fsmEditor != null)
            {
                fsmEditor.Update();
            }
        }

        private void OnInspectorUpdate()
        {
            if (Initialized && fsmEditor != null)
            {
                fsmEditor.OnInspectorUpdate();
            }
        }

        private void OnFocus()
        {
            if (Initialized && fsmEditor != null)
            {
                fsmEditor.OnFocus();
            }
        }

        private void OnSelectionChange()
        {
            if (Initialized && fsmEditor != null)
            {
                fsmEditor.OnSelectionChange();
            }
        }

        private void OnHierarchyChange()
        {
            if (Initialized && fsmEditor != null)
            {
                fsmEditor.OnHierarchyChange();
            }
        }

        private void OnProjectChange()
        {
            if (Initialized && fsmEditor != null)
            {
                fsmEditor.OnProjectChange();
            }
        }

        private void OnDisable()
        {
            if (Initialized && fsmEditor != null)
            {
                fsmEditor.OnDisable();
            }

            HighlighterHelper.Reset(GetType());
            
            instance = null;
        }

        private void OnDestroy()
        {
            CloseAllWindowsThatNeedMainEditor();
        }

        // ReSharper restore UnusedMember.Local
    }



}
