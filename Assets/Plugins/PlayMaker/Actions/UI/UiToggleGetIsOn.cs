// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the isOn value of a UI Toggle component. Optionally send events")]
	public class UiToggleGetIsOn : ComponentAction<UnityEngine.UI.Toggle>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Toggle))]
		[Tooltip("The GameObject with the UI Toggle component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("The isOn Value of the UI Toggle component.")]
		public FsmBool value;

		[Tooltip("Event sent when isOn Value is true.")]
		public FsmEvent isOnEvent;

		[Tooltip("Event sent when isOn Value is false.")]
		public FsmEvent isOffEvent;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.Toggle _toggle;

		public override void Reset()
		{
			gameObject = null;
			value = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{

			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
			    _toggle = cachedComponent;
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
		    if (_toggle == null) return;

		    value.Value = _toggle.isOn;

		    Fsm.Event(_toggle.isOn ? isOnEvent : isOffEvent);
		}
	}
}