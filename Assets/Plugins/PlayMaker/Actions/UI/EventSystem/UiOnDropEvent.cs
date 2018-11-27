// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine.EventSystems;

#if UNITY_5_6_OR_NEWER 

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnDrop is called on the GameObject. Warning this event is sent everyframe while dragging." +
		"\n Use GetLastPointerDataInfo action to get info from the event.")]
	public class UiOnDropEvent : EventTriggerActionBase
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnDrop is called")]
		public FsmEvent onDropEvent;

	    public override void Reset()
	    {
	        base.Reset();
	        onDropEvent = null;
	    }
		
	    public override void OnEnter()
	    {
	        Init(EventTriggerType.Drop, OnDropDelegate);
	    }


	    private void OnDropDelegate( BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData) data;
			SendEvent(eventTarget, onDropEvent);
		}
	}
}

#endif