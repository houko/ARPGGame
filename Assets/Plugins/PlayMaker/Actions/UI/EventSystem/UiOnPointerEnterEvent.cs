// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine.EventSystems;

#if UNITY_5_6_OR_NEWER   
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnPointerEnter is called on the GameObject." +
		"\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnPointerEnterEvent : EventTriggerActionBase
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when PointerEnter is called")]
		public FsmEvent onPointerEnterEvent;

	    public override void Reset()
	    {
	        base.Reset();
	        onPointerEnterEvent = null;
	    }
		
	    public override void OnEnter()
	    {
	        Init(EventTriggerType.PointerEnter, OnPointerEnterDelegate);
	    }


	    private void OnPointerEnterDelegate( BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData) data;
			SendEvent(eventTarget, onPointerEnterEvent);
		}
	}
}

#endif