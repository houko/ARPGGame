// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Interpolates between 2 Vector2 values over a specified Time.")]
	public class Vector2Interpolate : FsmStateAction
	{
		[Tooltip("The interpolation type")]
		public InterpolationType mode;
		
		[RequiredField]
		[Tooltip("The vector to interpolate from")]
		public FsmVector2 fromVector;
		
		[RequiredField]
		[Tooltip("The vector to interpolate to")]
		public FsmVector2 toVector;
		
		[RequiredField]
		[Tooltip("the interpolate time")]
		public FsmFloat time;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("the interpolated result")]
		public FsmVector2 storeResult;
		
		[Tooltip("This event is fired when the interpolation is done.")]
		public FsmEvent finishEvent;
		
		[Tooltip("Ignore TimeScale")]
		public bool realTime;

		private float startTime;
		private float currentTime;
		
		public override void Reset()
		{
			mode = InterpolationType.Linear;
			fromVector = new FsmVector2 { UseVariable = true };
			toVector = new FsmVector2 { UseVariable = true };
			time = 1.0f;
			storeResult = null;
			finishEvent = null;
			realTime = false;
		}

		public override void OnEnter()
		{
			startTime = FsmTime.RealtimeSinceStartup;
			currentTime = 0f;
			
			if (storeResult == null)
				Finish();
			else
				storeResult.Value = fromVector.Value;
		}
		
		public override void OnUpdate()
		{
			// update time
			
			if (realTime)
			{
				currentTime = FsmTime.RealtimeSinceStartup - startTime;
			}
			else
			{
				currentTime += Time.deltaTime;
			}
			
			float weight = currentTime/time.Value;
			
			switch (mode) {
			
			case InterpolationType.Linear:
				break;
				
			case InterpolationType.EaseInOut:
				weight = Mathf.SmoothStep(0, 1, weight);				
				break;
			}

			storeResult.Value = Vector2.Lerp(fromVector.Value, toVector.Value, weight);
			
			if (weight > 1)
			{
				if (finishEvent != null)
					Fsm.Event(finishEvent);

				Finish();
			}
				
		}
	}
}

