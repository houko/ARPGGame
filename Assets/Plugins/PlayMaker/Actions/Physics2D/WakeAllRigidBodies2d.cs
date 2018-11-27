// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Rigid bodies 2D start sleeping when they come to rest. This action wakes up all rigid bodies 2D in the scene. E.g., if you Set Gravity 2D and want objects at rest to respond.")]
	public class WakeAllRigidBodies2d : FsmStateAction
	{
        [Tooltip("Repeat every frame. Note: This would be very expensive!")]
		public bool everyFrame;

		public override void Reset()
		{
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoWakeAll();

		    if (!everyFrame)
		    {
		        Finish();
		    }		
		}
		
		public override void OnUpdate()
		{
			DoWakeAll();
		}
		
		void DoWakeAll()
		{
			var bodies = Object.FindObjectsOfType(typeof(Rigidbody2D)) as Rigidbody2D[];			
			if (bodies != null)
			{
				foreach (var body in bodies)
				{
					body.WakeUp();
				}
			}
		}
	}
}