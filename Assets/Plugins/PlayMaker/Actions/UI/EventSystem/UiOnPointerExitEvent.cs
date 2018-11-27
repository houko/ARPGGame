// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine.EventSystems;

#if UNITY_5_6_OR_NEWER   
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnPointerExit is called on the GameObject." +
		"\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnPointerExitEvent : EventTriggerActionBase
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when PointerExit is called")]
		public FsmEvent onPointerExitEvent;

	    public override void Reset()
	    {
	        base.Reset();
	        onPointerExitEvent = null;
	    }
		
	    public override void OnEnter()
	    {
	        Init(EventTriggerType.PointerExit, OnPointerExitDelegate);
	    }


	    private void OnPointerExitDelegate( BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData) data;
			SendEvent(eventTarget, onPointerExitEvent);
		}
	}
}

#endif