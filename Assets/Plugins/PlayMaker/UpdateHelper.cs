// Small helper class to allow Fsm to call SetDirty
// Fsm is inside dll so cannot use #if UNITY_EDITOR

#if (UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7)
#define UNITY_PRE_5_0
#endif

#if (UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2)
#define UNITY_PRE_5_3
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace HutongGames.PlayMaker
{
    /// <summary>
    /// Playmaker runtime code can't call unity editor code
    /// This class is a workaround, allowing runtime code to call EditorUtility.SetDirty
    /// </summary>
    public class UpdateHelper
    {
        private static bool editorPrefLoaded;

        // loading editorprefs can be slow (?) so cache setting
        private static bool _doLog;
        private static bool doLog
        {
            get
            {
#if UNITY_EDITOR
                if (!editorPrefLoaded)
                {
                    // set by FsmEditorSettings
                    _doLog = EditorPrefs.GetBool("PlayMaker.LogFsmUpdatedMessages", false);
                    editorPrefLoaded = true;
                }
#endif

                return _doLog;
            }
        }

        /// <summary>
        /// Helper that can be called by reflection from runtime class without referencing UnityEditor
        /// E.g. When Fsm is loaded it can need fixing and then needs to be marked dirty
        /// </summary>
        public static void SetDirty(Fsm fsm)
        {
#if UNITY_EDITOR

            // Unity 5.3.2 disallows scene dirty calls when playing
            if (Application.isPlaying) return;

            if (fsm == null || fsm.OwnerObject == null) return;

            if (doLog) 
            {
                Debug.Log("FSM Updated: " + FsmUtility.GetFullFsmLabel(fsm) + "\nPlease re-save the scene/project.", fsm.OwnerObject);
            }

            fsm.Preprocessed = false; // force pre-process to run again

            if (fsm.UsedInTemplate != null)
            {
                UnityEditor.EditorUtility.SetDirty(fsm.UsedInTemplate);
            }
            else if (fsm.Owner != null)
            {
                UnityEditor.EditorUtility.SetDirty(fsm.Owner);
#if !UNITY_PRE_5_3
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(fsm.Owner.gameObject.scene);
#elif !UNITY_PRE_5_0
                // Not sure if we need to do this...?
                UnityEditor.EditorApplication.MarkSceneDirty();
#endif
            }
#endif
        }
    }
}
