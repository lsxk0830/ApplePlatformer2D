namespace Blue
{
    /// <summary>
    /// 攀墙规则
    /// </summary>
    public class WallGrabRule : AbstractBonfireRule
    {
        public override int NeedSeconds { get; set; } = 50;
        public override string Key { get; set; } = nameof(WallGrabRule);
        public override string DisplayName { get; set; }= "攀墙";
    }
}