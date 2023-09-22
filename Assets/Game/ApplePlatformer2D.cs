using QFramework;

namespace Blue
{
    public class ApplePlatformer2D : Architecture<ApplePlatformer2D>
    {
        public static EasyEvent OnOpenBonfireUI = new EasyEvent();
        public static bool IsGameOver = false;
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
        }

        public static void ResetGameData()
        {
            ApplePlatformer2D.IsGameOver = false;
            ApplePlatformer2D.Interface.GetModel<IPlayerModel>().HP = 1;
            ApplePlatformer2D.Interface.GetModel<IPlayerModel>().MaxHP = 1;
            foreach (var bonfireRule in ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>().Rules)
            {
                bonfireRule.Reset();
            }
            Bonfire.RemainSeconds = 60;
        }
    }
}