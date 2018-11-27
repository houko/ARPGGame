// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
 
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("RectTransform")]
	[Tooltip("Set the local rotation of this RectTransform.")]
	public class RectTransformSetLocalRotation : BaseUpdateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The rotation. Set to none for no effect")]
		public FsmVector3 rotation;
		
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
			rotation = new FsmVector3(){UseVariable=true};
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

			Vector3 _rot = _rt.eulerAngles;

			if (!rotation.IsNone) _rot = rotation.Value;

			if (!x.IsNone) _rot.x = x.Value;
			if (!y.IsNone) _rot.y = y.Value;
			if (!z.IsNone) _rot.z = z.Value;

			_rt.eulerAngles = _rot;

		}
	}
}