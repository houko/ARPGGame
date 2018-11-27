// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Sets the Mass of a Game Object's Rigid Body 2D.")]
    public class SetMass2d : ComponentAction<Rigidbody2D>
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with the Rigidbody2D attached")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[HasFloatSlider(0.1f,10f)]
		[Tooltip("The Mass")]
		public FsmFloat mass;
		
		public override void Reset()
		{
			gameObject = null;
			mass = 1;
		}
		
		public override void OnEnter()
		{
			DoSetMass();
			
			Finish();
		}
		
		void DoSetMass()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (!UpdateCache(go))
            {
                return;
            }	
			
			rigidbody2d.mass = mass.Value;
		}
	}
}