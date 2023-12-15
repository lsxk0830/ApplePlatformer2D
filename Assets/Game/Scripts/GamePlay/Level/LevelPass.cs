using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Blue
{
    /// <summary>
    /// 游戏通关
    /// </summary>
    public class LevelPass : MonoBehaviour
    {
        public Text LevelPassText;
        public UnityEvent OnLevelPass;
        public UnityEvent OnLevelPassDelayFinish; // 通关延时完成事件
        public bool ResetPlayerOriginPoint = false; // 玩家是否回到原点
        public bool TriggerEventToController = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            LevelPassText.gameObject.SetActive(true);
            OnLevelPass?.Invoke();

            if(TriggerEventToController)
                LevelController.PassCurrentLevel();

            StartCoroutine(Delay(2, () =>
            {
                if (ResetPlayerOriginPoint)
                {
                    var player = GameObject.FindWithTag("Player");
                    player.transform.position = Vector2.zero;

                    var trigger2D = player.transform.Find("GroundCheck").GetComponent<Trigger2D>();
                    trigger2D.Reset();
                }

                OnLevelPassDelayFinish?.Invoke();
            }));

        }

        private IEnumerator Delay(float seconds, Action onFinish)
        {
            yield return new WaitForSeconds(seconds);
            onFinish?.Invoke();
        }
    }
}