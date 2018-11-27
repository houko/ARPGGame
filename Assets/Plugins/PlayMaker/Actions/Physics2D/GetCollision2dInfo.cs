// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Gets info on the last collision 2D event and store in variables. See Unity and PlayMaker docs on Unity 2D physics.")]
	public class GetCollision2dInfo : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the GameObject hit.")]
		public FsmGameObject gameObjectHit;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the relative velocity of the collision.")]
		public FsmVector3 relativeVelocity;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the relative speed of the collision. Useful for controlling reactions. E.g., selecting an appropriate sound fx.")]
		public FsmFloat relativeSpeed;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the world position of the collision contact. Useful for spawning effects etc.")]
		public FsmVector3 contactPoint;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the collision normal vector. Useful for aligning spawned effects etc.")]
		public FsmVector3 contactNormal;

		[UIHint(UIHint.Variable)]
		[Tooltip("The number of separate shaped regions in the collider.")]
		public FsmInt shapeCount;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the name of the physics 2D material of the colliding GameObject. Useful for triggering different effects. Audio, particles...")]
		public FsmString physics2dMaterialName;
		
		public override void Reset()
		{
			gameObjectHit = null;
			relativeVelocity = null;
			relativeSpeed = null;
			contactPoint = null;
			contactNormal = null;
			shapeCount = null;
			physics2dMaterialName = null;
		}
		
		void StoreCollisionInfo()
		{
		    if (Fsm.Collision2DInfo == null) return;

			gameObjectHit.Value = Fsm.Collision2DInfo.gameObject;
            relativeSpeed.Value = Fsm.Collision2DInfo.relativeVelocity.magnitude;
            relativeVelocity.Value = Fsm.Collision2DInfo.relativeVelocity;
            physics2dMaterialName.Value = Fsm.Collision2DInfo.collider.sharedMaterial != null ? Fsm.Collision2DInfo.collider.sharedMaterial.name : "";

            shapeCount.Value = Fsm.Collision2DInfo.collider.shapeCount;

            if (Fsm.Collision2DInfo.contacts != null && Fsm.Collision2DInfo.contacts.Length > 0)
			{
                contactPoint.Value = Fsm.Collision2DInfo.contacts[0].point;
                contactNormal.Value = Fsm.Collision2DInfo.contacts[0].normal;
			}
		}
		
		public override void OnEnter()
		{
			StoreCollisionInfo();
			
			Finish();
		}
	}
}