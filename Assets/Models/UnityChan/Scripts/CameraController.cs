//CameraController.cs for UnityChan
//Original Script is here:
//TAK-EMI / CameraController.cs
//https://gist.github.com/TAK-EMI/d67a13b6f73bed32075d
//https://twitter.com/TAK_EMI
//
//Revised by N.Kobayashi 2014/5/15 
//Change : To prevent rotation flips on XY plane, use Quaternion in cameraRotate()
//Change : Add the instrustion window
//Change : Add the operation for Mac
//


using UnityEngine;

namespace CameraController
{
    enum MouseButtonDown
    {
        MBD_LEFT = 0,
        MBD_RIGHT,
        MBD_MIDDLE
    }

    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector3 focus = Vector3.zero;
        [SerializeField] private GameObject focusObj;

        public bool showInstWindow = true;

        private Vector3 oldPos;

        void setupFocusObject(string name)
        {
            GameObject obj = focusObj = new GameObject(name);
            obj.transform.position = focus;
            obj.transform.LookAt(transform.position);
        }

        void Start()
        {
            if (focusObj == null)
                setupFocusObject("CameraFocusObject");

            Transform trans = transform;
            transform.parent = focusObj.transform;

            trans.LookAt(focus);
        }

//		void Update ()
//		{
//			this.mouseEvent();
//		}

//		//Show Instrustion Window
//		void OnGUI()
//		{
//			if(showInstWindow){
//				GUI.Box(new Rect(Screen.width -210, Screen.height - 100, 200, 90), "Camera Operations");
//				GUI.Label(new Rect(Screen.width -200, Screen.height - 80, 200, 30),"RMB / Alt+LMB: Tumble");
//				GUI.Label(new Rect(Screen.width -200, Screen.height - 60, 200, 30),"MMB / Alt+Cmd+LMB: Track");
//				GUI.Label(new Rect(Screen.width -200, Screen.height - 40, 200, 30),"Wheel / 2 Fingers Swipe: Dolly");
//			}
//
//		}

//		void mouseEvent()
//		{
//			float delta = Input.GetAxis("Mouse ScrollWheel");
//			if (Math.Abs(delta) > 0f)
//				this.mouseWheelEvent(delta);
//
//			if (Input.GetMouseButtonDown((int)MouseButtonDown.MBD_LEFT) ||
//				Input.GetMouseButtonDown((int)MouseButtonDown.MBD_MIDDLE) ||
//				Input.GetMouseButtonDown((int)MouseButtonDown.MBD_RIGHT))
//				this.oldPos = Input.mousePosition;
//
//			this.mouseDragEvent(Input.mousePosition);
//
//			return;
//		}

//		void mouseDragEvent(Vector3 mousePos)
//		{
//			Vector3 diff = mousePos - oldPos;
//
//			if(Input.GetMouseButton((int)MouseButtonDown.MBD_LEFT))
//			{
//				//Operation for Mac : "Left Alt + Left Command + LMB Drag" is Track
//				if(Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.LeftCommand))
//				{
//					if (diff.magnitude > Vector3.kEpsilon)
//						cameraTranslate(-diff / 100.0f);
//				}
//				//Operation for Mac : "Left Alt + LMB Drag" is Tumble
//				else if (Input.GetKey(KeyCode.LeftAlt))
//				{
//					if (diff.magnitude > Vector3.kEpsilon)
//						cameraRotate(new Vector3(diff.y, diff.x, 0.0f));
//				}
//				//Only "LMB Drag" is no action.
//			}
//			//Track
//			else if (Input.GetMouseButton((int)MouseButtonDown.MBD_MIDDLE))
//			{
//				if (diff.magnitude > Vector3.kEpsilon)
//					cameraTranslate(-diff / 100.0f);
//			}
//			//Tumble
//			else if (Input.GetMouseButton((int)MouseButtonDown.MBD_RIGHT))
//			{
//				if (diff.magnitude > Vector3.kEpsilon)
//					cameraRotate(new Vector3(diff.y, diff.x, 0.0f));
//			}
//				
//			oldPos = mousePos;
//		}
//
//		//Dolly
//		public void mouseWheelEvent(float delta)
//		{
//			Vector3 focusToPosition = transform.position - focus;
//
//			Vector3 post = focusToPosition * (1.0f + delta);
//
//			if (post.magnitude > 0.01)
//				transform.position = focus + post;
//		}

        void cameraTranslate(Vector3 vec)
        {
            Transform focusTrans = focusObj.transform;

            vec.x *= -1;

            focusTrans.Translate(Vector3.right * vec.x);
            focusTrans.Translate(Vector3.up * vec.y);

            focus = focusTrans.position;
        }

        public void cameraRotate(Vector3 eulerAngle)
        {
            //Use Quaternion to prevent rotation flips on XY plane
            Quaternion q = Quaternion.identity;

            Transform focusTrans = focusObj.transform;
            focusTrans.localEulerAngles = focusTrans.localEulerAngles + eulerAngle;

            //Change this.transform.LookAt(this.focus) to q.SetLookRotation(this.focus)
            q.SetLookRotation(focus);
        }
    }
}