using UnityEngine;

namespace UI
{
    public class MainUIManager : MonoBehaviour
    {
        public RectTransform EquipFrom;


        public void ToggleEquip()
        {
            EquipFrom.gameObject.SetActive(!EquipFrom.gameObject.active);
        }
    }
}
