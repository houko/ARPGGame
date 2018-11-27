// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
 
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("RectTransform")]
	[Tooltip("Set the local position of this RectTransform.")]
	public class RectTransformSetLocalPosition : BaseUpdateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The position. Set to none for no effect")]
		public FsmVector2 position2d;

		[Tooltip("Or the 3d position. Set to none for no effect")]
		public FsmVector3 position;
		
		[Tooltip("The x component of the rotation. Set to none for no effect")]
		public FsmFloat x;
		
		[Tooltip("The y component of the rotation. Set to none for no effect")]
		public FsmFloat y;

		[Tooltip("The z component of the rotation. Set to none for no effect")]
		public FsmFloat z;
		
		RectTransform _rt;
		
		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			position2d  = new FsmVector2(){UseVariable=true};
			position = new FsmVector3(){UseVariable=true};
			x = new FsmFloat(){UseVariable=true};
			y = new FsmFloat(){UseVariable=true};
			z = new FsmFloat(){UseVariable=true};
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				_rt = go.GetComponent<RectTransform>();
			}
			
			DoSetValues();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnActionUpdate()
		{
			DoSetValues();
		}
		
		void DoSetValues()
		{
			if (_rt==null)
			{
				return;
			}

			Vector3 _pos = _rt.localPosition;

			if (!position.IsNone) _pos = position.Value;

			if (!position2d.IsNone)
			{
				_pos.x = position2d.Value.x;
				_pos.y = position2d.Value.y;
			}

			if (!x.IsNone) _pos.x = x.Value;
			if (!y.IsNone) _pos.y = y.Value;
			if (!z.IsNone) _pos.z = z.Value;

			_rt.localPosition = _pos;

		}
	}
}