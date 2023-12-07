namespace Blue
{
    /// <summary>
    /// 由于9个关卡内容都差不多，所以统一提取一个关卡规则
    /// </summary>
    public class GenericlLevel : AbstractBonfireLevelRule
    {
        public override int NeedSeconds { get; set; } = 10;
        public override string Key { get; set; } = nameof(GenericlLevel);
        public override string DisplayName { get; set; } = "关卡 6";

        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked.Trigger(Key);
        }
    }
}