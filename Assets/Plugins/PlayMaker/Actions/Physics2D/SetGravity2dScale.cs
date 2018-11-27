// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Sets The degree to which this object is affected by gravity.  NOTE: Game object must have a rigidbody 2D.")]
    public class SetGravity2dScale : ComponentAction<Rigidbody2D>
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with a Rigidbody 2d attached")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The gravity scale effect")]
		public FsmFloat gravityScale;
		
		public override void Reset()
		{
			gameObject = null;
			gravityScale = 1f;
		}
		
		public override void OnEnter()
		{
			DoSetGravityScale();
			Finish();
		}
		
		void DoSetGravityScale()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (!UpdateCache(go))
            {
                return;
            }
			
			rigidbody2d.gravityScale = gravityScale.Value;
		}
	}
}