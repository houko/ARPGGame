// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
// based on Sebastio work: http://hutonggames.com/playmakerforum/index.php?topic=8452.msg42858#msg42858

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Set Group Alpha.")]
	public class UiCanvasGroupSetAlpha : ComponentAction<CanvasGroup>
	{
		[RequiredField]
		[CheckForComponent(typeof(CanvasGroup))]
		[Tooltip("The GameObject with a UI CanvasGroup component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The alpha of the UI component.")]
		public FsmFloat alpha;
		
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
		
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;

	    private CanvasGroup component;
	    private float originalValue;

		public override void Reset()
		{
			gameObject = null;
			alpha = null;
			
			resetOnExit = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				component = cachedComponent;
			}

            originalValue = component.alpha;

			DoSetValue();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetValue();
		}

	    private void DoSetValue()
		{
			if (component != null)
			{
				component.alpha = alpha.Value;
			}
		}

		public override void OnExit()
		{
			if (component==null) return;
			
			if (resetOnExit.Value)
			{
				component.alpha = originalValue;
			}
		}
		
	}
}