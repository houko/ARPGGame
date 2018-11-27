#if !PLAYMAKER_NO_UI

using HutongGames.PlayMaker.Actions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker
{
    [AddComponentMenu("PlayMaker/UI/UI Pointer Events")]
    public class PlayMakerUiPointerEvents : PlayMakerUiEventBase,
        IPointerClickHandler,
        IPointerDownHandler,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerUpHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            UiGetLastPointerDataInfo.lastPointerEventData = eventData;
            SendEvent(FsmEvent.UiPointerClick);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            UiGetLastPointerDataInfo.lastPointerEventData = eventData;
            SendEvent(FsmEvent.UiPointerDown);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            UiGetLastPointerDataInfo.lastPointerEventData = eventData;
            SendEvent(FsmEvent.UiPointerEnter);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UiGetLastPointerDataInfo.lastPointerEventData = eventData;
            SendEvent(FsmEvent.UiPointerExit);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            UiGetLastPointerDataInfo.lastPointerEventData = eventData;
            SendEvent(FsmEvent.UiPointerUp);
        }
    }
}

#endif