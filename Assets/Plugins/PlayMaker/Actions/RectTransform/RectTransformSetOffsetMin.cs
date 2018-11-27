// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
 
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("RectTransform")]
	[Tooltip("The offset of the lower left corner of the rectangle relative to the lower left anchor.")]
	public class RectTransformSetOffsetMin : BaseUpdateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The Vector2 offsetMin. Set to none for no effect, and/or set individual axis below.")]
		public FsmVector2 offsetMin;

		[Tooltip("Setting only the x value. Overrides offsetMin x value if set. Set to none for no effect")]
		public FsmFloat x;

		[Tooltip("Setting only the x value. Overrides offsetMin y value if set. Set to none for no effect")]
		public FsmFloat y;
		
		
		
		RectTransform _rt;
		
		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			offsetMin = null;
			// default axis to variable dropdown with None selected.
			x = new FsmFloat { UseVariable = true };
			y = new FsmFloat { UseVariable = true };
			
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				_rt = go.GetComponent<RectTransform>();
			}
			
			DoSetOffsetMin();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnActionUpdate()
		{
			DoSetOffsetMin();
		}
		
		void DoSetOffsetMin()
		{
			// init position	
			Vector2 _offset = _rt.offsetMin;

			if (!offsetMin.IsNone)
			{
				_offset = offsetMin.Value;
			}
			// override any axis
			if (!x.IsNone) _offset.x = x.Value;
			if (!y.IsNone) _offset.y = y.Value;
			
			// apply
			_rt.offsetMin = _offset;
		}
	}
}