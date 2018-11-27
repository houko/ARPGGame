// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Set up multiple button events in a single action.")]
	public class UiButtonArray : FsmStateAction
	{
	    [Tooltip("Where to send the events.")]
	    public FsmEventTarget eventTarget;

        [CompoundArray("Buttons", "Button", "Click Event")]
		[CheckForComponent(typeof(UnityEngine.UI.Button))]
		[Tooltip("The GameObject with the UI button component.")]
		public FsmGameObject[] gameObjects;

	    [Tooltip("Send this event when the button is Clicked.")]
	    public FsmEvent[] clickEvents;

        [SerializeField]
	    private UnityEngine.UI.Button[] buttons;

        [SerializeField]
        private  GameObject[] cachedGameObjects;

	    private UnityAction[] actions;

	    private int clickedButton;

		public override void Reset()
		{
			gameObjects = new FsmGameObject[3];
			clickEvents = new FsmEvent[3];
		}

        /// <summary>
        /// Try to do all GetComponent calls in Preprocess as part of build
        /// But sometimes the values are not known at build time...
        /// </summary>
	    public override void OnPreprocess()
	    {
	        buttons = new UnityEngine.UI.Button[gameObjects.Length];
	        cachedGameObjects = new GameObject[gameObjects.Length];
            actions = new UnityAction[gameObjects.Length];

	        InitButtons();
	    }

	    private void InitButtons()
	    {
	        for (var i = 0; i < gameObjects.Length; i++)
	        {
	            var go = gameObjects[i].Value;
	            if (go != null)
	            {
	                if (cachedGameObjects[i] != go)
	                {
	                    buttons[i] = go.GetComponent<UnityEngine.UI.Button>();
	                    cachedGameObjects[i] = go;
	                }
	            }
	        }
	    }

	    public override void OnEnter()
	    {
	        InitButtons();

	        for (var i = 0; i < buttons.Length; i++)
	        {
	            var button = buttons[i];
	            if (button == null) continue;

	            var index = i;
	            actions[i] = () => { OnClick(index); }; 
	            button.onClick.AddListener(actions[i]);
	        }
	    }
         
		public override void OnExit()
		{
		    for (var i = 0; i < gameObjects.Length; i++)
		    {
		        var go = gameObjects[i];
		        if (go.Value == null) continue;
		        go.Value.GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(actions[i]);
		    }
		}

		public void OnClick(int index)
		{   
			Fsm.Event(gameObjects[index].Value, eventTarget, clickEvents[index]);
		}
	}
}