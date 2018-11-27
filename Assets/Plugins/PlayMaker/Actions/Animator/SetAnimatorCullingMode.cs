// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3_OR_NEWER || UNITY_5 || UNITY_5_0
#define UNITY_5_OR_NEWER
#endif

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Controls culling of this Animator component.\n" +
		"If true, set to 'AlwaysAnimate': always animate the entire character. Object is animated even when offscreen.\n" +
	         "If False, set to 'BasedOnRenderes' or CullUpdateTransforms ( On Unity 5) animation is disabled when renderers are not visible.")]
	public class SetAnimatorCullingMode: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("If true, always animate the entire character, else animation is disabled when renderers are not visible")]
		public FsmBool alwaysAnimate;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			alwaysAnimate= null;
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
			
			SetCullingMode();
			
			Finish();
			
		}
	
		void SetCullingMode()
		{		
			if (_animator==null)
			{
				return;
			}

			#if UNITY_5_OR_NEWER
			_animator.cullingMode = alwaysAnimate.Value?AnimatorCullingMode.AlwaysAnimate:AnimatorCullingMode.CullUpdateTransforms;
			#else
			_animator.cullingMode = alwaysAnimate.Value?AnimatorCullingMode.AlwaysAnimate:AnimatorCullingMode.BasedOnRenderers;
			#endif
			
		}
		
	}
}