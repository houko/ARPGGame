// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the isOn property of a UI Toggle component.")]
	public class UiToggleSetIsOn : ComponentAction<UnityEngine.UI.Toggle>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Toggle))]
		[Tooltip("The GameObject with the UI Toggle component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Should the toggle be on?")]
		public FsmBool isOn;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		private UnityEngine.UI.Toggle _toggle;

	    private bool _originalValue;

		public override void Reset()
		{
			gameObject = null;
			isOn = null;
			resetOnExit = null;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				_toggle = cachedComponent;
			}

			DoSetValue();

		    Finish();
		}

	    private void DoSetValue()
		{
			if (_toggle!=null)
			{
			    _originalValue = _toggle.isOn;
				_toggle.isOn = isOn.Value;
			}
		}

		public override void OnExit()
		{
			if (_toggle==null) return;
						
			if (resetOnExit.Value)
			{
				_toggle.isOn = _originalValue;
			}
		}
	}
}