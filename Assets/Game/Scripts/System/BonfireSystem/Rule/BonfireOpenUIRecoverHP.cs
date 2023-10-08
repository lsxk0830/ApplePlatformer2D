namespace Blue
{
    /// <summary>
    /// 打开火堆UI血量恢复
    /// </summary>
    public class BonfireOpenUIRecoverHP : AbstractBonfireRule
    {
        public override int NeedSeconds { get; } = 30;
        public override string Key { get; } = nameof(BonfireOpenUIRecoverHP);
        public override string DisplayName { get; } = "回到火堆恢复 HP";
    };
}