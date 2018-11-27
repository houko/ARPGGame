// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets various properties of a UI Layout Element component.")]
	public class UiLayoutElementSetValues : ComponentAction<UnityEngine.UI.LayoutElement>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.LayoutElement))]
		[Tooltip("The GameObject with the UI LayoutElement component.")]
		public FsmOwnerDefault gameObject;

		[ActionSection("Values")]

		[Tooltip("The minimum width this layout element should have.")]
		public FsmFloat minWidth;

		[Tooltip("The minimum height this layout element should have.")]
		public FsmFloat minHeight;

		[Tooltip("The preferred width this layout element should have before additional available width is allocated.")]
		public FsmFloat preferredWidth;

		[Tooltip("The preferred height this layout element should have before additional available height is allocated.")]
		public FsmFloat preferredHeight;

		[Tooltip("The relative amount of additional available width this layout element should fill out relative to its siblings.")]
		public FsmFloat flexibleWidth;

		[Tooltip("The relative amount of additional available height this layout element should fill out relative to its siblings.")]
		public FsmFloat flexibleHeight;

		[ActionSection("Options")]
		[Tooltip("Repeats every frame")]
		public bool everyFrame;
		
		private UnityEngine.UI.LayoutElement layoutElement;
	
		public override void Reset()
		{
			gameObject = null;
			minWidth = new FsmFloat {UseVariable=true};
			minHeight = new FsmFloat {UseVariable=true};
			preferredWidth = new FsmFloat {UseVariable=true};
			preferredHeight = new FsmFloat {UseVariable=true};
			flexibleWidth = new FsmFloat {UseVariable=true};
			flexibleHeight = new FsmFloat {UseVariable=true};
		}
		
		public override void OnEnter()
		{		
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				layoutElement = cachedComponent;
			}
			
			DoSetValues();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetValues();
		}

	    private void DoSetValues()
		{
		    if (layoutElement == null) return;

		    if (!minWidth.IsNone)
		    {
		        layoutElement.minWidth = minWidth.Value;
		    }

		    if (!minHeight.IsNone)
		    {
		        layoutElement.minHeight = minHeight.Value;
		    }

		    if (!preferredWidth.IsNone)
		    {
		        layoutElement.preferredWidth = preferredWidth.Value;
		    }

		    if (!preferredHeight.IsNone)
		    {
		        layoutElement.preferredHeight = preferredHeight.Value;
		    }

		    if (!flexibleWidth.IsNone)
		    {
		        layoutElement.flexibleWidth = flexibleWidth.Value;
		    }

		    if (!flexibleHeight.IsNone)
		    {
		        layoutElement.flexibleHeight = flexibleHeight.Value;
		    }
		}

	}
}