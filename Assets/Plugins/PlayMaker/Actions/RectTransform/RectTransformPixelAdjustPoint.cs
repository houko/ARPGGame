// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
 
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("RectTransform")]
	[Tooltip("Convert a given point in screen space into a pixel correct point.")]
	public class RectTransformPixelAdjustPoint : BaseUpdateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[CheckForComponent(typeof(Canvas))]
		[Tooltip("The canvas. Leave to none to use the canvas of the gameObject")]
		public FsmGameObject canvas;

		[Tooltip("The screen position.")]
		public FsmVector2 screenPoint;

		[ActionSection("Result")]
		[RequiredField]
		[Tooltip("Pixel adjusted point from the screen position.")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 pixelPoint;

		RectTransform _rt;
		Canvas _canvas;

		
		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			canvas = new FsmGameObject(){UseVariable=true};
			
			screenPoint = null;
			pixelPoint = null;
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				_rt = go.GetComponent<RectTransform>();
			}
			
			GameObject goCanvas = canvas.Value;
			if (goCanvas != null)
			{
				_canvas = goCanvas.GetComponent<Canvas>();
			}

			if (_canvas==null && go!=null)
			{
				var _ui = go.GetComponent<UnityEngine.UI.Graphic>();
				if (_ui!=null)
				{
					_canvas = _ui.canvas;
				}
			}
			
			DoAction();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnActionUpdate()
		{
			DoAction();
		}
		
		
		void DoAction()
		{
			pixelPoint.Value = RectTransformUtility.PixelAdjustPoint(screenPoint.Value,_rt,_canvas);
		}
	
	}
}