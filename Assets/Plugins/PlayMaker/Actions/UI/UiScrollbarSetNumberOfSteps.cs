// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the number of distinct scroll positions allowed for a UI Scrollbar component.")]
	public class UiScrollbarSetNumberOfSteps : ComponentAction<UnityEngine.UI.Scrollbar>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Scrollbar))]
		[Tooltip("The GameObject with the UI Scrollbar component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The number of distinct scroll positions allowed for the UI Scrollbar.")]
		public FsmInt value;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.Scrollbar scrollbar;
	    private int originalValue;

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
		        scrollbar = cachedComponent;
		    }

			originalValue = scrollbar.numberOfSteps;

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
			if (scrollbar != null)
			{
				scrollbar.numberOfSteps = value.Value;
			}
		}

		public override void OnExit()
		{
			if (scrollbar == null) return;
			
			if (resetOnExit.Value)
			{
				scrollbar.numberOfSteps = originalValue;
			}
		}
	}
}