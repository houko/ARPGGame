// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the Character Limit value of a UI InputField component. This is the maximum number of characters that the user can type into the field.")]
	public class UiInputFieldGetCharacterLimit : ComponentAction<UnityEngine.UI.InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The maximum number of characters that the user can type into the UGui InputField component.")]
		public FsmInt characterLimit;

		[Tooltip("Event sent if limit is infinite (equal to 0)")]
		public FsmEvent hasNoLimitEvent;

		[Tooltip("Event sent if limit is more than 0")]
		public FsmEvent isLimitedEvent;

		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;
		
		private UnityEngine.UI.InputField inputField;
		
		public override void Reset()
		{
			characterLimit = null;
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
		    if (inputField == null) return;

		    characterLimit.Value = inputField.characterLimit;

		    Fsm.Event(inputField.characterLimit > 0 ? 
		        isLimitedEvent : hasNoLimitEvent);
		}
		
	}
}