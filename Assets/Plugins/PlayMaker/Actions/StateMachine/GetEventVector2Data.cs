// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Gets the Vector2 data from the last Event.")]
    public class GetEventVector2Data : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the vector2 data in a variable.")]
        public FsmVector2 getVector2Data;

        public override void Reset()
        {
            getVector2Data = null;
        }

        public override void OnEnter()
        {
            getVector2Data.Value = Fsm.EventData.Vector2Data;
			
            Finish();
        }
    }
}