// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("If true, automatically stabilize feet during transition and blending")]
	public class SetAnimatorStabilizeFeet: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("If true, automatically stabilize feet during transition and blending")]
		public FsmBool stabilizeFeet;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			stabilizeFeet= null;
		}
		
		public override void OnEnter()
		{
			// get the animator component
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (go==null)
			{
				Finish();
				return;
			}
			
			_animator = go.GetComponent<Animator>();
			
			if (_animator==null)
			{
				Finish();
				return;
			}
			
			DoStabilizeFeet();
			
			Finish();
			
		}
	
		void DoStabilizeFeet()
		{		
			if (_animator==null)
			{
				return;
			}
			
			_animator.stabilizeFeet = stabilizeFeet.Value;
			
		}
		
	}
}