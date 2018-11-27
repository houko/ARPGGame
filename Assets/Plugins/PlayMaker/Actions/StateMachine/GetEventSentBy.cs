// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Gets the sender of the last event.")]
    public class GetEventSentBy: FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the GameObject that sent the event.")]
        public FsmGameObject sentByGameObject;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the name of the GameObject that sent the event.")]
        public FsmString gameObjectName;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the name of the FSM that sent the event.")]
        public FsmString fsmName;

        public override void Reset()
        {
            sentByGameObject = null;
            gameObjectName = null;
            fsmName = null;
        }

        public override void OnEnter()
        {
		    if (Fsm.EventData.SentByGameObject != null)
		    {
		        sentByGameObject.Value = Fsm.EventData.SentByGameObject;	        
		    }
			else if (Fsm.EventData.SentByFsm != null)
			{
				sentByGameObject.Value = Fsm.EventData.SentByFsm.GameObject;
				fsmName.Value = Fsm.EventData.SentByFsm.Name;
			}
			else
			{
				sentByGameObject.Value = null;
				fsmName.Value = "";
			}

            if (sentByGameObject.Value != null)
            {
                gameObjectName.Value = sentByGameObject.Value.name;
            }

            Finish();
        }
    }
}