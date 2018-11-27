// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the selection color of a UI InputField component. This is the color of the highlighter to show what characters are selected")]
	public class UiInputFieldGetSelectionColor : ComponentAction<UnityEngine.UI.InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("This is the color of the highlighter to show what characters are selected of the UGui InputField component.")]
		public FsmColor selectionColor;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;
		
		private UnityEngine.UI.InputField inputField;
		
		public override void Reset()
		{
			selectionColor = null;
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
				selectionColor.Value = inputField.selectionColor;
			}
		}
		
	}
}