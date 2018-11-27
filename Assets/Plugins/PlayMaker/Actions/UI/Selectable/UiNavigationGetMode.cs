// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the navigation mode of a UI Selectable component.")]
	public class UiNavigationGetMode : ComponentAction<Selectable>
	{
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the UI Selectable component.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The navigation mode value")]
		public FsmString navigationMode;

		[Tooltip("Event sent if transition is ColorTint")]
		public FsmEvent automaticEvent;

		[Tooltip("Event sent if transition is ColorTint")]
		public FsmEvent horizontalEvent;

		[Tooltip("Event sent if transition is SpriteSwap")]
		public FsmEvent verticalEvent;

		[Tooltip("Event sent if transition is Animation")]
		public FsmEvent explicitEvent;

		[Tooltip("Event sent if transition is none")]
		public FsmEvent noNavigationEvent;

	    private Selectable selectable;
	    private Selectable.Transition originalTransition;
		
		
		public override void Reset()
		{
			gameObject = null;

		}
		
		public override void OnEnter()
		{
		    var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (UpdateCache(go))
		    {
		        selectable = cachedComponent;
		    }
			
			DoGetValue();

			Finish();
		}

	    private void DoGetValue()
		{
			if (selectable==null)
			{
				return;
			}

			navigationMode.Value = selectable.navigation.mode.ToString();

			if (selectable.navigation.mode == Navigation.Mode.None)
			{
				Fsm.Event(noNavigationEvent);
			}else if (selectable.navigation.mode == Navigation.Mode.Automatic)
			{
				Fsm.Event(automaticEvent);
			}else if (selectable.navigation.mode == Navigation.Mode.Vertical)
			{
				Fsm.Event(verticalEvent);
			}else if (selectable.navigation.mode == Navigation.Mode.Horizontal)
			{
				Fsm.Event(horizontalEvent);
			}else if (selectable.navigation.mode == Navigation.Mode.Explicit)
			{
				Fsm.Event(explicitEvent);
			}

		}
		

	}
}