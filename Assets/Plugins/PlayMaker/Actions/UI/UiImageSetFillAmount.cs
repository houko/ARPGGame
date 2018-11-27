// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
// Originally made by : DjayDino

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Set The Fill Amount on a UI Image")]
	public class UiImageSetFillAmount : ComponentAction<UnityEngine.UI.Image>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Image))]
		[Tooltip("The GameObject with the UI Image component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[HasFloatSlider(0,1)]
		[Tooltip("The fill amount.")]
		public FsmFloat ImageFillAmount;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

	    private UnityEngine.UI.Image image;

		public override void Reset()
		{
			gameObject = null;
			ImageFillAmount = 1;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				image = cachedComponent;
			}
			
			DoSetFillAmount();
		
			if (!everyFrame)
		    {
		        Finish();
		    }
		}
		
		public override void OnUpdate ()
		{
			DoSetFillAmount();
		}

	    private void DoSetFillAmount()
		{
			if (image!=null)
			{
				image.fillAmount = ImageFillAmount.Value;
			}
		}

		
	}
}