// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Gets the Bool data from the last Event.")]
	public class GetEventBoolData : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the bool data in a variable.")]
		public FsmBool getBoolData;

		public override void Reset()
		{
			getBoolData = null;
		}

		public override void OnEnter()
		{
			getBoolData.Value = Fsm.EventData.BoolData;
			
			Finish();
		}
	}
}