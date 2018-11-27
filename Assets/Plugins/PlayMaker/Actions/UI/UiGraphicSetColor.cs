// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// based on Sebastio work: http://hutonggames.com/playmakerforum/index.php?topic=8452.msg42858#msg42858

using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Set Graphic Color. E.g. to set Sprite Color.")]
	public class UiGraphicSetColor : ComponentAction<Graphic>
	{
		[RequiredField]
		[CheckForComponent(typeof(Graphic))]
		[Tooltip("The GameObject with a UI component.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The Color of the UI component. Leave to none and set the individual color values, for example to affect just the alpha channel")]
		public FsmColor color;
		
		[Tooltip("The red channel Color of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat red;
		
		[Tooltip("The green channel Color of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat green;
		
		[Tooltip("The blue channel Color of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat blue;
		
		[Tooltip("The alpha channel Color of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat alpha;
		
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
		
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;

	    private Graphic uiComponent;
	    private Color originalColor;

		public override void Reset()
		{
			gameObject = null;
			color = null;
			
			red = new FsmFloat {UseVariable=true};
			green = new FsmFloat {UseVariable=true};
			blue = new FsmFloat {UseVariable=true};
			alpha = new FsmFloat {UseVariable=true};
			
			resetOnExit = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				uiComponent = cachedComponent;
			}

			originalColor = uiComponent.color;

			DoSetColorValue();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetColorValue();
		}

	    private void DoSetColorValue()
		{
		    if (uiComponent == null) return;

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

		    uiComponent.color = _color;
		}

		public override void OnExit()
		{
			if (uiComponent==null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				uiComponent.color = originalColor;
			}
		}
		
	}
}