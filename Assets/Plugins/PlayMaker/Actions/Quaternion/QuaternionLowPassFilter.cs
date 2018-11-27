// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Use a low pass filter to reduce the influence of sudden changes in a quaternion Variable.")]
	public class QuaternionLowPassFilter : QuaternionBaseAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("quaternion Variable to filter. Should generally come from some constantly updated input")]
		public FsmQuaternion quaternionVariable;
		[Tooltip("Determines how much influence new changes have. E.g., 0.1 keeps 10 percent of the unfiltered quaternion and 90 percent of the previously filtered value.")]
		public FsmFloat filteringFactor;		
		
		Quaternion filteredQuaternion;
		
		public override void Reset()
		{
			quaternionVariable = null;
			filteringFactor = 0.1f;
			
			everyFrame = true;
			everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}
		
		public override void OnEnter()
		{
			filteredQuaternion = new Quaternion(quaternionVariable.Value.x, quaternionVariable.Value.y, quaternionVariable.Value.z,quaternionVariable.Value.w);

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (everyFrameOption == everyFrameOptions.Update )
			{
				DoQuatLowPassFilter();
			}
		}
		public override void OnLateUpdate()
		{
			if (everyFrameOption == everyFrameOptions.LateUpdate )
			{
				DoQuatLowPassFilter();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (everyFrameOption == everyFrameOptions.FixedUpdate )
			{
				DoQuatLowPassFilter();
			}
		}
		
		void DoQuatLowPassFilter()
		{
			filteredQuaternion.x = (quaternionVariable.Value.x * filteringFactor.Value) + (filteredQuaternion.x * (1.0f - filteringFactor.Value));
			filteredQuaternion.y = (quaternionVariable.Value.y * filteringFactor.Value) + (filteredQuaternion.y * (1.0f - filteringFactor.Value));
			filteredQuaternion.z = (quaternionVariable.Value.z * filteringFactor.Value) + (filteredQuaternion.z * (1.0f - filteringFactor.Value));
			filteredQuaternion.w = (quaternionVariable.Value.w * filteringFactor.Value) + (filteredQuaternion.w * (1.0f - filteringFactor.Value));

			quaternionVariable.Value = new Quaternion(filteredQuaternion.x,filteredQuaternion.y,filteredQuaternion.z,filteredQuaternion.w);
		}
	}
}

