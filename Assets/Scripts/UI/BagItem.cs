using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class BagItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private GameObject desc;


        private void Awake()
        {
            desc = transform.Find("Desc").gameObject;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            desc.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            desc.SetActive(false);
        }
    }
}
