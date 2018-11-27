// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Catches UI Scrollbar onValueChanged event. Store the new value and/or send events. Event float data will contain the new Scrollbar value")]
	public class UiScrollbarOnValueChanged : ComponentAction<UnityEngine.UI.Scrollbar>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Scrollbar))]
		[Tooltip("The GameObject with the UI Scrollbar component.")]
		public FsmOwnerDefault gameObject;
      
	    [Tooltip("Where to send the event.")]
	    public FsmEventTarget eventTarget;

		[Tooltip("Send this event when the UI Scrollbar value changes.")]
		public FsmEvent sendEvent;

	    [Tooltip("Store new value in float variable.")]
	    [UIHint(UIHint.Variable)]
	    public FsmFloat value;

		private UnityEngine.UI.Scrollbar scrollbar;
		
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
				scrollbar = cachedComponent;
				if (scrollbar != null)
				{
					scrollbar.onValueChanged.AddListener(DoOnValueChanged);
				}
			}

		}

		public override void OnExit()
		{
			if (scrollbar != null)
			{
				scrollbar.onValueChanged.RemoveListener(DoOnValueChanged);
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