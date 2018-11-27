// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the minimum and maximum limits for the value of a UI Slider component.")]
	public class UiSliderGetMinMax :  ComponentAction<UnityEngine.UI.Slider>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Slider))]
		[Tooltip("The GameObject with the UI Slider component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the minimum value of the UI Slider.")]
		public FsmFloat minValue;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the maximum value of the UI Slider.")]
		public FsmFloat maxValue;

		private UnityEngine.UI.Slider slider;

		public override void Reset()
		{
			gameObject = null;
			minValue = null;
			maxValue = null;
		}
		
		public override void OnEnter()
		{
		    var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (UpdateCache(go))
		    {
		        slider = cachedComponent;
		    }

			DoGetValue();
		}

	    private void DoGetValue()
		{
			if (slider != null)
			{
				if (!minValue.IsNone)
				{
					minValue.Value = slider.minValue;
				}
				if (!maxValue.IsNone)
				{
					maxValue.Value = slider.maxValue;
				}
			}
		}

	}
}