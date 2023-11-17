using QFramework;
using UnityEngine;

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
            set => mIsGameOver = value;
        }

        /// <summary>
        /// 是否继续游戏
        /// </summary>
        public static bool HasContinue
        {
            get => PlayerPrefs.GetInt(nameof(HasContinue), 0) == 1;
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
            Interface.GetSystem<ISaveSystem>().Clear();
            Bonfire.RemainSeconds = 60;
            Bonfire.LiveSeconds = 0;
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

            Bonfire.RemainSeconds = PlayerPrefs.GetFloat("RemainSeconds", 60);
            Bonfire.LiveSeconds = PlayerPrefs.GetFloat("LiveSeconds", 0);

            foreach (var bonfireRule in Interface.GetSystem<IBonfireSystem>().Rules)
            {
                bonfireRule.Load();
            }

            Interface.GetSystem<ISaveSystem>().Load();
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