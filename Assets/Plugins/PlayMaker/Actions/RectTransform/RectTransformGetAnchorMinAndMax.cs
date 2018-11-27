// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
// Combined Action by djaydino
 
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("RectTransform")]
	[Tooltip("The normalized position in the parent RectTransform that the upper right corner is anchored to. This is relative screen space, values ranges from 0 to 1")]
	public class RectTransformGetAnchorMinAndMax : BaseUpdateAction
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
			xMax = null;
			yMax = null;
			xMin = null;
			yMin = null;
			
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				_rt = go.GetComponent<RectTransform>();
			}
			
			DoGetValues();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnActionUpdate()
		{
			DoGetValues();
		}
		
		void DoGetValues()
		{



			// override any axis
			if (!anchorMax.IsNone) anchorMax.Value = _rt.anchorMax;
			if (!anchorMin.IsNone) anchorMin.Value = _rt.anchorMax;
			if (!xMax.IsNone) xMax.Value = _rt.anchorMax.x;
			if (!yMax.IsNone) yMax.Value = _rt.anchorMax.y;
			if (!xMin.IsNone) xMin.Value = _rt.anchorMin.x;
			if (!yMin.IsNone) yMin.Value = _rt.anchorMin.y;
		}
	}
}
