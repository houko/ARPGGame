// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the caret's blink rate of a UI InputField component.")]
	public class UiInputFieldGetCaretBlinkRate : ComponentAction<UnityEngine.UI.InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The caret's blink rate for the UI InputField component.")]
		public FsmFloat caretBlinkRate;

		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;
		
		private UnityEngine.UI.InputField inputField;
		
		public override void Reset()
		{
			caretBlinkRate = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				inputField = cachedComponent;
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
			if (inputField != null)
			{
				caretBlinkRate.Value = inputField.caretBlinkRate;
			}
		}
		
	}
}