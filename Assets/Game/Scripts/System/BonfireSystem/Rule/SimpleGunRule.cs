namespace Blue
{
    /// <summary>
    /// 简单枪规则
    /// </summary>
    public class SimpleGunRule : AbstractBonfireLevelRule
    {
        public override int NeedSeconds { get; set; }
        public override string Key { get; set; } = nameof(SimpleGunRule);
        public override string DisplayName { get; } = "简单枪";

        protected override void OnUnlock()
        {
            base.OnUnlock();

            ApplePlatformer2D.OnBonfireRuleUnlocked.Trigger(Key);
        }
    }
}