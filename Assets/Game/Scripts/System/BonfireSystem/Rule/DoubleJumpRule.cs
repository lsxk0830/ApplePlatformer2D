namespace Blue
{
    public class DoubleJumpRule : AbstractBonfireRule
    {
        public override int NeedSeconds { get; set; } = 100;
        public override string Key { get; set; } = nameof(DoubleJumpRule);
        public override string DisplayName { get; set; }= "二段跳";
    }
}