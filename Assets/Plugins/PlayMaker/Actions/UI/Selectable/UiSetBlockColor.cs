// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.


using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.UI)]
    [Tooltip("Sets the Color Block of a UI Selectable component. " +
             "Modifications will not be visible if transition is not ColorTint")]
    public class UiSetColorBlock : ComponentAction<Selectable>
    {
        [RequiredField]
        [CheckForComponent(typeof(Selectable))]
        [Tooltip("The GameObject with the UI Selectable component.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("The fade duration value. Leave as None for no effect")]
        public FsmFloat fadeDuration;

        [Tooltip("The color multiplier value. Leave as None for no effect")]
        public FsmFloat colorMultiplier;

        [Tooltip("The normal color value. Leave as None for no effect")]
        public FsmColor normalColor;

        [Tooltip("The pressed color value. Leave as None for no effect")]
        public FsmColor pressedColor;

        [Tooltip("The highlighted color value. Leave as None for no effect")]
        public FsmColor highlightedColor;

        [Tooltip("The disabled color value. Leave as None for no effect")]
        public FsmColor disabledColor;


        [Tooltip("Reset when exiting this state.")]
        public FsmBool resetOnExit;

        [Tooltip("Repeats every frame, useful for animation")]
        public bool everyFrame;

        private Selectable selectable;
        private ColorBlock _colorBlock;
        private ColorBlock originalColorBlock;


        public override void Reset()
        {
            gameObject = null;

            fadeDuration = new FsmFloat {UseVariable = true};
            colorMultiplier = new FsmFloat {UseVariable = true};
            normalColor = new FsmColor {UseVariable = true};
            highlightedColor = new FsmColor {UseVariable = true};
            pressedColor = new FsmColor {UseVariable = true};
            disabledColor = new FsmColor {UseVariable = true};

            resetOnExit = null;
            everyFrame = false;
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
                originalColorBlock = selectable.colors;
            }

            DoSetValue();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoSetValue();
        }

        private void DoSetValue()
        {
            if (selectable == null)
            {
                return;
            }

            _colorBlock = selectable.colors;
            if (!colorMultiplier.IsNone)
            {
                _colorBlock.colorMultiplier = colorMultiplier.Value;
            }

            if (!fadeDuration.IsNone)
            {
                _colorBlock.fadeDuration = fadeDuration.Value;
            }

            if (!normalColor.IsNone)
            {
                _colorBlock.normalColor = normalColor.Value;
            }

            if (!pressedColor.IsNone)
            {
                _colorBlock.pressedColor = pressedColor.Value;
            }

            if (!highlightedColor.IsNone)
            {
                _colorBlock.highlightedColor = highlightedColor.Value;
            }

            if (!disabledColor.IsNone)
            {
                _colorBlock.disabledColor = disabledColor.Value;
            }

            selectable.colors = _colorBlock;
        }

        public override void OnExit()
        {
            if (selectable == null)
            {
                return;
            }

            if (resetOnExit.Value)
            {
                selectable.colors = originalColorBlock;
            }
        }
    }
}