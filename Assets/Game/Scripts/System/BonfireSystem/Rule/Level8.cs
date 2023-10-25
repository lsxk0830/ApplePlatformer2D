namespace Blue
{
    public class Level8 : AbstractBonfireLevelRule
    {
        public override int NeedSeconds { get; set; } = 30;
        public override string Key { get; set; } = nameof(Level8);
        public override string DisplayName { get; } = "关卡 8";

        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked.Trigger(Key);
        }
    }
}