// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_5_OR_NEWER
    using UnityEngine.AI;
#endif
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Synchronize a NavMesh Agent velocity and rotation with the animator process.")]
	public class NavMeshAgentAnimatorSynchronizer : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(NavMeshAgent))]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Agent target. An Animator component and a NavMeshAgent component are required")]
		public FsmOwnerDefault gameObject;

		private Animator _animator;
		private NavMeshAgent _agent;
		
		private Transform _trans;
		
		public override void Reset()
		{
			gameObject = null;
		}

		public override void OnPreprocess ()
		{
			Fsm.HandleAnimatorMove = true;
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
			_agent = go.GetComponent<NavMeshAgent>();

			_animator = go.GetComponent<Animator>();
			
			if (_animator==null)
			{
				Finish();
				return;
			}
			
			_trans = go.transform;
		}
	
		public override void DoAnimatorMove()
		{
			_agent.velocity = _animator.deltaPosition / Time.deltaTime;
			_trans.rotation = _animator.rootRotation;
		}	
		
	}
}