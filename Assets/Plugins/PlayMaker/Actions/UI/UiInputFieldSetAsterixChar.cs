// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the Asterix Character of a UI InputField component.")]
	public class UiInputFieldSetAsterixChar : ComponentAction<UnityEngine.UI.InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The asterix Character used for password field type of the UGui InputField component. Only the first character will be used, the rest of the string will be ignored")]
		public FsmString asterixChar;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		private UnityEngine.UI.InputField inputField;

	    private char originalValue;
	    private static char __char__ = ' ';

		public override void Reset()
		{
			gameObject = null;
			asterixChar = "*";
			resetOnExit = null;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				inputField = cachedComponent;
			}

            originalValue = inputField.asteriskChar;

			DoSetValue();
			
			Finish();
		}

	    private void DoSetValue()
		{
			var _char = __char__;

			if (asterixChar.Value.Length>0)
			{
				_char = asterixChar.Value[0];
			}

			if (inputField != null)
			{
				inputField.asteriskChar = _char;
			}
		}

		public override void OnExit()
		{
			if (inputField == null) return;
			
			if (resetOnExit.Value)
			{
				inputField.asteriskChar = originalValue;
			}
		}
	}
}