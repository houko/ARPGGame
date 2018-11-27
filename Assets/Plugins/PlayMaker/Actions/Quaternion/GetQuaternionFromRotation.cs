// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Creates a rotation which rotates from fromDirection to toDirection. Usually you use this to rotate a transform so that one of its axes, e.g., the y-axis - follows a target direction toDirection in world space.")]
	public class GetQuaternionFromRotation : QuaternionBaseAction
	{

		[RequiredField]
		[Tooltip("the 'from' direction")]
		public FsmVector3 fromDirection;
		
		[RequiredField]
		[Tooltip("the 'to' direction")]
		public FsmVector3 toDirection;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("the resulting quaternion")]
		public FsmQuaternion result;

		public override void Reset()
		{
			fromDirection = null;
			toDirection = null;
	
			result = null;
			everyFrame = false;
			everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		
		}

		public override void OnEnter()
		{
			DoQuatFromRotation();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (everyFrameOption == everyFrameOptions.Update )
			{
				DoQuatFromRotation();
			}
		}
		public override void OnLateUpdate()
		{
			if (everyFrameOption == everyFrameOptions.LateUpdate )
			{
				DoQuatFromRotation();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (everyFrameOption == everyFrameOptions.FixedUpdate )
			{
				DoQuatFromRotation();
			}
		}
		
		void DoQuatFromRotation()
		{
			result.Value = Quaternion.FromToRotation(fromDirection.Value,toDirection.Value);		
		}
	}
}