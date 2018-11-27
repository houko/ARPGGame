// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
 
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("RectTransform")]
	[Tooltip("	The offset of the upper right corner of the rectangle relative to the upper right anchor.")]
	public class RectTransformSetOffsetMax : BaseUpdateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The Vector2 offsetMax. Set to none for no effect, and/or set individual axis below.")]
		public FsmVector2 offsetMax;

		[Tooltip("Setting only the x value. Overrides offsetMax x value if set. Set to none for no effect")]
		public FsmFloat x;

		[Tooltip("Setting only the y value. Overrides offsetMax y value if set. Set to none for no effect")]
		public FsmFloat y;
		
		
		
		RectTransform _rt;
		
		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			offsetMax = null;
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
			
			DoSetOffsetMax();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnActionUpdate()
		{
			DoSetOffsetMax();
		}
		
		void DoSetOffsetMax()
		{
			// init position	
			Vector2 _offset = _rt.offsetMax;

			if (!offsetMax.IsNone)
			{
				_offset = offsetMax.Value;
			}
			// override any axis
			if (!x.IsNone) _offset.x = x.Value;
			if (!y.IsNone) _offset.y = y.Value;
			
			// apply
			_rt.offsetMax = _offset;
		}
	}
}