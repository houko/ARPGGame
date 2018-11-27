// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine.EventSystems;

#if UNITY_5_6_OR_NEWER

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnCancel is called on the GameObject." +
		"\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnCancelEvent : EventTriggerActionBase
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnCancelEvent is called")]
		public FsmEvent onCancelEvent;

		public override void Reset()
		{
		    gameObject = null;
			onCancelEvent = null;
		}
		
	    public override void OnEnter()
	    {
	        Init(EventTriggerType.Cancel, OnCancelDelegate);
	    }

	    private void OnCancelDelegate( BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData) data;
			SendEvent(eventTarget, onCancelEvent);
		}
	}
}

#endif