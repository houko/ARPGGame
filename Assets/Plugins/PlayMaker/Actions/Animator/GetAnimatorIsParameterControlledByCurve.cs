// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns true if a parameter is controlled by an additional curve on an animation")]
	public class GetAnimatorIsParameterControlledByCurve: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The parameter's name")]
		public FsmString parameterName;
		
		[ActionSection("Results")]
		
		[UIHint(UIHint.Variable)]
		[Tooltip("True if controlled by curve")]
		public FsmBool isControlledByCurve;
		
		[Tooltip("Event send if controlled by curve")]
		public FsmEvent isControlledByCurveEvent;
		
		[Tooltip("Event send if not controlled by curve")]
		public FsmEvent isNotControlledByCurveEvent;

		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			parameterName = null;
			isControlledByCurve = null;
			isControlledByCurveEvent = null;
			isNotControlledByCurveEvent = null;

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

			
			DoCheckIsParameterControlledByCurve();

			Finish();

		}
		

		void DoCheckIsParameterControlledByCurve()
		{		
			if (_animator==null)
			{
				return;
			}
			
			bool _isControlledByCurve = _animator.IsParameterControlledByCurve(parameterName.Value);
			isControlledByCurve.Value = _isControlledByCurve;
			
			if (_isControlledByCurve)
			{
				Fsm.Event(isControlledByCurveEvent);
			}else{
				Fsm.Event(isNotControlledByCurveEvent);
			}
		}

	}
}