// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.


using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the Animation Triggers of a UI Selectable component. Modifications will not be visible if transition is not Animation")]
	public class UiSetAnimationTriggers : ComponentAction<Selectable>
	{
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the UI Selectable component.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The normal trigger value. Leave as None for no effect")]
		public FsmString normalTrigger;
		
		[Tooltip("The highlighted trigger value. Leave as None for no effect")]
		public FsmString highlightedTrigger;
		
		[Tooltip("The pressed trigger value. Leave as None for no effect")]
		public FsmString pressedTrigger;
		
		[Tooltip("The disabled trigger value. Leave as None for no effect")]
		public FsmString disabledTrigger;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

	    private Selectable selectable;
	    private AnimationTriggers _animationTriggers;
	    private AnimationTriggers originalAnimationTriggers;
		
		
		public override void Reset()
		{
			gameObject = null;
			
			normalTrigger = new FsmString {UseVariable=true};
			highlightedTrigger = new FsmString {UseVariable=true};
			pressedTrigger = new FsmString {UseVariable=true};
			disabledTrigger = new FsmString {UseVariable=true};
			
			resetOnExit = null;
		}
		
		public override void OnEnter()
		{
		    var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (UpdateCache(go))
		    {
		        selectable = cachedComponent;
		    }

			if (selectable!=null && resetOnExit.Value)
			{
				originalAnimationTriggers = selectable.animationTriggers;
			}

			DoSetValue();
		
			Finish();
		}


	    private void DoSetValue()
		{
			if (selectable==null)
			{
				return;
			}
			
			_animationTriggers = selectable.animationTriggers;

			if (!normalTrigger.IsNone)
			{
				_animationTriggers.normalTrigger = normalTrigger.Value;
			}
			if (!highlightedTrigger.IsNone)
			{
				_animationTriggers.highlightedTrigger = highlightedTrigger.Value;
			}
			if (!pressedTrigger.IsNone)
			{
				_animationTriggers.pressedTrigger = pressedTrigger.Value;
			}
			if (!disabledTrigger.IsNone)
			{
				_animationTriggers.disabledTrigger = disabledTrigger.Value;
			}

			selectable.animationTriggers = _animationTriggers;
		}
		
		public override void OnExit()
		{
			if (selectable==null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				selectable.animationTriggers = originalAnimationTriggers;
			}
		}
		
		
	}
}