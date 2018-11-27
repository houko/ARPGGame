// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("RectTransform")]
	[Tooltip("Check if a RectTransform contains the screen point as seen from the given camera.")]
	public class RectTransformContainsScreenPoint : FsmStateAction
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

		[Tooltip("Store the result.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isContained;

		[Tooltip("Event sent if screenPoint is contained in RectTransform.")]
		public FsmEvent isContainedEvent;

		[Tooltip("Event sent if screenPoint is NOT contained in RectTransform.")]
		public FsmEvent isNotContainedEvent;

		RectTransform _rt;
		Camera _camera;

		public override void Reset()
		{
			gameObject = null;
			screenPointVector2 = null;
			orScreenPointVector3 = new FsmVector3(){UseVariable=true};
			normalizedScreenPoint = false;
			camera = null;
			everyFrame = false;

			isContained = null;
			isContainedEvent = null;
			isNotContainedEvent = null;
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

			bool _result = RectTransformUtility.RectangleContainsScreenPoint(_rt,_screenPoint,_camera);
			if (!isContained.IsNone)
			{
				isContained.Value = _result;
			}

			if (_result)
			{
				if (isContainedEvent!=null)	Fsm.Event(isContainedEvent);
			}else{
				if (isNotContainedEvent!=null)	Fsm.Event(isNotContainedEvent);
			}

		}
		
		
	}
}