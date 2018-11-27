#if !PLAYMAKER_NO_UI

using HutongGames.PlayMaker.Actions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker
{
    [AddComponentMenu("PlayMaker/UI/UI Drag Events")]
    public class PlayMakerUiDragEvents : PlayMakerUiEventBase, 
        IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        {
            UiGetLastPointerDataInfo.lastPointerEventData = eventData;
            SendEvent(FsmEvent.UiBeginDrag);
        }

        public void OnDrag(PointerEventData eventData)
        {
            UiGetLastPointerDataInfo.lastPointerEventData = eventData;
            SendEvent(FsmEvent.UiDrag);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            UiGetLastPointerDataInfo.lastPointerEventData = eventData;
            SendEvent(FsmEvent.UiEndDrag);
        }
    }
}

#endif