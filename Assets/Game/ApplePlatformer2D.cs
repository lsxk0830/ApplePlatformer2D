using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Blue
{
    public class ApplePlatformer2D : Architecture<ApplePlatformer2D>
    {
        public static EasyEvent OnOpenBonfireUI = new EasyEvent();
        public static EasyEvent<string> OnBonfireRuleUnlocked = new EasyEvent<string>();
        private static bool mIsGameOver = false;
        public static bool IsGameOver
        {
            get => mIsGameOver;
            set
            {
                mIsGameOver = value;
                if (value)
                    SceneManager.LoadScene("GameOver");
            }
        }

        /// <summary>
        /// 暂停之前的事件缩放缓存
        /// </summary>
        private static float mCachedTimeScale;
        /// <summary>
        /// 暂停游戏
        /// </summary>
        public static void GamePause()
        {
            if (!IsGamePause)
            {
                mCachedTimeScale = Time.timeScale;
                Time.timeScale = 0;
                IsGamePause = true;
            }
        }
        /// <summary>
        /// 对外提供用于查询
        /// </summary>
        public static bool IsGamePause { get; set; } = false;
        /// <summary>
        /// 继续游戏
        /// </summary>
        public static void GameResume()
        {
            if (IsGamePause)
            {
                Time.timeScale = mCachedTimeScale;
                IsGamePause = false;
            }
        }

        /// <summary>
        /// 是否继续游戏
        /// </summary>
        public static bool HasContinue
        {
            //get => PlayerPrefs.GetInt(nameof(HasContinue), 0) == 1;
            get => false; // 设置成不用继续游戏
            set => PlayerPrefs.SetInt(nameof(HasContinue), value ? 1 : 0);
        }
        protected override void Init()
        {
            RegisterSystem();
            RegisterModel();

        }

        private void RegisterModel()
        {
            this.RegisterModel<IPlayerModel>(new PlayerModel());
        }

        private void RegisterSystem()
        {
            this.RegisterSystem<IBonfireSystem>(new BonfireSystem());
            this.RegisterSystem<ISaveSystem>(new SaveSystem());
            this.RegisterSystem<IInputSystem>(new InputSystem());
        }

        /// <summary>
        /// 重置游戏数据
        /// </summary>
        public static void ResetGameData()
        {
            IsGameOver = false;
            Interface.GetModel<IPlayerModel>().HP = 1;
            Interface.GetModel<IPlayerModel>().MaxHP = 1;
            Interface.GetModel<IPlayerModel>().CurrentAppleCount = 0;
            foreach (var bonfireRule in Interface.GetSystem<IBonfireSystem>().Rules)
            {
                bonfireRule.Reset();
            }
            Interface.GetSystem<IBonfireSystem>().GenerateRandomLevel(); // 重新生成一下关卡顺序
            Interface.GetSystem<ISaveSystem>().Clear();
            Bonfire.SetRemainSecondsWithoutChangeEvent(60);
            Bonfire.LiveSeconds = 0;
            Time.timeScale = 1.0f;
        }

        /// <summary>
        /// 继续游戏时获取的数据
        /// </summary>
        public static void ContinueGame()
        {
            IsGameOver = false;

            Interface.GetModel<IPlayerModel>().HP = PlayerPrefs.GetInt("HP", 1);
            Interface.GetModel<IPlayerModel>().MaxHP = PlayerPrefs.GetInt("MaxHP", 1);
            Interface.GetModel<IPlayerModel>().CurrentAppleCount = PlayerPrefs.GetInt("CurrentAppleCount", 0);

            Bonfire.SetRemainSecondsWithoutChangeEvent(PlayerPrefs.GetFloat("RemainSeconds", 60));
            Bonfire.LiveSeconds = PlayerPrefs.GetFloat("LiveSeconds", 0);

            foreach (var bonfireRule in Interface.GetSystem<IBonfireSystem>().Rules)
            {
                bonfireRule.Load();
            }
            Interface.GetSystem<ISaveSystem>().Load();

            Time.timeScale = 1.0f;
        }

        public static void Save()
        {
            // Save
            PlayerPrefs.SetInt("HP", Interface.GetModel<IPlayerModel>().HP);
            PlayerPrefs.SetInt("MaxHP", Interface.GetModel<IPlayerModel>().MaxHP);
            PlayerPrefs.SetFloat("CurrentAppleCount", Interface.GetModel<IPlayerModel>().CurrentAppleCount);
            PlayerPrefs.SetFloat("RemainSeconds", Bonfire.RemainSeconds);
            PlayerPrefs.SetFloat("LiveSeconds", Bonfire.LiveSeconds);

            foreach (var bonfireRule in Interface.GetSystem<IBonfireSystem>().Rules)
            {
                bonfireRule.Save();
            }

            Interface.GetSystem<ISaveSystem>().Save();
        }
    }
}