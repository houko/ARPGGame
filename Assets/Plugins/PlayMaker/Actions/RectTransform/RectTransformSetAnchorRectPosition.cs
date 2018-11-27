// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("RectTransform")]
	[Tooltip("The position ( normalized or not) in the parent RectTransform keeping the anchor rect size intact. This lets you position the whole Rect in one go. Use this to easily animate movement (like IOS sliding UIView)")]
	public class RectTransformSetAnchorRectPosition : BaseUpdateAction
	{
		public enum AnchorReference {TopLeft,Top,TopRight,Right,BottomRight,Bottom,BottomLeft,Left,Center};

		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The reference for the given position")]
		public AnchorReference anchorReference;

		[Tooltip("Are the supplied screen coordinates normalized (0-1), or in pixels.")]
		public FsmBool normalized;

		[Tooltip("The Vector2 position, and/or set individual axis below.")]
		public FsmVector2 anchor;
		
		[HasFloatSlider(0f,1f)]
		public FsmFloat x;

		[HasFloatSlider(0f,1f)]
		public FsmFloat y;
		
		RectTransform _rt;

		Rect _anchorRect;

		public override void Reset()
		{
			base.Reset();

			normalized = true;
			gameObject = null;
			anchorReference = AnchorReference.BottomLeft;
			anchor= null;
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
			
			DoSetAnchor();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnActionUpdate()
		{
			DoSetAnchor();
		}
		
		void DoSetAnchor()
		{

			_anchorRect = new Rect();
			_anchorRect.min = _rt.anchorMin;
			_anchorRect.max = _rt.anchorMax;

			// init position	
			Vector2 _anchor = Vector2.zero;
			_anchor = _anchorRect.min;

			if (!anchor.IsNone)
			{
				if (normalized.Value)
				{
					_anchor = anchor.Value;
				}else{
					_anchor.x = anchor.Value.x / Screen.width;
					_anchor.y = anchor.Value.y / Screen.height;
				}

			}
			// override any axis
			
			if (!x.IsNone)
			{
				if (normalized.Value)
				{
				_anchor.x = x.Value;
				}else{
					_anchor.x = x.Value / Screen.width;
				}
			}

			if (!y.IsNone) 
			{
				if (normalized.Value)
				{
					_anchor.y = y.Value;
				}else{
					_anchor.y = y.Value / Screen.height;
				}
			}


			if (anchorReference == AnchorReference.BottomLeft)
			{
				_anchorRect.x = _anchor.x;
				_anchorRect.y = _anchor.y;

			}else if (anchorReference == AnchorReference.Left)
			{
				_anchorRect.x = _anchor.x;
				_anchorRect.y = _anchor.y - 0.5f;
			}else if (anchorReference == AnchorReference.TopLeft)
			{
				_anchorRect.x = _anchor.x;
				_anchorRect.y = _anchor.y - 1f;
			}else if (anchorReference == AnchorReference.Top)
			{
				_anchorRect.x = _anchor.x - 0.5f;
				_anchorRect.y = _anchor.y - 1f;
			}else if (anchorReference == AnchorReference.TopRight)
			{
				_anchorRect.x = _anchor.x - 1f;
				_anchorRect.y = _anchor.y - 1f;
			}else if (anchorReference == AnchorReference.Right)
			{
				_anchorRect.x = _anchor.x - 1f;
				_anchorRect.y = _anchor.y - 0.5f;
			}else if (anchorReference == AnchorReference.BottomRight)
			{
				_anchorRect.x = _anchor.x - 1f;
				_anchorRect.y = _anchor.y;
			}else if (anchorReference == AnchorReference.Bottom)
			{
				_anchorRect.x = _anchor.x - 0.5f;
				_anchorRect.y = _anchor.y;
			}else if (anchorReference == AnchorReference.Center)
			{
				_anchorRect.x = _anchor.x - 0.5f;
				_anchorRect.y = _anchor.y - 0.5f;
			}

		
			// apply
			_rt.anchorMin = _anchorRect.min;
			_rt.anchorMax = _anchorRect.max;
		}
	}
}