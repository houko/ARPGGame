// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.


using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the Color Block of a UI Selectable component.")]
	public class UiGetColorBlock : ComponentAction<Selectable>
	{
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the UI Selectable component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The fade duration value. Leave as None for no effect")]
		[UIHint(UIHint.Variable)]
		public FsmFloat fadeDuration;

		[Tooltip("The color multiplier value. Leave as None for no effect")]
		[UIHint(UIHint.Variable)]
		public FsmFloat colorMultiplier;
	
		[Tooltip("The normal color value. Leave as None for no effect")]
		[UIHint(UIHint.Variable)]
		public FsmColor normalColor;

		[Tooltip("The pressed color value. Leave as None for no effect")]
		[UIHint(UIHint.Variable)]
		public FsmColor pressedColor;

		[Tooltip("The highlighted color value. Leave as None for no effect")]
		[UIHint(UIHint.Variable)]
		public FsmColor highlightedColor;

		[Tooltip("The disabled color value. Leave as None for no effect")]
		[UIHint(UIHint.Variable)]
		public FsmColor disabledColor;

		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;

	    private Selectable selectable;
				
		
		public override void Reset()
		{
			gameObject = null;

			fadeDuration = null;
			colorMultiplier = null;
			normalColor = null;
			highlightedColor = null;
			pressedColor = null;
			disabledColor = null;

			everyFrame = false;
		}
		
		public override void OnEnter()
		{
		    var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (UpdateCache(go))
		    {
		        selectable = cachedComponent;
		    }
			
			DoGetValue();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetValue();
		}

	    private void DoGetValue()
		{
		    if (selectable == null) return;

			if (!colorMultiplier.IsNone)
			{
				colorMultiplier.Value = selectable.colors.colorMultiplier ;
			}
			if (!fadeDuration.IsNone)
			{
				fadeDuration.Value =selectable.colors.fadeDuration;
			}
			if (!normalColor.IsNone)
			{
				normalColor.Value = selectable.colors.normalColor;
			}
			if (!pressedColor.IsNone)
			{
				pressedColor.Value = selectable.colors.pressedColor;
			}
			if (!highlightedColor.IsNone)
			{
				highlightedColor.Value =selectable.colors.highlightedColor;
			}
			if (!disabledColor.IsNone)
			{
				disabledColor.Value = selectable.colors.disabledColor;
			}

		}

	}
}