namespace Blue
{
    public class Level4 : AbstractBonfireLevelRule
    {
        public override int NeedSeconds { get; set; } = 10;
        public override string Key { get; } = nameof(Level4);
        public override string DisplayName { get; } = "关卡 4";

        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked.Trigger(Key);
        }
    }
}