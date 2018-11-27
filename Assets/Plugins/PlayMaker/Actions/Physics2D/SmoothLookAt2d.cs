// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Smoothly Rotates a 2d Game Object so its right vector points at a Target. The target can be defined as a 2d Game Object or a 2d/3d world Position. If you specify both, then the position will be used as a local offset from the object's position.")]
	public class SmoothLookAt2d : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to rotate to face a target.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("A target GameObject.")]
		public FsmGameObject targetObject;

		[Tooltip("A target position. If a Target Object is defined, this is used as a local offset.")]
		public FsmVector2 targetPosition2d;

		[Tooltip("A target position. If a Target Object is defined, this is used as a local offset.")]
		public FsmVector3 targetPosition;

		[Tooltip("Set the GameObject starting offset. In degrees. 0 if your object is facing right, 180 if facing left etc...")]
		public FsmFloat rotationOffset;

		[HasFloatSlider(0.5f,15)]
		[Tooltip("How fast the look at moves.")]
		public FsmFloat speed;
		
		[Tooltip("Draw a line in the Scene View to the look at position.")]
		public FsmBool debug;
		
		[Tooltip("If the angle to the target is less than this, send the Finish Event below. Measured in degrees.")]
		public FsmFloat finishTolerance;
		
		[Tooltip("Event to send if the angle to target is less than the Finish Tolerance.")]
		public FsmEvent finishEvent;
		
		private GameObject previousGo; // track game object so we can re-initialize when it changes.
		private Quaternion lastRotation;
		private Quaternion desiredRotation;
		
		public override void Reset()
		{
			gameObject = null;
			targetObject = null;
			targetPosition2d = new FsmVector2 { UseVariable = true};
			targetPosition = new FsmVector3 { UseVariable = true};
			rotationOffset = 0;
			debug = false;
			speed = 5;
			finishTolerance = 1;
			finishEvent = null;
		}

        public override void OnPreprocess()
        {
            Fsm.HandleLateUpdate = true;
        }
		
		public override void OnEnter()
		{
			previousGo = null;
		}
		
		public override void OnLateUpdate()
		{
			DoSmoothLookAt();
		}
		
		void DoSmoothLookAt()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}
			
			var goTarget = targetObject.Value;

			// re-initialize if game object has changed
			
			if (previousGo != go)
			{
				lastRotation = go.transform.rotation;
				desiredRotation = lastRotation;
				previousGo = go;
			}
			
			// desired look at position

			var lookAtPos = new Vector3(targetPosition2d.Value.x,targetPosition2d.Value.y,0f);
			if (!targetPosition.IsNone)
			{
				lookAtPos += targetPosition.Value;
			}

			if (goTarget != null)
			{
				lookAtPos = goTarget.transform.position;
				var _offset = Vector3.zero;

				if (!targetPosition.IsNone)
				{
					_offset +=targetPosition.Value;
				}
				if (!targetPosition2d.IsNone)
				{
					_offset.x = _offset.x+ targetPosition2d.Value.x;
					_offset.y = _offset.y+ targetPosition2d.Value.y;
				}

				if (!targetPosition2d.IsNone || !targetPosition.IsNone)
				{
					lookAtPos += goTarget.transform.TransformPoint(targetPosition2d.Value);
				}
			}
		
			var diff = lookAtPos - go.transform.position;
			diff.Normalize();
			
			
			var rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			desiredRotation = Quaternion.Euler(0f, 0f, rot_z - rotationOffset.Value);


			lastRotation = Quaternion.Slerp(lastRotation, desiredRotation, speed.Value * Time.deltaTime);
			go.transform.rotation = lastRotation;
			
			// debug line to target
			
			if (debug.Value)
			{
				Debug.DrawLine(go.transform.position, lookAtPos, Color.grey);
			}
			
			// send finish event?
			
			if (finishEvent != null)
			{
				//var targetDir = lookAtPos - go.transform.position;
				//var angle = Vector3.Angle(targetDir, go.transform.right) - rotationOffset.Value;
				var angle = Vector3.Angle(desiredRotation.eulerAngles,lastRotation.eulerAngles);
				if (Mathf.Abs(angle ) <= finishTolerance.Value)
				{
					Fsm.Event(finishEvent);
				}
			}
		}
		
	}
}