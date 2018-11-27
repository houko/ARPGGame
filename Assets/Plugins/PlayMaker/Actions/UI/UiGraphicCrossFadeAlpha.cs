// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Tweens the alpha of the CanvasRenderer color associated with this Graphic.")]
	public class UiGraphicCrossFadeAlpha : ComponentAction<UnityEngine.UI.Graphic>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Graphic))]
		[Tooltip("The GameObject with an Unity UI component.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The alpha target")]
		public FsmFloat alpha;
	
		[Tooltip("The duration of the tween")]
		public FsmFloat duration;

		[Tooltip("Should ignore Time.scale?")]
		public FsmBool ignoreTimeScale;

	    private UnityEngine.UI.Graphic uiComponent;


		public override void Reset()
		{
			gameObject = null;
			alpha = null;

			duration = 1f;
		
			ignoreTimeScale = null;
		}
		
		public override void OnEnter()
		{		
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				uiComponent = cachedComponent;
			}

			uiComponent.CrossFadeAlpha(alpha.Value, duration.Value, ignoreTimeScale.Value);

			Finish();
		}
	}
}