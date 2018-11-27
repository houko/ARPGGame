// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Gets the Int data from the last Event.")]
    public class GetEventIntData : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the int data in a variable.")]
        public FsmInt getIntData;

        public override void Reset()
        {
            getIntData = null;
        }

        public override void OnEnter()
        {
            getIntData.Value = Fsm.EventData.IntData;
			
            Finish();
        }
    }
}