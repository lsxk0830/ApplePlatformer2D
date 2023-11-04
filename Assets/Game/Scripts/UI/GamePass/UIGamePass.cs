using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Blue
{
    public class UIGamePass : MonoBehaviour
    {
        void Start()
        {
            transform.Find("BtnBack").GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene("GameStart");
            });

            transform.Find("PassLevelTimeText").GetComponent<Text>().text = "通关时长：" + (int)Bonfire.LiveSeconds + "s";

            // 删除存档
            ApplePlatformer2D.HasContinue = false;
        }
    }
}