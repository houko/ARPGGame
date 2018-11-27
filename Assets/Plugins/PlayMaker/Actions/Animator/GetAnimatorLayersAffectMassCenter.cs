// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns if additional layers affects the mass center")]
	public class GetAnimatorLayersAffectMassCenter : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[ActionSection("Results")]
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("If true, additional layers affects the mass center")]
		public FsmBool affectMassCenter;
		
		[Tooltip("Event send if additional layers affects the mass center")]
		public FsmEvent affectMassCenterEvent;
		
		[Tooltip("Event send if additional layers do no affects the mass center")]
		public FsmEvent doNotAffectMassCenterEvent;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			affectMassCenter = null;
			affectMassCenterEvent = null;
			doNotAffectMassCenterEvent = null;
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
			
			CheckAffectMassCenter();
			
			Finish();
			
		}
	
		void CheckAffectMassCenter()
		{		
			if (_animator==null)
			{
				return;
			}
			
			bool _affect = _animator.layersAffectMassCenter;
			
			affectMassCenter.Value = _affect;
			
			if (_affect)
			{
				Fsm.Event(affectMassCenterEvent);
			}else{
				Fsm.Event(doNotAffectMassCenterEvent);
			}

		}
		
	}
}