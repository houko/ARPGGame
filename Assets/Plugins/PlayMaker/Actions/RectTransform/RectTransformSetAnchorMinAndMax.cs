// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// Combined Action for easier use by djaydino
 
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("RectTransform")]
	[Tooltip("The normalized position in the parent RectTransform that the upper right corner is anchored to. This is relative screen space, values ranges from 0 to 1")]
	public class RectTransformSetAnchorMinAndMax : BaseUpdateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The Vector2 anchor max. Set to none for no effect, and/or set individual axis below.")]
		public FsmVector2 anchorMax;
		
		[Tooltip("The Vector2 anchor min. Set to none for no effect, and/or set individual axis below.")]
		public FsmVector2 anchorMin;
		
		[HasFloatSlider(0f,1f)]
		[Tooltip("Setting only the x value. Overrides anchorMax x value if set. Set to none for no effect")]
		public FsmFloat xMax;

		[HasFloatSlider(0f,1f)]
		[Tooltip("Setting only the x value. Overrides anchorMax x value if set. Set to none for no effect")]
		public FsmFloat yMax;
		
		[HasFloatSlider(0f,1f)]
		[Tooltip("Setting only the x value. Overrides anchorMin x value if set. Set to none for no effect")]
		public FsmFloat xMin;

		[HasFloatSlider(0f,1f)]
		[Tooltip("Setting only the x value. Overrides anchorMin x value if set. Set to none for no effect")]
		public FsmFloat yMin;
		
		
		RectTransform _rt;
		
		public override void Reset()
		{
			base.Reset();
			
			gameObject = null;
			anchorMax = null;
			anchorMin = null;
			// default axis to variable dropdown with None selected.
			xMax = new FsmFloat { UseVariable = true };
			yMax = new FsmFloat { UseVariable = true };
			xMin = new FsmFloat { UseVariable = true };
			yMin = new FsmFloat { UseVariable = true };
			
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				_rt = go.GetComponent<RectTransform>();
			}
			
			DoSetAnchorMax();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnActionUpdate()
		{
			DoSetAnchorMax();
		}
		
		void DoSetAnchorMax()
		{
			// init position	
			Vector2 _anchorMax = _rt.anchorMax;
			Vector2 _anchorMin = _rt.anchorMin;

			if (!anchorMax.IsNone)
			{
				_anchorMax = anchorMax.Value;
				_anchorMin = anchorMin.Value;
			}
			
			// override any axis
			if (!xMax.IsNone) _anchorMax.x = xMax.Value;
			if (!yMax.IsNone) _anchorMax.y = yMax.Value;
			if (!xMin.IsNone) _anchorMin.x = xMin.Value;
			if (!yMin.IsNone) _anchorMin.y = yMin.Value;
			
			// apply
			_rt.anchorMax = _anchorMax;
			_rt.anchorMin = _anchorMin;
		}
	}
}
