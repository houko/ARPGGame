// (c) Copyright HutongGames, all rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Gets the Vector3 data from the last Event.")]
    public class GetEventVector3Data : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the vector3 data in a variable.")]
        public FsmVector3 getVector3Data;

        public override void Reset()
        {
            getVector3Data = null;
        }

        public override void OnEnter()
        {
            getVector3Data.Value = Fsm.EventData.Vector3Data;
			
            Finish();
        }
    }
}