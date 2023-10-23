namespace Blue
{
    public class Level8 : AbstractBonfireRule
    {
        public override int NeedSeconds { get; } = 30;
        public override string Key { get; } = nameof(Level8);
        public override string DisplayName { get; } = "关卡 8";

        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked.Trigger(Key);
        }
    }
}