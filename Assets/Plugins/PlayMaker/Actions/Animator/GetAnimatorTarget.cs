// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the position and rotation of the target specified by SetTarget(AvatarTarget targetIndex, float targetNormalizedTime)).\n" +
		"The position and rotation are only valid when a frame has being evaluated after the SetTarget call")]
	public class GetAnimatorTarget: FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[ActionSection("Results")]
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The target position")]
		public FsmVector3 targetPosition;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The target rotation")]
		public FsmQuaternion targetRotation;
		
		[Tooltip("If set, apply the position and rotation to this gameObject")]
		public FsmGameObject targetGameObject;

		private Animator _animator;
		
		private Transform _transform;
		
		public override void Reset()
		{
			base.Reset();

			gameObject = null;
			targetPosition= null;
			targetRotation = null;
			targetGameObject = null;
			everyFrame = false;
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

			GameObject _target = targetGameObject.Value;
			if (_target!=null)
			{
				_transform = _target.transform;
			}
			
			DoGetTarget();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnActionUpdate() 
		{
			DoGetTarget();
		}

		void DoGetTarget()
		{		
			if (_animator==null)
			{
				return;
			}
			
			targetPosition.Value = _animator.targetPosition;
			targetRotation.Value = _animator.targetRotation;
			
			if (_transform!=null)
			{
				_transform.position = _animator.targetPosition;
				_transform.rotation = _animator.targetRotation;
			}
		}
	}
}