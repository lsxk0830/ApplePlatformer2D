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
                SceneManager.LoadScene("Game");
            });
        }
    }
}