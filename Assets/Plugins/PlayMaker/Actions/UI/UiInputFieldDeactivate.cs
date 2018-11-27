// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Deactivate a UI InputField to stop the processing of Events and send OnSubmit if not canceled. Optionally Activate on state exit")]
	public class UiInputFieldDeactivate : ComponentAction<UnityEngine.UI.InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Activate when exiting this state.")]
		public FsmBool activateOnExit;

		private UnityEngine.UI.InputField inputField;

		public override void Reset()
		{
			gameObject = null;
			activateOnExit = null;
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
				inputField.DeactivateInputField();
			}
		}

		public override void OnExit()
		{
			if (inputField == null) return;
			
			if (activateOnExit.Value)
			{
				inputField.ActivateInputField();
			}
		}

	}
}