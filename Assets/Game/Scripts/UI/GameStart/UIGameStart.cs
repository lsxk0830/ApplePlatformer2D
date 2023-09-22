using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Blue
{
    public class UIGameStart : MonoBehaviour
    {
        void Start()
        {
            transform.Find("BtnStart").GetComponent<Button>().onClick.AddListener(()=>
            {
                ApplePlatformer2D.ResetGameData();
                ApplePlatformer2D.HasContinue = true;
                SceneManager.LoadScene("Game");
            });
            var btnContinue = transform.Find("BtnContinue").GetComponent<Button>();
            if(ApplePlatformer2D.HasContinue)
            {
                btnContinue.onClick.AddListener(()=>
                {
                    ApplePlatformer2D.ContinueGame();
                    SceneManager.LoadScene("Game");
                });
            }
            else
            {
                btnContinue.gameObject.SetActive(false);
            }
        }
    }
}