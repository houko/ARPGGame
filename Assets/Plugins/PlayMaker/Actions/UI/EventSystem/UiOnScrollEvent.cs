// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine.EventSystems;

#if UNITY_5_6_OR_NEWER   

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnScroll is called on the GameObject." +
		"\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnScrollEvent : EventTriggerActionBase
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnScroll is called")]
		public FsmEvent onScrollEvent;

	    public override void Reset()
	    {
	        base.Reset();
	        onScrollEvent = null;
	    }
		
	    public override void OnEnter()
	    {
	        Init(EventTriggerType.Scroll, OnScrollDelegate);
	    }

	    private void OnScrollDelegate( BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData) data;
			SendEvent(eventTarget, onScrollEvent);
		}
	}
}

#endif