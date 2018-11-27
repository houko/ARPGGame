// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Rebuild a UI Graphic component.")]
	public class UiRebuild: ComponentAction<UnityEngine.UI.Graphic>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Graphic))]
		[Tooltip("The GameObject with the UI Graphic component.")]
		public FsmOwnerDefault gameObject;

		public UnityEngine.UI.CanvasUpdate canvasUpdate;

		[Tooltip("Only Rebuild when state exits.")]
		public bool rebuildOnExit;

		private UnityEngine.UI.Graphic graphic;

		public override void Reset()
		{
			gameObject = null;
			canvasUpdate = UnityEngine.UI.CanvasUpdate.LatePreRender;
			rebuildOnExit = false;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				graphic = cachedComponent;
			}

			if(!rebuildOnExit)
			{
				DoAction();
			}

			Finish();
		}

	    private void DoAction()
		{
			if (graphic!=null)
			{
				graphic.Rebuild(canvasUpdate);
			}
		}

		public override void OnExit()
		{
			if (rebuildOnExit)
			{
				DoAction();
			}
		}

	}
}