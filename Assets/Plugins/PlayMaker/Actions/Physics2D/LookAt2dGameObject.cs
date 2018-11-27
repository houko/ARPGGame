// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
// original action created by collidernyc: http://hutonggames.com/playmakerforum/index.php?topic=7075.msg37373#msg37373

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Rotates a 2d Game Object on it's z axis so its forward vector points at a Target.")]
	public class LookAt2dGameObject : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to rotate.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The GameObject to Look At.")]
		public FsmGameObject targetObject;
		
		[Tooltip("Set the GameObject starting offset. In degrees. 0 if your object is facing right, 180 if facing left etc...")]
		public FsmFloat rotationOffset;
		
		[Title("Draw Debug Line")]
		[Tooltip("Draw a debug line from the GameObject to the Target.")]
		public FsmBool debug;
		
		[Tooltip("Color to use for the debug line.")] 
		public FsmColor debugLineColor;
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame = true;
		
		private GameObject go;
		private GameObject goTarget;

	    public override void Reset()
		{
			gameObject = null;
			targetObject = null;
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
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			goTarget = targetObject.Value;
			
			if (go == null || targetObject == null)
			{
				return;
			}
			
			var diff = goTarget.transform.position - go.transform.position;
			diff.Normalize();
			
			var rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			go.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - rotationOffset.Value);
			
			if (debug.Value)
			{
				Debug.DrawLine(go.transform.position, goTarget.transform.position, debugLineColor.Value);
			}
			
		}
		
	}
}