// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the minimum and maximum limits for the value of a UI Slider component. Optionally resets on exit")]
	public class UiSliderSetMinMax : ComponentAction<UnityEngine.UI.Slider>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Slider))]
		[Tooltip("The GameObject with the UI Slider component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The minimum value of the UI Slider component. Leave as None for no effect")]
		public FsmFloat minValue;

		[Tooltip("The maximum value of the UI Slider component. Leave as None for no effect")]
		public FsmFloat maxValue;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.Slider slider;

	    private float originalMinValue;
	    private float originalMaxValue;

		public override void Reset()
		{
			gameObject = null;
			minValue = new FsmFloat {UseVariable=true};
			maxValue = new FsmFloat {UseVariable=true};
			resetOnExit = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
		    var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (UpdateCache(go))
		    {
		        slider = cachedComponent;
		    }

			if (resetOnExit.Value)
			{
				originalMinValue = slider.minValue;
				originalMaxValue = slider.maxValue;
			}

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
		    if (slider == null) return;

		    if (!minValue.IsNone)
		    {
		        slider.minValue = minValue.Value;
		    }
		    if (!maxValue.IsNone)
		    {
		        slider.maxValue = maxValue.Value;
		    }
		}

		public override void OnExit()
		{
		    if (slider == null) return;
			
			if (resetOnExit.Value)
			{
				slider.minValue = originalMinValue;
				slider.maxValue = originalMaxValue;
			}
		}
	}
}