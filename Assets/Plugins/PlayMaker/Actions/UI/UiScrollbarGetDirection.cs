// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the direction of a UGui Scrollbar component.")]
	public class UiScrollbarGetDirection : ComponentAction<UnityEngine.UI.Scrollbar>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Scrollbar))]
		[Tooltip("The GameObject with the UI Scrollbar component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the direction of the UI Scrollbar.")]
		[ObjectType(typeof(UnityEngine.UI.Scrollbar.Direction))]
		public FsmEnum direction;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

	    private UnityEngine.UI.Scrollbar scrollbar;

		public override void Reset()
		{
			gameObject = null;
			direction = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				scrollbar = cachedComponent;
			}

			DoGetValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoGetValue();
		}

	    private void DoGetValue()
		{
			if (scrollbar != null)
			{
				direction.Value = scrollbar.direction ;
			}
		}
	}
}