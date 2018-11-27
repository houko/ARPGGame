// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets info on the last joint break event.")]
	public class GetJointBreakInfo : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
        [Tooltip("Get the force that broke the joint.")]
		public FsmFloat breakForce;

		public override void Reset()
		{
		    breakForce = null;
		}

		public override void OnEnter()
		{
		    breakForce.Value = Fsm.JointBreakForce;
			
			Finish();
		}
	}
}