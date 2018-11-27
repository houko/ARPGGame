// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Creates a rotation that looks along forward with the head upwards along upwards.")]
	public class QuaternionLookRotation : QuaternionBaseAction
	{
		[RequiredField]
		[Tooltip("the rotation direction")]
		public FsmVector3 direction;
		
		[Tooltip("The up direction")]
		public FsmVector3 upVector;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the inverse of the rotation variable.")]
		public FsmQuaternion result;

		public override void Reset()
		{
			direction = null;
			upVector = new FsmVector3(){UseVariable=true};
			result = null;
			everyFrame = true;
			everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		public override void OnEnter()
		{
			DoQuatLookRotation();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (everyFrameOption == everyFrameOptions.Update )
			{
				DoQuatLookRotation();
			}
		}
		public override void OnLateUpdate()
		{
			if (everyFrameOption == everyFrameOptions.LateUpdate )
			{
				DoQuatLookRotation();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (everyFrameOption == everyFrameOptions.FixedUpdate )
			{
				DoQuatLookRotation();
			}
		}

		void DoQuatLookRotation()
		{
			if (!upVector.IsNone)
			{
				result.Value = Quaternion.LookRotation(direction.Value,upVector.Value);
			}
            else
            {
				result.Value = Quaternion.LookRotation(direction.Value);
			}
		}
	}
}

