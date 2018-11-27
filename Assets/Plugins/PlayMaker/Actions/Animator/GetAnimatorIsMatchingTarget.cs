// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns true if automatic matching is active. Can also send events")]
	public class GetAnimatorIsMatchingTarget: FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component and a PlayMakerAnimatorProxy component are required")]
		public FsmOwnerDefault gameObject;

		[ActionSection("Results")]
		
		[UIHint(UIHint.Variable)]
		[Tooltip("True if automatic matching is active")]
		public FsmBool isMatchingActive;
		
		[Tooltip("Event send if automatic matching is active")]
		public FsmEvent matchingActivatedEvent;
		
		[Tooltip("Event send if automatic matching is not active")]
		public FsmEvent matchingDeactivedEvent;

		private Animator _animator;
		
		public override void Reset()
		{
			base.Reset();

			gameObject = null;
			isMatchingActive = null;
			matchingActivatedEvent = null;
			matchingDeactivedEvent = null;
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

			DoCheckIsMatchingActive();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
	
		
		public override void OnActionUpdate() 
		{
			DoCheckIsMatchingActive();

		}

		void DoCheckIsMatchingActive()
		{		
			if (_animator==null)
			{
				return;
			}
			
			bool _isMatchingActive = _animator.isMatchingTarget;
			isMatchingActive.Value = _isMatchingActive;
			
			if (_isMatchingActive)
			{
				Fsm.Event(matchingActivatedEvent);
			}else{
				Fsm.Event(matchingDeactivedEvent);
			}
		}
		
	}
}