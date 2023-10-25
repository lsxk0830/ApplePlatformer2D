namespace Blue
{
    public class Level3 : AbstractBonfireLevelRule
    {
        public override int NeedSeconds { get; set; } = 10;
        public override string Key { get; set; } = nameof(Level3);
        public override string DisplayName { get; } = "关卡 3";

        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked.Trigger(Key);
        }
    }
}