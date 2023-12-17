namespace Blue
{
    /// <summary>
    /// 血量条
    /// </summary>
    public class DashRule : AbstractBonfireRule
    {
        public override int NeedSeconds { get; set; } = 50;
        public override string Key { get; set; } = nameof(DashRule);
        public override string DisplayName { get; set; }= "冲刺";
    }
}