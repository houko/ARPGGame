// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.


using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.UI)]
    [Tooltip("Gets the interactable flag of a UI Selectable component.")]
    public class UiGetIsInteractable : ComponentAction<Selectable>
    {
        [RequiredField]
        [CheckForComponent(typeof(Selectable))]
        [Tooltip("The GameObject with the UI Selectable component.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("The Interactable value")] [UIHint(UIHint.Variable)]
        public FsmBool isInteractable;

        [Tooltip("Event sent if Component is Interactable")]
        public FsmEvent isInteractableEvent;

        [Tooltip("Event sent if Component is not Interactable")]
        public FsmEvent isNotInteractableEvent;

        private Selectable selectable;
        private bool originalState;


        public override void Reset()
        {
            gameObject = null;
            isInteractable = null;
            isInteractableEvent = null;
            isNotInteractableEvent = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                selectable = cachedComponent;
            }

            DoGetValue();

            Finish();
        }

        private void DoGetValue()
        {
            if (selectable == null)
            {
                return;
            }

            var _flag = selectable.IsInteractable();
            isInteractable.Value = _flag;

            if (_flag)
            {
                Fsm.Event(isInteractableEvent);
            }
            else
            {
                Fsm.Event(isNotInteractableEvent);
            }
        }
    }
}