// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Set Apply Root Motion: If true, Root is controlled by animations")]
	public class SetAnimatorApplyRootMotion: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("If true, Root is controlled by animations")]
		public FsmBool applyRootMotion;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			applyRootMotion= null;
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
			
			DoApplyRootMotion();
			
			Finish();
			
		}
	
		void DoApplyRootMotion()
		{		
			if (_animator==null)
			{
				return;
			}
			
			_animator.applyRootMotion = applyRootMotion.Value;
		}
		
	}
}