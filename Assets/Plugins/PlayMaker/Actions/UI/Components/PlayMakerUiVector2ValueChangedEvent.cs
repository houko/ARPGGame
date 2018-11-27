#if !PLAYMAKER_NO_UI

using UnityEngine.UI;
using UnityEngine;

namespace HutongGames.PlayMaker
{
    [AddComponentMenu("PlayMaker/UI/UI Vector2 Value Changed Event")]
    public class PlayMakerUiVector2ValueChangedEvent : PlayMakerUiEventBase
    {
        public ScrollRect scrollRect;

        protected override void Initialize()
        {
            if (initialized) return;
            initialized = true;

            if (scrollRect == null)
            {
                scrollRect = GetComponent<ScrollRect>();
            }

            if (scrollRect != null)
            {
                scrollRect.onValueChanged.AddListener(OnValueChanged);
            }
        }

        protected void OnDisable()
        {
            initialized = false;

            if (scrollRect != null)
            {
                scrollRect.onValueChanged.RemoveListener(OnValueChanged);
            }
        }

        private void OnValueChanged(Vector2 value)
        {
            Fsm.EventData.Vector2Data = value;
            SendEvent(FsmEvent.UiVector2ValueChanged);
        }

    }
}

#endif