// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Creates a rotation which rotates angle degrees around axis.")]
	public class QuaternionAngleAxis : QuaternionBaseAction
	{
		[RequiredField]
		[Tooltip("The angle.")]
		public FsmFloat angle;
		
		[RequiredField]
		[Tooltip("The rotation axis.")]
		public FsmVector3 axis;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the rotation of this quaternion variable.")]
		public FsmQuaternion result;

		public override void Reset()
		{
			angle = null;
			axis = null;
			result = null;
			everyFrame = true;
			everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		public override void OnEnter()
		{
			DoQuatAngleAxis();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (everyFrameOption == everyFrameOptions.Update )
			{
				DoQuatAngleAxis();
			}
		}
		public override void OnLateUpdate()
		{
			if (everyFrameOption == everyFrameOptions.LateUpdate )
			{
				DoQuatAngleAxis();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (everyFrameOption == everyFrameOptions.FixedUpdate )
			{
				DoQuatAngleAxis();
			}
		}

		void DoQuatAngleAxis()
		{
			result.Value = Quaternion.AngleAxis(angle.Value,axis.Value);
		}
	}
}

