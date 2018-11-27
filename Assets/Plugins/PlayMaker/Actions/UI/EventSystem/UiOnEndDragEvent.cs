// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine.EventSystems;

#if UNITY_5_6_OR_NEWER   
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event Called by the EventSystem once dragging ends." +
		"\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnEndDragEvent : EventTriggerActionBase
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnEndDrag is called")]
		public FsmEvent onEndDragEvent;

	    public override void Reset()
	    {
	        base.Reset();
	        onEndDragEvent = null;
	    }
		
	    public override void OnEnter()
	    {
	        Init(EventTriggerType.EndDrag, OnEndDragDelegate);
	    }


	    private void OnEndDragDelegate( BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData) data;
			SendEvent(eventTarget, onEndDragEvent);
		}
	}
}

#endif