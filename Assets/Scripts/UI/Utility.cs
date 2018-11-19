using UnityEngine;

namespace UI
{
    public class Utility : MonoBehaviour
    {
        /// <summary>
        /// 把目标UI设置到屏中间
        /// </summary>
        /// <param name="rectTrans"></param>
        public static void SetToCenter(RectTransform rectTrans)
        {
            rectTrans.localPosition = Vector3.zero;
        }
    }
}
