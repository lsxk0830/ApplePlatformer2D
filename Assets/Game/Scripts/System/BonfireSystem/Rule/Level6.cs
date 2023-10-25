namespace Blue
{
    public class Level6 : AbstractBonfireLevelRule
    {
        public override int NeedSeconds { get; set; } = 10;
        public override string Key { get; set; } = nameof(Level6);
        public override string DisplayName { get; } = "关卡 6";

        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked.Trigger(Key);
        }
    }
}