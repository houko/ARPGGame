// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine.EventSystems;

#if UNITY_5_6_OR_NEWER   

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnDeselect is called on the GameObject." +
		"\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnDeselectEvent : EventTriggerActionBase
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnDeselectEvent is called")]
		public FsmEvent onDeselectEvent;

	    public override void Reset()
	    {
	        base.Reset();
	        onDeselectEvent = null;
	    }
		
	    public override void OnEnter()
	    {
	        Init(EventTriggerType.Deselect, OnDeselectDelegate);
	    }

	    private void OnDeselectDelegate( BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData) data;
			SendEvent(eventTarget, onDeselectEvent);
		}
	}
}

#endif