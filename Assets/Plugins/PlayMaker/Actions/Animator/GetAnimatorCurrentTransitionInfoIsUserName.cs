// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Check the active Transition user-specified name on a specified layer.")]
	public class GetAnimatorCurrentTransitionInfoIsUserName : FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;
		
		[Tooltip("The user-specified name to check the transition against.")]
		public FsmString userName;

		[ActionSection("Results")]

		[UIHint(UIHint.Variable)]
		[Tooltip("True if name matches")]
		public FsmBool nameMatch;
		
		[Tooltip("Event send if name matches")]
		public FsmEvent nameMatchEvent;
		
		[Tooltip("Event send if name doesn't match")]
		public FsmEvent nameDoNotMatchEvent;

		private Animator _animator;
		
		public override void Reset()
		{
			base.Reset();

			gameObject = null;
			layerIndex = null;
			
			userName = null;
			
			nameMatch = null;
			nameMatchEvent = null;
			nameDoNotMatchEvent = null;

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
			
			IsName();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnActionUpdate()
		{
			IsName();
		}
		
		void IsName()
		{		
			if (_animator!=null)
			{
				AnimatorTransitionInfo _info = _animator.GetAnimatorTransitionInfo(layerIndex.Value);

				bool _isMatch = _info.IsUserName(userName.Value);

				if (! nameMatch.IsNone)
				{
					nameMatch.Value = _isMatch;
				}

				if (_isMatch)
				{
					Fsm.Event(nameMatchEvent);
				}else{
					Fsm.Event(nameDoNotMatchEvent);
				}
			}
		}
	}
}