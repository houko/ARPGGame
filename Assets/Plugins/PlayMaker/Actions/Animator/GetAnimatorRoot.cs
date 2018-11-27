// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the avatar body mass center position and rotation.Optionally accept a GameObject to get the body transform. \nThe position and rotation are local to the gameobject")]
	public class GetAnimatorRoot: FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target.")]
		public FsmOwnerDefault gameObject;
		
		[ActionSection("Results")]
			
		[UIHint(UIHint.Variable)]
		[Tooltip("The avatar body mass center")]
		public FsmVector3 rootPosition;

		[UIHint(UIHint.Variable)]
		[Tooltip("The avatar body mass center")]
		public FsmQuaternion rootRotation;
		
		[Tooltip("If set, apply the body mass center position and rotation to this gameObject")]
		public FsmGameObject bodyGameObject;
			
		private Animator _animator;
		
		private Transform _transform;
		
		public override void Reset()
		{
			base.Reset();

			gameObject = null;
			rootPosition= null;
			rootRotation = null;
			bodyGameObject = null;

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
			
			DoGetBodyPosition();
			
			if (!everyFrame)
			{
				Finish();
			}
			
		}

		public override void OnActionUpdate() 
		{
			DoGetBodyPosition();
		}
		
		void DoGetBodyPosition()
		{		
			if (_animator==null)
			{
				return;
			}
			
			rootPosition.Value = _animator.rootPosition;
			rootRotation.Value = _animator.rootRotation;
			
			if (_transform!=null)
			{
				_transform.position = _animator.rootPosition;
				_transform.rotation = _animator.rootRotation;
			}
		}

	}
}