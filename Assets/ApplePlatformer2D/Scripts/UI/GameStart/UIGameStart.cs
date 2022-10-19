using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UTGM
{
    public class UIGameStart : MonoBehaviour
    {
        void Start()
        {
            transform.Find("BtnStart").GetComponent<Button>().onClick.AddListener(() =>
            {
                ApplePlatformer2D.ResetGameData();

                ApplePlatformer2D.HasContinue = true;

                SceneManager.LoadScene("Game");
            });

            var btnContinueButton = transform.Find("BtnContinue").GetComponent<Button>();

            if(ApplePlatformer2D.HasContinue)
            {
                btnContinueButton.onClick.AddListener(() =>
                {
                    ApplePlatformer2D.ContinueGame();
                    SceneManager.LoadScene("Game");
                });
            }
            else
            {
                btnContinueButton.gameObject.SetActive(false);  
            }
        }
    }
}