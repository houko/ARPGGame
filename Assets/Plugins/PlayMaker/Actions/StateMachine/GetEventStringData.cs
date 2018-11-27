// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Gets the String data from the last Event.")]
    public class GetEventStringData : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the string data in a variable.")]
        public FsmString getStringData;

        public override void Reset()
        {
            getStringData = null;
        }

        public override void OnEnter()
        {
            getStringData.Value = Fsm.EventData.StringData;
			
            Finish();
        }

#if UNITY_EDITOR
        public override string AutoName()
        {
            return "Get Event String > " + ActionHelpers.GetValueLabel(getStringData);
        }
#endif
    }
}