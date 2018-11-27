#if !PLAYMAKER_NO_UI

using UnityEngine.UI;
using UnityEngine;

namespace HutongGames.PlayMaker
{
    [AddComponentMenu("PlayMaker/UI/UI Int Value Changed Event")]
    public class PlayMakerUiIntValueChangedEvent : PlayMakerUiEventBase
    {
        public Dropdown dropdown;

        protected override void Initialize()
        {
            if (initialized) return;
            initialized = true;

            if (dropdown == null)
            {
                dropdown = GetComponent<Dropdown>();
            }

            if (dropdown != null)
            {
                dropdown.onValueChanged.AddListener(OnValueChanged);
            }
        }

        protected void OnDisable()
        {
            initialized = false;

            if (dropdown != null)
            {
                dropdown.onValueChanged.RemoveListener(OnValueChanged);
            }
        }

        private void OnValueChanged(int value)
        {
            Fsm.EventData.IntData = value;
            SendEvent(FsmEvent.UiIntValueChanged);
        }

    }
}

#endif