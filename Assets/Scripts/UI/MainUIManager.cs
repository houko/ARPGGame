using UnityEngine;

namespace UI
{
    public class MainUIManager : MonoBehaviour
    {
        public RectTransform EquipFrom;
        public RectTransform BagFrom;


        public void ToggleEquip()
        {
            EquipFrom.gameObject.SetActive(!EquipFrom.gameObject.activeSelf);
        }

        public void ToggleBag()
        {
            BagFrom.gameObject.SetActive(!BagFrom.gameObject.activeSelf);
        }


        public void CloseBag()
        {
            BagFrom.gameObject.SetActive(false);
        }
    }
}
