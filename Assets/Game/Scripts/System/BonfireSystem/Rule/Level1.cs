namespace Blue
{
    public class Level1 : AbstractBonfireLevelRule
    {
        public override int NeedSeconds { get; set; } = 10;
        public override string Key { get; set; } = nameof(Level1);
        public override string DisplayName { get; } = "关卡 1";

        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked.Trigger(Key);
        }
    }
}