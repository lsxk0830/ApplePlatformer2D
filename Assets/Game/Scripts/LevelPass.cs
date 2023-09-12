using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Blue
{
    /// <summary>
    /// 游戏通关
    /// </summary>
    public class LevelPass : MonoBehaviour
    {
        public Text LevelPassText;
        private void OnTriggerEnter2D(Collider2D other)
        {
            var currentScene = SceneManager.GetActiveScene();

            LevelPassText.gameObject.SetActive(true);

            StartCoroutine(Delay(2,()=>
            {
                SceneManager.LoadScene(currentScene.name);
            }));
        }

        private IEnumerator Delay(float seconds,Action onFinish)
        {
            yield return new WaitForSeconds(seconds);
            onFinish?.Invoke();
        }
    }
}