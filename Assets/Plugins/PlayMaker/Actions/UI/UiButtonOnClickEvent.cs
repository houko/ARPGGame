// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends an event when a UI Button is clicked.")]
	public class UiButtonOnClickEvent : ComponentAction<UnityEngine.UI.Button>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Button))]
		[Tooltip("The GameObject with the UGui button component.")]
		public FsmOwnerDefault gameObject;

        [Tooltip("Where to send the event.")]
	    public FsmEventTarget eventTarget;

		[Tooltip("Send this event when Clicked.")]
		public FsmEvent sendEvent;

		private UnityEngine.UI.Button button;
		
		public override void Reset()
		{
			gameObject = null;
			sendEvent = null;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
			    if (button != null)
			    {
                    button.onClick.RemoveListener(DoOnClick);
			    }

				button = cachedComponent;

				if (button != null)
				{
					button.onClick.AddListener(DoOnClick);
				}
				else
				{
					LogError("Missing UI.Button on "+go.name);
				}
			}
			else
			{
				LogError("Missing GameObject ");
			}

		}
         
		public override void OnExit()
		{
			if (button != null)
			{
				button.onClick.RemoveListener(DoOnClick);
			}
		}

		public void DoOnClick()
		{   
			SendEvent(eventTarget, sendEvent);
		}
	}
}