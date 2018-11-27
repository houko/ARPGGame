// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Tweens the color of the CanvasRenderer color associated with this Graphic.")]
	public class UiGraphicCrossFadeColor : ComponentAction<UnityEngine.UI.Graphic>
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Graphic))]
		[Tooltip("The GameObject with a UI component.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The Color target of the UI component. Leave to none and set the individual color values, for example to affect just the alpha channel")]
		public FsmColor color;
		
		[Tooltip("The red channel Color target of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat red;
		
		[Tooltip("The green channel Color target of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat green;
		
		[Tooltip("The blue channel Color target of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat blue;
		
		[Tooltip("The alpha channel Color target of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat alpha;
	
		[Tooltip("The duration of the tween")]
		public FsmFloat duration;

		[Tooltip("Should ignore Time.scale?")]
		public FsmBool ignoreTimeScale;

		[Tooltip("Should also Tween the alpha channel?")]
		public FsmBool useAlpha;

	    private UnityEngine.UI.Graphic uiComponent;

		public override void Reset()
		{
			gameObject = null;
			color = null;
			
			red = new FsmFloat {UseVariable=true};
			green = new FsmFloat {UseVariable=true};
			blue = new FsmFloat {UseVariable=true};
			alpha = new FsmFloat {UseVariable=true};

			useAlpha = null;
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

			var _color = uiComponent.color;
			
			if (!color.IsNone)
			{
				_color = color.Value;
			}
			
			if (!red.IsNone)
			{
				_color.r = red.Value;
			}

			if (!green.IsNone)
			{
				_color.g = green.Value;
			}

			if (!blue.IsNone)
			{
				_color.b = blue.Value;
			}

			if (!alpha.IsNone)
			{
				_color.a = alpha.Value;
			}

			uiComponent.CrossFadeColor(_color, duration.Value, ignoreTimeScale.Value, useAlpha.Value);

			Finish();
		}
	}
}