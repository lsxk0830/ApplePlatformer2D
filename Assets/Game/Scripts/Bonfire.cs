using UnityEngine;
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

        private IInputSystem mInputSystem;

        private void Awake()
        {
            mInputSystem = ApplePlatformer2D.Interface.GetSystem<IInputSystem>();

            var bonfireUIController = transform.GetComponentInChildren<BonfireUIController>();
            if (bonfireUIController)
                bonfireUIController.BtnClose.onClick.AddListener(() =>
                {
                    CloseUI();
                });

            if (IsDebug)
            {
                SetRemainSecondsWithoutChangeEvent(DebugInitRemainSeconds);
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

                ApplePlatformer2D.Save(); // 每次打开火堆时进行存储操作

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        /// <summary>
        /// 对外提供一个打开UI的方法
        /// </summary>
        public void OpenUI()
        {
            ApplePlatformer2D.GamePause();
            mOpenBonfireUI = true;
            ApplePlatformer2D.OnOpenBonfireUI.Trigger();
            AudioSystem.PlayUIFeedback();

            var bonfireUIController = transform.GetComponentInChildren<BonfireUIController>();
            if (bonfireUIController)
                bonfireUIController.Open();
        }

        /// <summary>
        /// 对外提供一个关闭UI的方法
        /// </summary>
        public void CloseUI()
        {
            ApplePlatformer2D.GameResume();
            mOpenBonfireUI = false;
            AudioSystem.PlayUIFeedback();

            var bonfireUIController = transform.GetComponentInChildren<BonfireUIController>();
            if (bonfireUIController)
                bonfireUIController.Close();
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
        public static float RemainSeconds => mRemainSeconds;

        /// <summary>
        /// 触发寿命变更事件的设置方式
        /// </summary>
        /// <param name="newRemainSeconds"></param>
        public static void SetRemainSecondsWithChangerEvent(float newRemainSeconds)
        {
            if (Mathf.Abs(mRemainSeconds - newRemainSeconds) > 0.01f)
            {
                OnRemainSecondsChanged.Trigger(newRemainSeconds - mRemainSeconds);
                mRemainSeconds = newRemainSeconds;
            }
        }
        /// <summary>
        /// 不触发寿命変更的事件方式
        /// </summary>
        public static void SetRemainSecondsWithoutChangeEvent(float newRemainSeconds)
        {
            mRemainSeconds = newRemainSeconds;
        }

        private static float mRemainSeconds = 60;
        public static EasyEvent<float> OnRemainSecondsChanged = new EasyEvent<float>();

        /// <summary>
        /// 存活时间
        /// </summary>
        public static float LiveSeconds = 0;
        private void Update()
        {
            if (ApplePlatformer2D.IsGameOver)
                return;

            SetRemainSecondsWithoutChangeEvent(RemainSeconds - Time.deltaTime);
            LiveSeconds += Time.deltaTime;
            if (RemainSeconds <= 0)
            {
                ApplePlatformer2D.IsGameOver = true;
            }

            if (mPlayerEntered && !mOpenBonfireUI)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    OpenUI();
                }
            }
            else if (mOpenBonfireUI)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    CloseUI();
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
            SetDsignResolution(WIDTH, HEIGHT);

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
        }

        private void OnDestroy()
        {
            mBonfireSystem = null;
            mInputSystem = null;
        }
    }
}