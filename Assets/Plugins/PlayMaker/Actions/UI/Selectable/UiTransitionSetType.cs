// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
// waiting for 1.8 to make it available using fsmEnum

using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the transition type of a UI Selectable component.")]
	public class UiTransitionSetType : ComponentAction<Selectable>
	{
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the UI Selectable component.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The transition value")]
		public Selectable.Transition transition;
		
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

	    private Selectable selectable;
	    private Selectable.Transition originalTransition;
		
		
		public override void Reset()
		{
			gameObject = null;
			transition = Selectable.Transition.ColorTint;

			resetOnExit = false;
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
				originalTransition = selectable.transition;
			}

			DoSetValue();

			Finish();
		}

	    private void DoSetValue()
		{
			if (selectable!=null)
			{
				selectable.transition = transition;
			}
		}
		
		public override void OnExit()
		{
			if (selectable==null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				selectable.transition = originalTransition;
			}
		}
		
		
	}
}