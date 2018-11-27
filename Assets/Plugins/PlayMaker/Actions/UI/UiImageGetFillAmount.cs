// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
// Originally made by : DjayDino

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Set The Fill Amount on a UI Image")]
	public class UiImageGetFillAmount :  ComponentAction<UnityEngine.UI.Image>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Image))]
		[Tooltip("The GameObject with the UI Image component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The fill amount.")]
		public FsmFloat ImageFillAmount;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

	    private UnityEngine.UI.Image image;

		public override void Reset()
		{
			gameObject = null;
			ImageFillAmount = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				image = cachedComponent;
			}
			
			DoGetFillAmount();
		
			if (!everyFrame)
		    {
		        Finish();
		    }
		}
		
		public override void OnUpdate ()
		{
			DoGetFillAmount();
		}

	    private void DoGetFillAmount()
		{
			if (image!=null)
			{
				ImageFillAmount.Value =image.fillAmount;
			}
		}	
	}
}