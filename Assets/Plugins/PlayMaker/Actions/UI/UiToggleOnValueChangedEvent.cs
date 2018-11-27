// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Catches onValueChanged event in a UI Toggle component. Store the new value and/or send events. Event bool data will contain the new Toggle value")]
	public class UiToggleOnValueChangedEvent : ComponentAction<UnityEngine.UI.Toggle>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Toggle))]
		[Tooltip("The GameObject with the UI Toggle component.")]
		public FsmOwnerDefault gameObject;

	    [Tooltip("Where to send the event.")]
	    public FsmEventTarget eventTarget;

		[Tooltip("Send this event when the value changes.")]
		public FsmEvent sendEvent;

	    [Tooltip("Store the new value in bool variable.")]
	    [UIHint(UIHint.Variable)]
	    public FsmBool value;

		private UnityEngine.UI.Toggle toggle;
		
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
			    if (toggle != null)
			    {
                    toggle.onValueChanged.RemoveListener(DoOnValueChanged);
			    }

				toggle = cachedComponent;

				if (toggle != null)
				{
					toggle.onValueChanged.AddListener(DoOnValueChanged);
				}
				else
				{
					LogError("Missing UI.Toggle on " + go.name);
				}
			}
			else
			{
				LogError("Missing GameObject");
			}

		}

		public override void OnExit()
		{
			if (toggle != null)
			{
				toggle.onValueChanged.RemoveListener(DoOnValueChanged);
			}
		}

		public void DoOnValueChanged(bool _value)
		{
		    value.Value = _value;

			Fsm.EventData.BoolData = _value;
			SendEvent(eventTarget, sendEvent);
		}
	}
}