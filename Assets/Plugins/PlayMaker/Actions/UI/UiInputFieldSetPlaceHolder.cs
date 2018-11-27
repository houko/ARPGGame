// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the placeholder of a UI InputField component. Optionally reset on exit")]
	public class UiInputFieldSetPlaceHolder : ComponentAction<UnityEngine.UI.InputField>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Graphic))]
		[Tooltip("The placeholder (any graphic UI Component) for the UI InputField component.")]
		public FsmGameObject placeholder;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		private UnityEngine.UI.InputField inputField;
	    private UnityEngine.UI.Graphic originalValue;

		public override void Reset()
		{
			gameObject = null;
			placeholder = null;
			resetOnExit = null;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				inputField = cachedComponent;
			}

			originalValue = inputField.placeholder;

			DoSetValue();
			
			Finish();
		}

	    private void DoSetValue()
		{
			if (inputField != null)
			{
				var placeHolderGo = placeholder.Value;
				if (placeHolderGo==null)
				{
					inputField.placeholder = null;
					return;
				}

				inputField.placeholder = placeHolderGo.GetComponent<UnityEngine.UI.Graphic>();
			}
		}

		public override void OnExit()
		{
			if (inputField == null) return;
			
			if (resetOnExit.Value)
			{
				inputField.placeholder = originalValue;
			}
		}
	}
}