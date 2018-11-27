// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.UI)]
    [Tooltip("Sets the explicit navigation properties of a UI Selectable component. " +
             "Note that it will have no effect until Navigation mode is set to 'Explicit'.")]
    public class UiNavigationExplicitSetProperties : ComponentAction<Selectable>
    {
        [RequiredField]
        [CheckForComponent(typeof(Selectable))]
        [Tooltip("The GameObject with the UI Selectable component.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("The down Selectable. Leave as None for no effect")] [CheckForComponent(typeof(Selectable))]
        public FsmGameObject selectOnDown;

        [Tooltip("The up Selectable.  Leave as None for no effect")] [CheckForComponent(typeof(Selectable))]
        public FsmGameObject selectOnUp;

        [Tooltip("The left Selectable.  Leave as None for no effect")] [CheckForComponent(typeof(Selectable))]
        public FsmGameObject selectOnLeft;

        [Tooltip("The right Selectable.  Leave as None for no effect")] [CheckForComponent(typeof(Selectable))]
        public FsmGameObject selectOnRight;

        [Tooltip("Reset when exiting this state.")]
        public FsmBool resetOnExit;

        private Selectable selectable;
        private Navigation navigation;
        private Navigation originalState;


        public override void Reset()
        {
            gameObject = null;
            selectOnDown = new FsmGameObject {UseVariable = true};
            selectOnUp = new FsmGameObject {UseVariable = true};
            selectOnLeft = new FsmGameObject {UseVariable = true};
            selectOnRight = new FsmGameObject {UseVariable = true};

            resetOnExit = false;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                selectable = cachedComponent;
            }

            if (selectable != null && resetOnExit.Value)
            {
                originalState = selectable.navigation;
            }

            DoSetValue();


            Finish();
        }

        private void DoSetValue()
        {
            if (selectable == null) return;

            navigation = selectable.navigation;

            if (!selectOnDown.IsNone)
            {
                navigation.selectOnDown = GetComponentFromFsmGameObject<Selectable>(selectOnDown);
            }

            if (!selectOnUp.IsNone)
            {
                navigation.selectOnUp = GetComponentFromFsmGameObject<Selectable>(selectOnUp);
            }

            if (!selectOnLeft.IsNone)
            {
                navigation.selectOnLeft = GetComponentFromFsmGameObject<Selectable>(selectOnLeft);
            }

            if (!selectOnRight.IsNone)
            {
                navigation.selectOnRight = GetComponentFromFsmGameObject<Selectable>(selectOnRight);
            }

            selectable.navigation = navigation;
        }

        public override void OnExit()
        {
            if (selectable == null) return;

            if (resetOnExit.Value)
            {
                navigation = selectable.navigation;
                navigation.selectOnDown = originalState.selectOnDown;
                navigation.selectOnLeft = originalState.selectOnLeft;
                navigation.selectOnRight = originalState.selectOnRight;
                navigation.selectOnUp = originalState.selectOnUp;

                selectable.navigation = navigation;
            }
        }

        private static T GetComponentFromFsmGameObject<T>(FsmGameObject variable) where T : Component
        {
            if (variable.Value != null)
            {
                return variable.Value.GetComponent<T>();
            }

            return null;
        }
    }
}