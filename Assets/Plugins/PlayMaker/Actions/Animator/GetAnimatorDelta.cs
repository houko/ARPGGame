// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the avatar delta position and rotation for the last evaluated frame.")]
	public class GetAnimatorDelta: FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The avatar delta position for the last evaluated frame")]
		public FsmVector3 deltaPosition;

		[UIHint(UIHint.Variable)]
		[Tooltip("The avatar delta position for the last evaluated frame")]
		public FsmQuaternion deltaRotation;
		
		private Animator _animator;
		
		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			deltaPosition= null;
			deltaRotation = null;
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

			DoGetDeltaPosition();
			
			Finish();
			
		}
	
		public override void OnActionUpdate() 
		{
			DoGetDeltaPosition();

		}

		void DoGetDeltaPosition()
		{		
			if (_animator==null)
			{
				return;
			}
			
			deltaPosition.Value = _animator.deltaPosition;
			deltaRotation.Value = _animator.deltaRotation;
		}

	}
}