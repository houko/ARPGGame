using System.Security.Cryptography;
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


        /// <summary>
        /// 登陆按钮触发
        /// </summary>
        public void LoginGame()
        {
            Utility.SetToCenter(RegionFrom);
            LoginFrom.gameObject.SetActive(false);
            RegionFrom.gameObject.SetActive(true);
        }


        public void ToLogin()
        {
            Utility.SetToCenter(RegisterFrom);
            LoginFrom.gameObject.SetActive(true);
            RegisterFrom.gameObject.SetActive(false);
        }


        /// <summary>
        /// 注册按钮触发（到注册页面）
        /// </summary>
        public void Register()
        {
            Utility.SetToCenter(RegisterFrom);
            LoginFrom.gameObject.SetActive(false);
            RegisterFrom.gameObject.SetActive(true);
        }


        /// <summary>
        /// 选区按钮触发（进入选区按钮）
        /// </summary>
        public void SelectRegion()
        {
            Utility.SetToCenter(RegionFrom);
            LoginFrom.gameObject.SetActive(false);
            RegisterFrom.gameObject.SetActive(false);
            RegionFrom.gameObject.SetActive(true);
        }


        /// <summary>
        /// 确定选区
        /// </summary>
        public void DecideRegion()
        {
            Utility.SetToCenter(CharacterFrom);
            ToCharacter();
        }


        /// <summary>
        /// 到角色选择界面
        /// </summary>
        public void ToCharacter()
        {
            Utility.SetToCenter(CharacterFrom);
            RegionFrom.gameObject.SetActive(false);
            CharacterFrom.gameObject.SetActive(true);
        }


        /// <summary>
        /// 选角
        /// </summary>
        public void ChangeRole()
        {
            Utility.SetToCenter(LoadingForm);
        }


        /// <summary>
        /// 进入Main场景
        /// </summary>
        public void EnterGame()
        {
            Utility.SetToCenter(LoadingForm);
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync("Main");
            LoadingScene.Instance().StartLoading(loadSceneAsync);
        }
    }
}
