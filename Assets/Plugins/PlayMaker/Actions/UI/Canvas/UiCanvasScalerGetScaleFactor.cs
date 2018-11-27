// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Get the ScaleFactor of a CanvasScaler.")]
	public class UiCanvasScalerGetScaleFactor : ComponentAction<CanvasScaler>
	{
		[RequiredField]
		[CheckForComponent(typeof(CanvasScaler))]
		[Tooltip("The GameObject with a UI CanvasScaler component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The scaleFactor of the CanvasScaler component.")]
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
			if (component != null)
			{
				scaleFactor.Value = component.scaleFactor;
			}
		}
	}
}