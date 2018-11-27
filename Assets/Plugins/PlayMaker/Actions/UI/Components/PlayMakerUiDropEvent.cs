#if !PLAYMAKER_NO_UI

using HutongGames.PlayMaker.Actions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker
{
    [AddComponentMenu("PlayMaker/UI/UI Drop Event")]
    public class PlayMakerUiDropEvent : PlayMakerUiEventBase, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            UiGetLastPointerDataInfo.lastPointerEventData = eventData;
            SendEvent(FsmEvent.UiDrop);
        }
    }
}
#endif