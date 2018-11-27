// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the text value of a UI Text component.")]
	public class UiTextGetText : ComponentAction<UnityEngine.UI.Text>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Text))]
		[Tooltip("The GameObject with the UI Text component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The text value of the UGui Text component.")]
		public FsmString text;

		[Tooltip("Runs every frame. Useful to animate values over time.")]
		public bool everyFrame;
		
		private UnityEngine.UI.Text uiText;
		
		public override void Reset()
		{
			text = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{ 	
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				uiText = cachedComponent;
			}
			
			DoGetTextValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoGetTextValue();
		}

	    private void DoGetTextValue()
		{
			if (uiText!=null)
			{
				text.Value = uiText.text;
			}
		}
		
	}
}