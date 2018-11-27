// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.

using UnityEngine.EventSystems;

#if UNITY_5_6_OR_NEWER   

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when Called by the EventSystem when a Select event occurs." +
		"\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnSelectEvent : EventTriggerActionBase
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnSelect is called")]
		public FsmEvent onSelectEvent;

	    public override void Reset()
	    {
	        base.Reset();
	        onSelectEvent = null;
	    }
		
	    public override void OnEnter()
	    {
	        Init(EventTriggerType.Select, OnSelectDelegate);
	    }

	    private void OnSelectDelegate( BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData) data;
			SendEvent(eventTarget, onSelectEvent);
		}
	}
}

#endif