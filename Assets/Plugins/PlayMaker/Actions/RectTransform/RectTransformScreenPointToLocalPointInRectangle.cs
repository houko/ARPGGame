// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
 
using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("RectTransform")]
	[Tooltip("Transform a screen space point to a local position that is on the plane of the given RectTransform. Also check if the plane of the RectTransform is hit, regardless of whether the point is inside the rectangle.")]
	public class RectTransformScreenPointToLocalPointInRectangle : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The screenPoint as a Vector2. Leave to none if you want to use the Vector3 alternative")]
		public FsmVector2 screenPointVector2;

		[Tooltip("The screenPoint as a Vector3. Leave to none if you want to use the Vector2 alternative")]
		public FsmVector3 orScreenPointVector3;

		[Tooltip("Define if screenPoint are expressed as normalized screen coordinates (0-1). Otherwise coordinates are in pixels.")]
		public bool normalizedScreenPoint;
		
		[Tooltip("The Camera. For a RectTransform in a Canvas set to Screen Space - Overlay mode, the cam parameter should be set to null explicitly (default).\n" +
		         "Leave to none and the camera will be the one from EventSystem.current.camera")]
		[CheckForComponent(typeof(Camera))]
		public FsmGameObject camera;

		[Tooltip("Repeat every frame")]
		public  bool everyFrame;

		[ActionSection("Result")]

		[Tooltip("Store the local Position as a vector3 of the screenPoint on the RectTransform Plane.")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 localPosition;

		[Tooltip("Store the local Position as a vector2 of the screenPoint on the RectTransform Plane.")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 localPosition2d;

		[Tooltip("True if the plane of the RectTransform is hit, regardless of whether the point is inside the rectangle.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isHit;

		[Tooltip("Event sent if the plane of the RectTransform is hit, regardless of whether the point is inside the rectangle.")]
		public FsmEvent hitEvent;

		[Tooltip("Event sent if the plane of the RectTransform is NOT hit, regardless of whether the point is inside the rectangle.")]
		public FsmEvent noHitEvent;

		RectTransform _rt;
		Camera _camera;

		public override void Reset()
		{
			gameObject = null;
			screenPointVector2 = null;
			orScreenPointVector3 = new FsmVector3(){UseVariable=true};
			normalizedScreenPoint = false;
			camera = new FsmGameObject(){UseVariable=true};
			everyFrame = false;

			localPosition = null;
			localPosition2d = null;

			isHit = null;
			hitEvent = null;
			noHitEvent = null;
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				_rt = go.GetComponent<RectTransform>();
			}

			if (!camera.IsNone)
			{
				_camera = camera.Value.GetComponent<Camera>();
			}else{
				_camera = EventSystem.current.GetComponent<Camera>();
			}

			DoCheck();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoCheck();
		}

		void DoCheck()
		{
			if (_rt==null)
			{
				return;
			}

			Vector2 _screenPoint = screenPointVector2.Value;
			if (!orScreenPointVector3.IsNone)
			{
				_screenPoint.x = orScreenPointVector3.Value.x;
				_screenPoint.y = orScreenPointVector3.Value.y;
			}

			if (normalizedScreenPoint)
			{
				_screenPoint.x *= Screen.width;
				_screenPoint.y *= Screen.height;
			}
			Vector2 _localPosition;

			bool _result = false;

			_result = RectTransformUtility.ScreenPointToLocalPointInRectangle(_rt,_screenPoint,_camera,out _localPosition);

			if (!localPosition2d.IsNone)
			{
				localPosition2d.Value = _localPosition;
			}

			if (!localPosition.IsNone)
			{
				localPosition.Value = new Vector3(_localPosition.x,_localPosition.y,0f);
			}

			if (!isHit.IsNone)
			{
				isHit.Value = _result;
			}

			if (_result)
			{
				if (hitEvent!=null)	Fsm.Event(hitEvent);
			}else{
				if (noHitEvent!=null)	Fsm.Event(noHitEvent);
			}

		}
		
		
	}
}