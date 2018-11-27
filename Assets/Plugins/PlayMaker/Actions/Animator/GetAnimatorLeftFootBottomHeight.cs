// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Get the left foot bottom height.")]
	public class GetAnimatorLeftFootBottomHeight : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[ActionSection("Result")]

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("the left foot bottom height.")]
		public FsmFloat leftFootHeight;
		
		[Tooltip("Repeat every frame. Useful when value is subject to change over time.")]
		public bool everyFrame;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			leftFootHeight = null;
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
			
			_getLeftFootBottonHeight();
			
			if (!everyFrame) 
			{
				Finish();
			}
		}
		
		public override void OnLateUpdate()
		{
			_getLeftFootBottonHeight();
		}
		
		void _getLeftFootBottonHeight()
		{		
			if (_animator!=null)
			{
				leftFootHeight.Value = _animator.leftFeetBottomHeight;
			}
		}
	}
}