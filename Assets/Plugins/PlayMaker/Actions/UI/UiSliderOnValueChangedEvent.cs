// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Catches onValueChanged event for a UI Slider component. Store the new value and/or send events. Event float data will contain the new slider value")]
	public class UiSliderOnValueChangedEvent : ComponentAction<UnityEngine.UI.Slider>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Slider))]
		[Tooltip("The GameObject with the UI Slider component.")]
		public FsmOwnerDefault gameObject;

	    [Tooltip("Where to send the event.")]
	    public FsmEventTarget eventTarget;

		[Tooltip("Send this event when Clicked.")]
		public FsmEvent sendEvent;

	    [Tooltip("Store the new value in float variable.")]
	    [UIHint(UIHint.Variable)]
	    public FsmFloat value;

		private UnityEngine.UI.Slider slider;
		
		public override void Reset()
		{
			gameObject = null;
		    eventTarget = FsmEventTarget.Self;
		    sendEvent = null;
		    value = null;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				slider = cachedComponent;
				if (slider != null)
				{
					slider.onValueChanged.AddListener(DoOnValueChanged);
				}
			}

		}

		public override void OnExit()
		{
			if (slider != null)
			{
				slider.onValueChanged.RemoveListener(DoOnValueChanged);
			}
		}

		public void DoOnValueChanged(float _value)
		{
		    value.Value = _value;

			Fsm.EventData.FloatData = _value;
			SendEvent(eventTarget, sendEvent);
		}
	}
}