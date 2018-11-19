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
            rectTrans.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
            rectTrans.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
            rectTrans.anchorMin = Vector2.zero;
            rectTrans.anchorMax = Vector2.one;
        }
    }
}
