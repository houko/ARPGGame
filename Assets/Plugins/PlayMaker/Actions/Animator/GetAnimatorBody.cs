// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the avatar body mass center position and rotation. Optionally accepts a GameObject to get the body transform. \nThe position and rotation are local to the gameobject")]
	public class GetAnimatorBody: FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[ActionSection("Results")]
			
		[UIHint(UIHint.Variable)]
		[Tooltip("The avatar body mass center")]
		public FsmVector3 bodyPosition;

		[UIHint(UIHint.Variable)]
		[Tooltip("The avatar body mass center")]
		public FsmQuaternion bodyRotation;
		
		[Tooltip("If set, apply the body mass center position and rotation to this gameObject")]
		public FsmGameObject bodyGameObject;

		private Animator _animator;
		
		private Transform _transform;
		
		public override void Reset()
		{
			base.Reset();

			gameObject = null;
			bodyPosition= null;
			bodyRotation = null;
			bodyGameObject = null;
			this.everyFrame = false;
			this.everyFrameOption = AnimatorFrameUpdateSelector.OnAnimatorIK;
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

			GameObject _body = bodyGameObject.Value;
			if (_body!=null)
			{
				_transform = _body.transform;
			}

			if (this.everyFrameOption != AnimatorFrameUpdateSelector.OnAnimatorIK)
			{
				this.everyFrameOption = AnimatorFrameUpdateSelector.OnAnimatorIK;
			}
		}
	
		public override void OnActionUpdate()
		{
			DoGetBodyPosition();

			if (!everyFrame)
			{
				Finish();
			}
		}	
		
		void DoGetBodyPosition()
		{		
			if (_animator==null)
			{
				return;
			}
			
			bodyPosition.Value = _animator.bodyPosition;
			bodyRotation.Value = _animator.bodyRotation;
			
			if (_transform!=null)
			{
				_transform.position = _animator.bodyPosition;
				_transform.rotation = _animator.bodyRotation;
			}
		}

		public override string ErrorCheck()
		{
			if ( this.everyFrameOption != AnimatorFrameUpdateSelector.OnAnimatorIK)
			{
				return "Getting Body Position should only be done in OnAnimatorIK";
			}
			return string.Empty;
		}

	}
}