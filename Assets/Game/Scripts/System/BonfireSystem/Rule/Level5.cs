namespace Blue
{
    public class Level5 : AbstractBonfireLevelRule
    {
        public override int NeedSeconds { get; set; } = 10;
        public override string Key { get; } = nameof(Level5);
        public override string DisplayName { get; } = "关卡 5";

        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked.Trigger(Key);
        }
    }
}