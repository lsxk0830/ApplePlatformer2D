using QFramework;
using UnityEngine;

namespace Blue
{
    public class ApplePlatformer2D : Architecture<ApplePlatformer2D>
    {
        public static EasyEvent OnOpenBonfireUI = new EasyEvent();
        private static bool mIsGameOver = false;
        public static bool IsGameOver
        {
            get => mIsGameOver;
            set
            {
                if (value)
                {
                    HasContinue = false;
                }
                mIsGameOver = value;
            }
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

            GlobalMonoEvents.OnApplicationQuitEvent.Register(() =>
            {
                // Save
                PlayerPrefs.SetInt("HP", Interface.GetModel<IPlayerModel>().HP);
                PlayerPrefs.SetInt("MaxHP", Interface.GetModel<IPlayerModel>().MaxHP);
                PlayerPrefs.SetFloat("RemainSeconds", Bonfire.RemainSeconds);
                foreach (var bonfireRule in Interface.GetSystem<IBonfireSystem>().Rules)
                {
                    bonfireRule.Save();
                }
            });
        }

        private void RegisterModel()
        {
            this.RegisterModel<IPlayerModel>(new PlayerModel());
        }

        private void RegisterSystem()
        {
            this.RegisterSystem<IBonfireSystem>(new BonfireSystem());
        }

        /// <summary>
        /// 重置游戏数据
        /// </summary>
        public static void ResetGameData()
        {
            IsGameOver = false;
            Interface.GetModel<IPlayerModel>().HP = 1;
            Interface.GetModel<IPlayerModel>().MaxHP = 1;
            foreach (var bonfireRule in Interface.GetSystem<IBonfireSystem>().Rules)
            {
                bonfireRule.Reset();
            }
            Bonfire.RemainSeconds = 60;
        }

        /// <summary>
        /// 继续游戏时获取的数据
        /// </summary>
        public static void ContinueGame()
        {
            Interface.GetModel<IPlayerModel>().HP = PlayerPrefs.GetInt("HP", 1);
            Interface.GetModel<IPlayerModel>().MaxHP = PlayerPrefs.GetInt("MaxHP", 1);
            Bonfire.RemainSeconds = PlayerPrefs.GetFloat("RemainSeconds", 60);
            foreach (var bonfireRule in Interface.GetSystem<IBonfireSystem>().Rules)
            {
                bonfireRule.Load();
            }
        }
    }
}