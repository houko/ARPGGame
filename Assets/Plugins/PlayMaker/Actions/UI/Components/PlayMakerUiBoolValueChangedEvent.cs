#if !PLAYMAKER_NO_UI

using UnityEngine.UI;
using UnityEngine;

namespace HutongGames.PlayMaker
{
    [AddComponentMenu("PlayMaker/UI/UI Bool Value Changed Event")]
    public class PlayMakerUiBoolValueChangedEvent : PlayMakerUiEventBase
    {
        public Toggle toggle;

        protected override void Initialize()
        {
            if (initialized) return;
            initialized = true;

            if (toggle == null)
            {
                toggle = GetComponent<Toggle>();
            }

            if (toggle != null)
            {
                toggle.onValueChanged.AddListener(OnValueChanged);
            }
        }

        protected void OnDisable()
        {
            initialized = false;

            if (toggle != null)
            {
                toggle.onValueChanged.RemoveListener(OnValueChanged);
            }
        }

        private void OnValueChanged(bool value)
        {
            Fsm.EventData.BoolData = value;
            SendEvent(FsmEvent.UiBoolValueChanged);
        }

    }
}

#endif