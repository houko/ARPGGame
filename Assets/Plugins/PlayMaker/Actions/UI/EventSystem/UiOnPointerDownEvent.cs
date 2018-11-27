// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine.EventSystems;

#if UNITY_5_6_OR_NEWER

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.UI)]
    [Tooltip("Sends event when OnPointerDown is called on the GameObject." +
             "\n Use GetLastPointerDataInfo action to get info from the event")]
    public class UiOnPointerDownEvent : EventTriggerActionBase
    {
        [UIHint(UIHint.Variable)] 
        [Tooltip("Event sent when PointerDown is called")]
        public FsmEvent onPointerDownEvent;

        public override void Reset()
        {
            base.Reset();
            onPointerDownEvent = null;
        }
		
        public override void OnEnter()
        {
            Init(EventTriggerType.PointerDown, OnPointerDownDelegate);
        }

        private void OnPointerDownDelegate(BaseEventData data)
        {
            UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData) data;
            SendEvent(eventTarget, onPointerDownEvent);
        }
    }
}

#endif