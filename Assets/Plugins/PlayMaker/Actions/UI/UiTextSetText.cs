// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the text value of a UI Text component.")]
	public class UiTextSetText : ComponentAction<UnityEngine.UI.Text>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Text))]
		[Tooltip("The GameObject with the UI Text component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.TextArea)]
		[Tooltip("The text of the UGui Text component.")]
		public FsmString text;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.Text uiText;
	    private string originalString;

		public override void Reset()
		{
			gameObject = null;
			text = null;
			resetOnExit = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (UpdateCache(go))
		    {
		        uiText = cachedComponent;
		    }

		    originalString = uiText.text;

			DoSetTextValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetTextValue();
		}

	    private void DoSetTextValue()
		{
		    if (uiText == null) return;

		    uiText.text = text.Value;
		}

		public override void OnExit()
		{
			if (uiText == null) return;
			
			if (resetOnExit.Value)
			{
				uiText.text = originalString;
			}
		}

#if UNITY_EDITOR
	    public override string AutoName()
	    {
	        return "UISetText : " + ActionHelpers.GetValueLabel(text);
	    }
#endif
	}
}