// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the wholeNumbers property of a UI Slider component. If true, the Slider is constrained to integer values")]
	public class UiSliderGetWholeNumbers : ComponentAction<UnityEngine.UI.Slider>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Slider))]
		[Tooltip("The GameObject with the UI Slider component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("Is the Slider constrained to integer values?")]
		public FsmBool wholeNumbers;

		[Tooltip("Event sent if slider is showing integers")]
		public FsmEvent isShowingWholeNumbersEvent;
		
		[Tooltip("Event sent if slider is showing floats")]
		public FsmEvent isNotShowingWholeNumbersEvent;

		private UnityEngine.UI.Slider slider;

		public override void Reset()
		{
			gameObject = null;
			isShowingWholeNumbersEvent = null;
			isNotShowingWholeNumbersEvent = null;
			wholeNumbers = null;
		}
		
		public override void OnEnter()
		{
		    var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (UpdateCache(go))
		    {
		        slider = cachedComponent;
		    }

			DoGetValue();

			Finish();
		}

	    private void DoGetValue()
		{
			var val = false;

			if (slider != null)
			{
				val = slider.wholeNumbers;
			}
		
			wholeNumbers.Value = val;

		    Fsm.Event(val ? isShowingWholeNumbersEvent : 
		        isNotShowingWholeNumbersEvent);
		}
	}
}