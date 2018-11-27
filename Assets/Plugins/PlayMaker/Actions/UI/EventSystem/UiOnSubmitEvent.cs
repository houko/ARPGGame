// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine.EventSystems;

#if UNITY_5_6_OR_NEWER   
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnSubmit is called on the GameObject." +
		"\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnSubmitEvent : EventTriggerActionBase
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnSubmitEvent is called")]
		public FsmEvent onSubmitEvent;

	    public override void Reset()
	    {
	        base.Reset();
	        onSubmitEvent = null;
	    }
		
	    public override void OnEnter()
	    {
	        Init(EventTriggerType.Submit, OnSubmitDelegate);
	    }


	    private void OnSubmitDelegate( BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData) data;
			SendEvent(eventTarget, onSubmitEvent);
		}
	}
}

#endif