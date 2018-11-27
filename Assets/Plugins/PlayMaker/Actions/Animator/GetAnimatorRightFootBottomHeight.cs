// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Get the right foot bottom height.")]
	public class GetAnimatorRightFootBottomHeight : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[ActionSection("Result")]
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The right foot bottom height.")]
		public FsmFloat rightFootHeight;

		[Tooltip("Repeat every frame during LateUpdate. Useful when value is subject to change over time.")]
		public bool everyFrame;

		private Animator _animator;
		
		public override void Reset()
		{
			base.Reset();

			gameObject = null;
			rightFootHeight = null;
			everyFrame = false;
		}

        public override void OnPreprocess()
        {
            Fsm.HandleLateUpdate = true;
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
			
			_getRightFootBottonHeight();
			
			if (!everyFrame) 
			{
				Finish();
			}
		}
		
		public override void OnLateUpdate()
		{
			_getRightFootBottonHeight();
		}
		
		void _getRightFootBottonHeight()
		{		
			if (_animator!=null)
			{
				rightFootHeight.Value = _animator.rightFeetBottomHeight;
			}
		}
	}
}