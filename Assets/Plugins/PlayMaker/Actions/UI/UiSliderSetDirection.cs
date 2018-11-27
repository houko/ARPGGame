// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the direction of a UI Slider component.")]
	public class UiSliderSetDirection : ComponentAction<UnityEngine.UI.Slider>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Slider))]
		[Tooltip("The GameObject with the UI Slider component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The direction of the UI Slider component.")]
		[ObjectType(typeof(UnityEngine.UI.Slider.Direction))]
		public FsmEnum  direction;

		[Tooltip("Include the  RectLayouts. Leave to none for no effect")]
		public FsmBool includeRectLayouts;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

	    private UnityEngine.UI.Slider slider;
	    private UnityEngine.UI.Slider.Direction originalValue;

		public override void Reset()
		{
			gameObject = null;
			direction = UnityEngine.UI.Slider.Direction.LeftToRight;
			includeRectLayouts = new FsmBool {UseVariable=true};
			resetOnExit = null;
		}
		
		public override void OnEnter()
		{
		    var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (UpdateCache(go))
		    {
		        slider = cachedComponent;
		    }

		    originalValue = slider.direction;

			DoSetValue();
		}

	    private void DoSetValue()
	    {
	        if (slider == null) return;

	        if (includeRectLayouts.IsNone)
	        {
	            slider.direction = (UnityEngine.UI.Slider.Direction)direction.Value;
	        }
	        else
	        {
	            slider.SetDirection((UnityEngine.UI.Slider.Direction)direction.Value, 
	                includeRectLayouts.Value);
	        }
	    }

		public override void OnExit()
		{
			if (slider==null) return;		
			
			if (resetOnExit.Value)
			{
				if (includeRectLayouts.IsNone)
				{
					slider.direction = originalValue;
				}
				else
				{
					slider.SetDirection(originalValue,includeRectLayouts.Value);
				}
			}
		}
	}
}