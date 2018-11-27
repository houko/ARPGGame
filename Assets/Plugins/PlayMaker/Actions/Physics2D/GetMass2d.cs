// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Gets the Mass of a Game Object's Rigid Body 2D.")]
    public class GetMass2d : ComponentAction<Rigidbody2D>
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with a Rigidbody2D attached.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the mass of gameObject.")]
		public FsmFloat storeResult;
		
		public override void Reset()
		{
			gameObject = null;
			storeResult = null;
		}
		
		public override void OnEnter()
		{
			DoGetMass();
			
			Finish();
		}
		
		void DoGetMass()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (!UpdateCache(go))
            {
                return;
            }
			
			storeResult.Value = rigidbody2d.mass;
		}
	}
}