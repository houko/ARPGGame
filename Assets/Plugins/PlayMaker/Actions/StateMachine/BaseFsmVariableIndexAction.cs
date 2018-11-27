using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    /// <summary>
    /// Base class for Get/Set FSM Variable actions
    /// </summary>
    [ActionCategory(ActionCategory.StateMachine)]
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    public abstract class BaseFsmVariableIndexAction : FsmStateAction
    {
        [ActionSection("Events")]

        [Tooltip("The event to trigger if the index is out of range")]
        public FsmEvent indexOutOfRange;

        [Tooltip("The event to send if the FSM is not found.")]
        public FsmEvent fsmNotFound;

        [Tooltip("The event to send if the Variable is not found.")]
        public FsmEvent variableNotFound;

        private GameObject cachedGameObject;
        private string cachedFsmName;

        protected PlayMakerFSM fsm;

        public override void Reset()
        {
            fsmNotFound = null;
            variableNotFound = null;
        }

        protected bool UpdateCache(GameObject go, string fsmName)
        {
            if (go == null)
            {
                return false;
            }

            if (fsm == null || cachedGameObject != go || cachedFsmName != fsmName)
            {
                fsm = ActionHelpers.GetGameObjectFsm(go, fsmName);
                cachedGameObject = go;
                cachedFsmName = fsmName;

                if (fsm == null)
                {
                    LogWarning("Could not find FSM: " + fsmName);
                    Fsm.Event(fsmNotFound);
                }
            }

            return true;
        }

        protected void DoVariableNotFound(string variableName)
        {
            LogWarning("Could not find variable: " + variableName);
            Fsm.Event(variableNotFound);
        }
    }
}
