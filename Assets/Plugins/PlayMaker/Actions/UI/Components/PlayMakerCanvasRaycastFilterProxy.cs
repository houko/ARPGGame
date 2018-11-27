#if !PLAYMAKER_NO_UI

using UnityEngine;

namespace HutongGames.PlayMaker
{
    public class PlayMakerCanvasRaycastFilterProxy : MonoBehaviour , ICanvasRaycastFilter
    {
        public bool RayCastingEnabled = true;

        #region ICanvasRaycastFilter implementation
        public bool IsRaycastLocationValid (Vector2 sp, Camera eventCamera)
        {
            return RayCastingEnabled;
        }
        #endregion
    }
}

#endif