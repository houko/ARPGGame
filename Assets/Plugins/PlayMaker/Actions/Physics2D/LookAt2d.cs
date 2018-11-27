// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
// original action created by collidernyc: http://hutonggames.com/playmakerforum/index.php?topic=7075.msg37373#msg37373

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Rotates a 2d Game Object on it's z axis so its forward vector points at a 2d or 3d position.")]
	public class LookAt2d : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to rotate.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The 2d position to Look At.")]
		public FsmVector2 vector2Target;

		[Tooltip("The 3d position to Look At. If not set to none, will be added to the 2d target")]
		public FsmVector3 vector3Target;

		[Tooltip("Set the GameObject starting offset. In degrees. 0 if your object is facing right, 180 if facing left etc...")]
		public FsmFloat rotationOffset;
		
		[Title("Draw Debug Line")]
		[Tooltip("Draw a debug line from the GameObject to the Target.")]
		public FsmBool debug;
		
		[Tooltip("Color to use for the debug line.")] 
		public FsmColor debugLineColor;
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame = true;

	    public override void Reset()
		{
			gameObject = null;

			vector2Target = null;
			vector3Target = new FsmVector3() {UseVariable=true};

			debug = false;
			debugLineColor = Color.green;
			everyFrame = true;
		}
		
		public override void OnEnter()
		{
			DoLookAt();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoLookAt();
		}
		
		void DoLookAt()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);			
			if (go == null)
			{
				return;
			}
			
			var target = new Vector3(vector2Target.Value.x,vector2Target.Value.y,0f);
			if (!vector3Target.IsNone)
			{
				target += vector3Target.Value;
			}

			var diff = target - go.transform.position;
			diff.Normalize();

			
			var rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			go.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - rotationOffset.Value);
			
			if (debug.Value)
			{
				Debug.DrawLine(go.transform.position, target, debugLineColor.Value);
			}
			
		}
		
	}
}