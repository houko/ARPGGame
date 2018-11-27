using HedgehogTeam.EasyTouch;
using UnityEngine;

namespace CameraController
{
    public class CameraController : MonoBehaviour
    {
        //摄像机对准目标
        private GameObject mTarget;

        //移动速度
        public float moveSpeed;

        //缩放速度
        public float scaleSpeed;

        //摄像机与目标位置偏移
        private Vector3 mOffset;

        //单例
        public static CameraController _instance;


        private void OnEnable()
        {
            EasyTouch.On_Swipe += On_Swipe;
            EasyTouch.On_Drag += On_Drag;
            EasyTouch.On_Pinch += On_Pinch;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            mTarget = GameObject.FindWithTag("Player");
            //初始化摄像机与目标位置偏移
            mOffset = transform.position - mTarget.transform.position;
        }


        /// <summary>
        /// 清除注册事件
        /// </summary>
        private void OnDestroy()
        {
            EasyTouch.On_Swipe -= On_Swipe;
            EasyTouch.On_Drag -= On_Drag;
        }

        /// <summary>
        /// 拖动事件
        /// </summary>
        /// <param name="gesture"></param>
        private void On_Drag(Gesture gesture)
        {
            On_Swipe(gesture);
        }


        /// <summary>
        /// 控制摄像机锁定目标玩家
        /// </summary>
        public void LockTarget()
        {
            //目标不为空则移动摄像机
            if (mTarget != null)
            {
                //计算摄像机当前位置
                Vector3 currentPosition = mTarget.transform.position + mOffset;
                //移动摄像机
                transform.position = currentPosition;
            }
        }


        /// <summary>
        ///控制视野移动
        /// </summary>
        /// <param name="gesture"></param>
        private void On_Swipe(Gesture gesture)
        {
            transform.Translate(Vector3.left * gesture.deltaPosition.x / Screen.width * moveSpeed, Space.World);
            transform.Translate(Vector3.back * gesture.deltaPosition.y / Screen.height * moveSpeed, Space.World);
        }

        /// <summary>
        /// 双指滑动,控制视野缩放
        /// </summary>
        /// <param name="gesture"></param>
        private void On_Pinch(Gesture gesture)
        {
            // Camera.main.fieldOfView += gesture.deltaPinch * Time.deltaTime;
            transform.position += transform.forward * gesture.deltaPinch * Time.deltaTime * scaleSpeed;
        }
    }
}
