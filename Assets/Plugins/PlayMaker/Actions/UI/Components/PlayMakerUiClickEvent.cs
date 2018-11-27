#if !PLAYMAKER_NO_UI

using UnityEngine.UI;
using UnityEngine;

namespace HutongGames.PlayMaker
{
    [AddComponentMenu("PlayMaker/UI/UI Click Event")]
    public class PlayMakerUiClickEvent : PlayMakerUiEventBase
    {
        public Button button;

        protected override void Initialize()
        {
            if (initialized) return;
            initialized = true;

            if (button == null)
            {
                button = GetComponent<Button>();
            }

            if (button != null)
            {
                button.onClick.AddListener(DoOnClick);
            }
        }

        protected void OnDisable()
        {
            initialized = false;

            if (button != null)
            {
                button.onClick.RemoveListener(DoOnClick);
            }
        }

        private void DoOnClick()
        {
            SendEvent(FsmEvent.UiClick);
        }

    }
}

#endif