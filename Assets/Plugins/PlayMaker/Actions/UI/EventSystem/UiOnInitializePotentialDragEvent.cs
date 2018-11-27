// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine.EventSystems;

#if UNITY_5_6_OR_NEWER   
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when Called by the EventSystem when a drag has been found, but before it is valid to begin the drag." +
		"\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnInitializePotentialDragEvent : EventTriggerActionBase
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnInitializePotentialDrag is called")]
		public FsmEvent onInitializePotentialDragEvent;

	    public override void Reset()
	    {
	        base.Reset();
	        onInitializePotentialDragEvent = null;
	    }
		
	    public override void OnEnter()
	    {
	        Init(EventTriggerType.InitializePotentialDrag, OnInitializePotentialDragDelegate);
	    }


	    private void OnInitializePotentialDragDelegate( BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData) data;
			SendEvent(eventTarget, onInitializePotentialDragEvent);
		}
	}
}

#endif