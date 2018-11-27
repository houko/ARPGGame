// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine.EventSystems;

#if UNITY_5_6_OR_NEWER   
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnDrag is called on the GameObject. Warning this event is sent every frame while dragging." +
		"\n Use GetLastPointerDataInfo action to get info from the event.")]
	public class UiOnDragEvent : EventTriggerActionBase
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnDrag is called")]
		public FsmEvent onDragEvent;

	    public override void Reset()
	    {
	        base.Reset();
	        onDragEvent = null;
	    }
		
	    public override void OnEnter()
	    {
	        Init(EventTriggerType.Drag, OnDragDelegate);
	    }

	    private void OnDragDelegate( BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData) data;
			SendEvent(eventTarget, onDragEvent);
		}
	}
}

#endif