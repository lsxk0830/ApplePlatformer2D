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
    }
}