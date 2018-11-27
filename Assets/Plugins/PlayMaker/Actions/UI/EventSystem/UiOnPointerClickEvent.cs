// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine.EventSystems;

#if UNITY_5_6_OR_NEWER   
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnPointerClick is called on the GameObject." +
		"\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnPointerClickEvent : EventTriggerActionBase
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when PointerClick is called")]
		public FsmEvent onPointerClickEvent;

	    public override void Reset()
	    {
	        base.Reset();
	        onPointerClickEvent = null;
	    }
		
	    public override void OnEnter()
	    {
	        Init(EventTriggerType.PointerClick, OnPointerClickDelegate);
	    }


	    private void OnPointerClickDelegate( BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData) data;
			SendEvent(eventTarget, onPointerClickEvent);
		}
	}
}

#endif