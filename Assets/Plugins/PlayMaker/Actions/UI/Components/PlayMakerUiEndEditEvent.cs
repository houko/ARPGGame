#if !PLAYMAKER_NO_UI

using UnityEngine.UI;
using UnityEngine;

namespace HutongGames.PlayMaker
{
    [AddComponentMenu("PlayMaker/UI/UI End Edit Event")]
    public class PlayMakerUiEndEditEvent : PlayMakerUiEventBase
    {
        public InputField inputField;

        protected override void Initialize()
        {
            if (initialized) return;
            initialized = true;

            if (inputField == null)
            {
                inputField = GetComponent<InputField>();
            }

            if (inputField != null)
            {
                inputField.onEndEdit.AddListener(DoOnEndEdit);
            }
        }

        protected void OnDisable()
        {
            initialized = false;

            if (inputField != null)
            {
                inputField.onEndEdit.RemoveListener(DoOnEndEdit);
            }
        }

        private void DoOnEndEdit(string value)
        {
            Fsm.EventData.StringData = value;
            SendEvent(FsmEvent.UiEndEdit);
        }

    }
}

#endif