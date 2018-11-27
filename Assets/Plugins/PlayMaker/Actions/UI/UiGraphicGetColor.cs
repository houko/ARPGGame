// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the color of a UI Graphic component. (E.g. UI Sprite)")]
	public class UiGraphicGetColor : ComponentAction<UnityEngine.UI.Graphic>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Graphic))]
		[Tooltip("The GameObject with the UI component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Color of the UI component")]
		public FsmColor color;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

	    private UnityEngine.UI.Graphic uiComponent;

		public override void Reset()
		{
			gameObject = null;
			color = null;
		}
		
		public override void OnEnter()
		{		
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				uiComponent = cachedComponent;
			}

			DoGetColorValue();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetColorValue();
		}

	    private void DoGetColorValue()
		{
			if (uiComponent!=null)
			{
				color.Value = uiComponent.color;
			}
		}

	}
}