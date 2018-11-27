#if !PLAYMAKER_NO_UI

using UnityEngine.UI;
using UnityEngine;

namespace HutongGames.PlayMaker
{
    [AddComponentMenu("PlayMaker/UI/UI Float Value Changed Event")]
    public class PlayMakerUiFloatValueChangedEvent : PlayMakerUiEventBase
    {
        public Slider slider;
        public Scrollbar scrollbar;

        protected override void Initialize()
        {
            if (initialized) return;
            initialized = true;

            if (slider == null)
            {
                slider = GetComponent<Slider>();
            }

            if (slider != null)
            {
                slider.onValueChanged.AddListener(OnValueChanged);
            }

            if (scrollbar == null)
            {
                scrollbar = GetComponent<Scrollbar>();
            }

            if (scrollbar != null)
            {
                scrollbar.onValueChanged.AddListener(OnValueChanged);
            }
        }

        protected void OnDisable()
        {
            initialized = false;

            if (slider != null)
            {
                slider.onValueChanged.RemoveListener(OnValueChanged);
            }

            if (scrollbar != null)
            {
                scrollbar.onValueChanged.RemoveListener(OnValueChanged);
            }
        }

        private void OnValueChanged(float value)
        {
            Fsm.EventData.FloatData = value;
            SendEvent(FsmEvent.UiFloatValueChanged);
        }

    }
}

#endif