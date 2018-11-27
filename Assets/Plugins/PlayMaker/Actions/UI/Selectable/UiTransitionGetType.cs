// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.


using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.UI)]
    [Tooltip("Gets the transition type of a UI Selectable component.")]
    public class UiTransitionGetType : ComponentAction<Selectable>
    {
        [RequiredField]
        [CheckForComponent(typeof(Selectable))]
        [Tooltip("The GameObject with the UI Selectable component.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("The transition value")] public FsmString transition;

        [Tooltip("Event sent if transition is ColorTint")]
        public FsmEvent colorTintEvent;

        [Tooltip("Event sent if transition is SpriteSwap")]
        public FsmEvent spriteSwapEvent;

        [Tooltip("Event sent if transition is Animation")]
        public FsmEvent animationEvent;

        [Tooltip("Event sent if transition is none")]
        public FsmEvent noTransitionEvent;

        private Selectable selectable;
        private Selectable.Transition originalTransition;


        public override void Reset()
        {
            gameObject = null;
            transition = null;

            colorTintEvent = null;
            spriteSwapEvent = null;
            animationEvent = null;
            noTransitionEvent = null;
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

            transition.Value = selectable.transition.ToString();

            if (selectable.transition == Selectable.Transition.None)
            {
                Fsm.Event(noTransitionEvent);
            }
            else if (selectable.transition == Selectable.Transition.ColorTint)
            {
                Fsm.Event(colorTintEvent);
            }
            else if (selectable.transition == Selectable.Transition.SpriteSwap)
            {
                Fsm.Event(spriteSwapEvent);
            }
            else if (selectable.transition == Selectable.Transition.Animation)
            {
                Fsm.Event(animationEvent);
            }
        }
    }
}