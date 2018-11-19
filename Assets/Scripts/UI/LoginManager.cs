using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class LoginManager : MonoBehaviour
    {
        public RectTransform LoginFrom;
        public RectTransform RegisterFrom;
        public RectTransform RegionFrom;
        public RectTransform CharacterFrom;
        public RectTransform LoadingForm;

        #region InputField

        public InputField userNameInputField;
        public InputField passwordInputField;

        #endregion

        private void Start()
        {
            EventSystem.current.SetSelectedGameObject(userNameInputField.gameObject);
            passwordInputField.contentType = InputField.ContentType.Password;
        }


        public void LoginGame()
        {
            LoginFrom.gameObject.SetActive(false);
            RegionFrom.gameObject.SetActive(true);
        }


        public void Register()
        {
            LoginFrom.gameObject.SetActive(false);
            RegisterFrom.gameObject.SetActive(true);
        }


        public void SelectRegion()
        {
            LoginFrom.gameObject.SetActive(false);
            RegisterFrom.gameObject.SetActive(false);
            RegionFrom.gameObject.SetActive(true);
        }


        public void DecideRegion()
        {
            ToCharacter();
        }


        public void ToCharacter()
        {
            RegionFrom.gameObject.SetActive(false);
            CharacterFrom.gameObject.SetActive(true);
        }

        public void ChangeRole()
        {
        }


        public void EnterGame()
        {
            Utility.SetToCenter(LoadingForm);
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync("Main");
            LoadingScene.Instance().StartLoading(loadSceneAsync);
        }
    }
}
