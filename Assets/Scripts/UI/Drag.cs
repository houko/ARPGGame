using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        {
            SetDragPosition(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SetDragPosition(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            SetDragPosition(eventData);
        }


        private void SetDragPosition(PointerEventData eventData)
        {
            Vector3 pos;
            var rt = gameObject.GetComponent<RectTransform>();
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out pos))
            {
                rt.position = pos;
            }
        }
    }
}
