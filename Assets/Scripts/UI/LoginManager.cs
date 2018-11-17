using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LoginManager : MonoBehaviour
    {
        public RectTransform LoginFrom;
        public RectTransform RegisterFrom;
        public RectTransform RegionFrom;
        public RectTransform CharacterFrom;






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
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync("Main");
        }
        
        
        
        
    }
}
