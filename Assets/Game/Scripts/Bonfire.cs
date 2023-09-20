using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Blue
{
    public class Bonfire : MonoBehaviour
    {
        public GameObject KeyTips;

        private bool mPlayerEntered = false; // 主角是否进入

        private void Start()
        {
            // 添加规则
            mRules.Add(new HPBar());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                mPlayerEntered = true;

                KeyTips.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                mPlayerEntered = false;

                KeyTips.SetActive(false);
            }
        }

        private bool mOpenBonfireUI = false; // 是否打开火堆的UI

        /// <summary>
        /// 剩余时间
        /// </summary>
        public static float RemainSeconds = 60;
        private void Update()
        {
            RemainSeconds -= Time.deltaTime;
            if(RemainSeconds<=0)
            {
                RemainSeconds = 60;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (mPlayerEntered && !mOpenBonfireUI)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    mOpenBonfireUI = true;
                }
            }
            else if (mOpenBonfireUI)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    mOpenBonfireUI = false;
                }
            }
        }

        private List<IBonfireRule> mRules = new List<IBonfireRule>();
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(Screen.width - 200, 0, 200, 200)); // 在一个固定的屏幕区域中开始 GUI 控件的 GUILayout 块

            GUILayout.Label("寿命:" + (int)RemainSeconds + "s", Styles.label.Value);

            foreach (var bonfireRule in mRules)
            {
                bonfireRule.OnTopRightGUI();
            }

            GUILayout.EndArea();

            foreach (var bonfireRule in mRules)
            {
                bonfireRule.OnGUI();
            }

            if (mOpenBonfireUI)
            {
                GUILayout.Label("火堆 UI", Styles.label.Value);

                foreach (var bonfireRule in mRules)
                {
                    bonfireRule.OnBonfireOnGUI();
                }
            }
        }
    }
}