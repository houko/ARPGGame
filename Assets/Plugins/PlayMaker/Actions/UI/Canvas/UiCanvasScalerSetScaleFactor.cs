// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the ScaleFactor of a CanvasScaler.")]
	public class UiCanvasScalerSetScaleFactor : ComponentAction<CanvasScaler>
	{
		[RequiredField]
		[CheckForComponent(typeof(CanvasScaler))]
		[Tooltip("The GameObject with a UI CanvasScaler component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The scaleFactor of the UI CanvasScaler.")]
		public FsmFloat scaleFactor;
		
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;

	    private CanvasScaler component;

		public override void Reset()
		{
			gameObject = null;
			scaleFactor = null;

			everyFrame = false;
		}
		
		public override void OnEnter()
		{		
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				component = cachedComponent;
			}

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
			if (component != null)
			{
				component.scaleFactor = scaleFactor.Value ;
			}
		}
	}
}