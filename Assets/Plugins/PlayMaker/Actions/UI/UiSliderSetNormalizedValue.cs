// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the normalized value ( between 0 and 1) of a UI Slider component.")]
	public class UiSliderSetNormalizedValue : ComponentAction<UnityEngine.UI.Slider>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Slider))]
		[Tooltip("The GameObject with the UI Slider component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[HasFloatSlider(0f,1f)]
		[Tooltip("The normalized value ( between 0 and 1) of the UI Slider component.")]
		public FsmFloat value;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.Slider slider;
	    private float originalValue;

		public override void Reset()
		{
			gameObject = null;
			value = null;
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

			originalValue = slider.normalizedValue;

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
			if (slider != null)
			{
				slider.normalizedValue = value.Value;
			}
		}

		public override void OnExit()
		{
			if (slider==null) return;
			
			if (resetOnExit.Value)
			{
				slider.normalizedValue = originalValue;
			}
		}
	}
}