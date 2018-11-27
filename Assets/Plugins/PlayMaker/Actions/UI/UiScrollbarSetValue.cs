// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the position value of a UI Scrollbar component. Ranges from 0.0 to 1.0.")]
	public class UiScrollbarSetValue : ComponentAction<UnityEngine.UI.Scrollbar>
	{	
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Scrollbar))]
		[Tooltip("The GameObject with the UI Scrollbar component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The position's value of the UI Scrollbar component. Ranges from 0.0 to 1.0.")]
		[HasFloatSlider(0f,1f)]
		public FsmFloat value;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.Scrollbar scrollbar;
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
		        scrollbar = cachedComponent;
		    }

			originalValue = scrollbar.value;

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
				scrollbar.value = value.Value;
			}
		}

		public override void OnExit()
		{
			if (scrollbar==null) return;
			
			if (resetOnExit.Value)
			{
				scrollbar.value = originalValue;
			}
		}
	}
}