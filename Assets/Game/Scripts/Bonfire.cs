using UnityEngine;
using UnityEngine.SceneManagement;
using QFramework;

namespace Blue
{
    public class Bonfire : MonoBehaviour
    {
        public GameObject KeyTips;

        private bool mPlayerEntered = false; // 主角是否进入

        private IBonfireSystem mBonfireSystem;

        public int DebugInitRemainSeconds = 60;
        public int DebugInitPlayerHp = 5;
        public bool IsDebug = false;
        private void Awake()
        {
            if (IsDebug)
            {
                RemainSeconds = DebugInitRemainSeconds;
                var playerModel = ApplePlatformer2D.Interface.GetModel<IPlayerModel>();
                playerModel.HP = DebugInitPlayerHp;
                playerModel.MaxHP = DebugInitPlayerHp;
            }

            mBonfireSystem = ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>();

            ApplePlatformer2D.OnOpenBonfireUI.Register(() =>
            {
                var rule = mBonfireSystem.GetRuleByKey(nameof(BonfireOpenUIRecoverHP));
                if (rule.Unlocked)
                {
                    var playerModel = ApplePlatformer2D.Interface.GetModel<IPlayerModel>();
                    playerModel.HP = playerModel.MaxHP;
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
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

        /// <summary>
        /// 存活时间
        /// </summary>
        public static float LiveSeconds = 0;
        private void Update()
        {
            if (ApplePlatformer2D.IsGameOver)
                return;

            RemainSeconds -= Time.deltaTime;
            LiveSeconds += Time.deltaTime;
            if (RemainSeconds <= 0)
            {
                ApplePlatformer2D.IsGameOver = true;
            }

            if (mPlayerEntered && !mOpenBonfireUI)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    mOpenBonfireUI = true;
                    ApplePlatformer2D.OnOpenBonfireUI.Trigger();
                    AudioSystem.PlayUIFeedback();
                }
            }
            else if (mOpenBonfireUI)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    mOpenBonfireUI = false;
                    AudioSystem.PlayUIFeedback();
                }
            }
        }

        private const int WIDTH = 1024;
        private const int HEIGHT = 768;

        public void SetDsignResolution(int width, int height)
        {
            var scaleX = Screen.width / width;
            var scaleY = Screen.height / height;

            var scale = Mathf.Max(scaleX, scaleY);
            GUIUtility.ScaleAroundPivot(new Vector2(scale, scale), new Vector2(0, 0));
        }

        private void OnGUI()
        {
            SetDsignResolution(WIDTH,HEIGHT);

            GUILayout.BeginArea(new Rect(WIDTH - 200, 0, 200, 200)); // 在一个固定的屏幕区域中开始 GUI 控件的 GUILayout 块

            GUILayout.BeginHorizontal();
            GUILayout.Label("寿命:" + (int)RemainSeconds + "s", Styles.label.Value);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("已存活:" + (int)LiveSeconds + "s", Styles.label.Value);
            GUILayout.EndHorizontal();

            foreach (var bonfireRule in mBonfireSystem.Rules)
            {
                bonfireRule.OnTopRightGUI();
            }

            GUILayout.EndArea();

            foreach (var bonfireRule in mBonfireSystem.Rules)
            {
                bonfireRule.OnGUI();
            }

            if (mOpenBonfireUI)
            {
                GUILayout.Label("火堆 UI", Styles.label.Value);

                foreach (var bonfireRule in mBonfireSystem.Rules)
                {
                    bonfireRule.OnBonfireOnGUI();
                }
            }

            if (ApplePlatformer2D.IsGameOver)
            {
                // 游戏结束界面
                GUILayout.BeginArea(new Rect(0, 0, WIDTH, HEIGHT));

                GUILayout.FlexibleSpace();

                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    GUILayout.Label("游戏结束", Styles.Biglabel.Value);
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(50); // 设置间距

                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("重新开始", Styles.BigButton.Value))
                    {
                        ApplePlatformer2D.ResetGameData();
                        AudioSystem.PlayUIFeedback();
                        SceneManager.LoadScene("Game");
                    }
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("回到主页", Styles.BigButton.Value))
                    {
                        ApplePlatformer2D.ResetGameData();
                        AudioSystem.PlayUIFeedback();
                        SceneManager.LoadScene("GameStart");
                    }
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();

                GUILayout.FlexibleSpace();
                GUILayout.EndArea();
            }
        }

        private void OnDestroy()
        {
            mBonfireSystem = null;
        }
    }
}