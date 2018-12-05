using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class BagItem : MonoBehaviour, IPointerClickHandler
    {
        private GameObject desc;


        private void Awake()
        {
            desc = transform.Find("Desc").gameObject;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            desc.SetActive(!desc.activeSelf);
        }
    }
}
