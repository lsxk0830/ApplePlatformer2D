namespace Blue
{
    public class Level7 : AbstractBonfireRule
    {
        public override int NeedSeconds { get; } = 30;
        public override string Key { get; } = nameof(Level7);
        public override string DisplayName { get; } = "关卡 7";

        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked.Trigger(Key);
        }
    }
}