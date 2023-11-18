namespace Blue
{
    public class Level9 : AbstractBonfireLevelRule
    {
        public override int NeedSeconds { get; set; } = 30;
        public override string Key { get; set; } = nameof(Level9);
        public override string DisplayName { get; } = "关卡 9";

        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked.Trigger(Key);
        }
    }
}