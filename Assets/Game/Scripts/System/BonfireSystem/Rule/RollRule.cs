namespace Blue
{
    /// <summary>
    /// 翻滚规则
    /// </summary>
    public class RollRule : AbstractBonfireRule
    {
        public override int NeedSeconds { get; set; } = 50;
        public override string Key { get; set; } = nameof(RollRule);
        public override string DisplayName { get; set; }= "翻滚(L)";
    }
}