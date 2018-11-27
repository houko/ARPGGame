// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns the culling of this Animator component. Optionally sends events.\n" +
		"If true ('AlwaysAnimate'): always animate the entire character. Object is animated even when offscreen.\n" +
		 "If False ('BasedOnRenderers') animation is disabled when renderers are not visible.")]
	public class GetAnimatorCullingMode : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[ActionSection("Results")]
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("If true, always animate the entire character, else animation is disabled when renderers are not visible")]
		public FsmBool alwaysAnimate;
		
		[Tooltip("Event send if culling mode is 'AlwaysAnimate'")]
		public FsmEvent alwaysAnimateEvent;
		
		[Tooltip("Event send if culling mode is 'BasedOnRenders'")]
		public FsmEvent basedOnRenderersEvent;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			alwaysAnimate = null;
			alwaysAnimateEvent = null;
			basedOnRenderersEvent = null;
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
			
			DoCheckCulling();
			
			Finish();
			
		}
	
		void DoCheckCulling()
		{		
			if (_animator==null)
			{
				return;
			}
			
			bool _alwaysOn = _animator.cullingMode==AnimatorCullingMode.AlwaysAnimate?true:false;
			
			alwaysAnimate.Value = _alwaysOn;
			
			if (_alwaysOn)
			{
				Fsm.Event(alwaysAnimateEvent);
			}else{
				Fsm.Event(basedOnRenderersEvent);
			}

		}
		
	}
}