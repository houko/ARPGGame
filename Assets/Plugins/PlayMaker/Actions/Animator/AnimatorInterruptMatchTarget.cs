// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Interrupts the automatic target matching. CompleteMatch will make the gameobject match the target completely at the next frame.")]
	public class AnimatorInterruptMatchTarget : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Will make the gameobject match the target completely at the next frame")]
		public FsmBool completeMatch;
		
		public override void Reset()
		{
			gameObject = null;
			completeMatch = true;
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
			
			Animator _animator = go.GetComponent<Animator>();
			
			if (_animator!=null)
			{
				_animator.InterruptMatchTarget(completeMatch.Value);
			}
			
			Finish();
			
		}
		
		
	}
}