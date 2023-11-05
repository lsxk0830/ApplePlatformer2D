namespace Blue
{
    /// <summary>
    /// 每10个苹果恢复1HP
    /// </summary>
    public class AddHPEvery10ApplesRule : AbstractBonfireRule
    {
        public override int NeedSeconds { get; set; } = 50;
        public override string Key { get; set; } = nameof(AddHPEvery10ApplesRule);
        public override string DisplayName { get; } = "每10个苹果恢复1HP";
    }
}