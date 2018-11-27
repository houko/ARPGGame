// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the UI ScrollRect horizontal flag")]
	public class UiScrollRectSetHorizontal : ComponentAction<UnityEngine.UI.ScrollRect>
	{	
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.ScrollRect))]
		[Tooltip("The GameObject with the UI ScrollRect component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The horizontal flag")]
		public FsmBool horizontal;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.ScrollRect scrollRect;
	    private bool originalValue;

		public override void Reset()
		{
			gameObject = null;
			horizontal = null;
			resetOnExit = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
		    var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (UpdateCache(go))
		    {
		        scrollRect = cachedComponent;
		    }

			originalValue = scrollRect.vertical;

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
			if (scrollRect != null)
			{
				scrollRect.horizontal = horizontal.Value;
			}
		}

		public override void OnExit()
		{
			if (scrollRect == null) return;
			
			if (resetOnExit.Value)
			{
				scrollRect.horizontal = originalValue;
			}
		}
	}
}