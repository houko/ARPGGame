// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Activate a UI InputField component to begin processing Events. Optionally Deactivate on state exit")]
	public class UiInputFieldActivate : ComponentAction<UnityEngine.UI.InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool deactivateOnExit;

		private UnityEngine.UI.InputField inputField;

		public override void Reset()
		{
			gameObject = null;
			deactivateOnExit = null;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				inputField = cachedComponent;
			}

			DoAction();
			
			Finish();
		}

	    private void DoAction()
		{
			if (inputField != null)
			{
				inputField.ActivateInputField();
			}
		}

		public override void OnExit()
		{
		    if (inputField == null) return;
			
			if (deactivateOnExit.Value)
			{
				inputField.DeactivateInputField();
			}
		}

	}
}