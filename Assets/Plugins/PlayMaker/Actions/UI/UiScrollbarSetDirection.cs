// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the direction of a UI Scrollbar component.")]
	public class UiScrollbarSetDirection : ComponentAction<UnityEngine.UI.Scrollbar>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Scrollbar))]
		[Tooltip("The GameObject with the UI Scrollbar component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The direction of the UI Scrollbar.")]
		[ObjectType(typeof(UnityEngine.UI.Scrollbar.Direction))]
		public FsmEnum direction;

		[Tooltip("Include the  RectLayouts. Leave to none for no effect")]
		public FsmBool includeRectLayouts;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

	    private UnityEngine.UI.Scrollbar scrollbar;
	    private UnityEngine.UI.Scrollbar.Direction originalValue;

		public override void Reset()
		{
			gameObject = null;
			direction = UnityEngine.UI.Scrollbar.Direction.LeftToRight;
			includeRectLayouts = new FsmBool {UseVariable=true};
			resetOnExit = null;
		}
		
		public override void OnEnter()
		{
		    var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (UpdateCache(go))
		    {
		        scrollbar = cachedComponent;
		    }

			if (resetOnExit.Value)
			{
				originalValue = scrollbar.direction;
			}

			DoSetValue();

			Finish();
		}

	    private void DoSetValue()
	    {
	        if (scrollbar == null) return;

	        if (includeRectLayouts.IsNone)
	        {
	            scrollbar.direction = (UnityEngine.UI.Scrollbar.Direction)direction.Value;
	        }
	        else
	        {
	            scrollbar.SetDirection((UnityEngine.UI.Scrollbar.Direction)direction.Value,
	                includeRectLayouts.Value);
	        }
	    }

		public override void OnExit()
		{
			if (scrollbar==null) return;

			if (resetOnExit.Value)
			{
				if (includeRectLayouts.IsNone)
				{
					scrollbar.direction = originalValue;
				}
				else
				{
					scrollbar.SetDirection(originalValue, includeRectLayouts.Value);
				}

			}
		}
	}
}