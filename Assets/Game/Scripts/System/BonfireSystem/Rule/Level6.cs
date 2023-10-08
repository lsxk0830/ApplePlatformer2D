namespace Blue
{
    public class Level6 : AbstractBonfireRule
    {
        public override int NeedSeconds { get; } = 10;
        public override string Key { get; } = nameof(Level6);
        public override string DisplayName { get; } = "关卡 6";

        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked.Trigger(Key);
        }
    }
}