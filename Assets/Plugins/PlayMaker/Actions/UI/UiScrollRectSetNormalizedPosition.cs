// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("The normalized scroll position as a Vector2 between (0,0) and (1,1) with (0,0) being the lower left corner.")]
	public class UiScrollRectSetNormalizedPosition : ComponentAction<UnityEngine.UI.ScrollRect>
	{	
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.ScrollRect))]
		[Tooltip("The GameObject with the UI ScrollRect component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The position's value of the UI ScrollRect component. Ranges from 0.0 to 1.0.")]
		public FsmVector2 normalizedPosition;

		[Tooltip("The horizontal position's value of the UI ScrollRect component. Ranges from 0.0 to 1.0.")]
		[HasFloatSlider(0f,1f)]
		public FsmFloat horizontalPosition;
	
		[Tooltip("The vertical position's value of the UI ScrollRect component. Ranges from 0.0 to 1.0.")]
		[HasFloatSlider(0f,1f)]
		public FsmFloat verticalPosition;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.ScrollRect scrollRect;
	    private Vector2 originalValue;

		public override void Reset()
		{
			gameObject = null;
			normalizedPosition = null;
			horizontalPosition = new FsmFloat {UseVariable=true};
			verticalPosition = new FsmFloat {UseVariable=true};
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

			originalValue = scrollRect.normalizedPosition;

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
		    if (scrollRect == null) return;

		    var temp = scrollRect.normalizedPosition;

		    if (!normalizedPosition.IsNone)
		    {
		        temp = normalizedPosition.Value;
		    }

		    if (!horizontalPosition.IsNone)
		    {
		        temp.x = horizontalPosition.Value;
		    }

		    if (!verticalPosition.IsNone)
		    {
		        temp.y = verticalPosition.Value;
		    }

		    scrollRect.normalizedPosition = temp;
		}

		public override void OnExit()
		{
			if (scrollRect == null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				scrollRect.normalizedPosition = originalValue;
			}
		}
	}
}