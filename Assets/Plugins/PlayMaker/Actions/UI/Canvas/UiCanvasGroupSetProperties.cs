// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
// original action by Nuclear Napalm Entertainment LLC: http://hutonggames.com/playmakerforum/index.php?topic=10581.msg49831#msg49831

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets properties of a UI CanvasGroup component.")]
	public class UiCanvasGroupSetProperties : ComponentAction<CanvasGroup>
	{
		[RequiredField]
		[CheckForComponent(typeof(CanvasGroup))]
		[Tooltip("The GameObject with the UI CanvasGroup component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Canvas group alpha. Ranges from 0.0 to 1.0.")]
		[HasFloatSlider(0f,1f)]
		public FsmFloat alpha;

		[Tooltip("Is the group interactable (are the elements beneath the group enabled). Leave as None for no effect")]
		public FsmBool interactable;

		[Tooltip("Does this group block raycasting (allow collision). Leave as None for no effect")]
		public FsmBool blocksRaycasts;

		[Tooltip("Should the group ignore parent groups? Leave as None for no effect")]
		public FsmBool ignoreParentGroup;
		
		[Tooltip("Reset when exiting this state. Leave as None for no effect")]
		public FsmBool resetOnExit;

		public bool everyFrame;

	    private CanvasGroup component;

	    private float originalAlpha;
	    private bool originalInteractable;
	    private bool originalBlocksRaycasts;
	    private bool originalIgnoreParentGroup;

		public override void Reset()
		{
			gameObject = null;
			alpha = new FsmFloat {UseVariable=true};
			interactable = new FsmBool {UseVariable=true};
			blocksRaycasts = new FsmBool {UseVariable=true};
			ignoreParentGroup = new FsmBool {UseVariable=true};
			resetOnExit = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				component = cachedComponent;
			    if (component != null)
			    {
			        originalAlpha = component.alpha;
			        originalInteractable = component.interactable;
			        originalBlocksRaycasts = component.blocksRaycasts;
			        originalIgnoreParentGroup = component.ignoreParentGroups;
			    }
			}
			
			DoAction();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoAction();
		}

	    private void DoAction()
		{
		    if (component == null) return;

		    if (!alpha.IsNone)
		    {
		        component.alpha = alpha.Value;
		    }
		    if (!interactable.IsNone)
		    {
		        component.interactable = interactable.Value;
		    }
		    if (!blocksRaycasts.IsNone)
		    {
		        component.blocksRaycasts = blocksRaycasts.Value;
		    }
		    if (!ignoreParentGroup.IsNone)
		    {
		        component.ignoreParentGroups = ignoreParentGroup.Value;
		    }
		}
		
		public override void OnExit()
		{
			if (component==null) return;
			
			if (resetOnExit.Value)
			{
				if (!alpha.IsNone)
				{
					component.alpha = originalAlpha;
				}
				if (!interactable.IsNone)
				{
					component.interactable = originalInteractable;
				}
				if (!blocksRaycasts.IsNone)
				{
					component.blocksRaycasts = originalBlocksRaycasts;
				}
				if (!ignoreParentGroup.IsNone)
				{
					component.ignoreParentGroups = originalIgnoreParentGroup;
				}
			}
		}
	}
}
