namespace Blue
{
    /// <summary>
    /// 打开火堆UI重新生成敌人
    /// </summary>
    public class BonfireOpenUIRebornEnemy : AbstractBonfireRule
    {
        public override int NeedSeconds { get; set; } = 5;
        public override string Key { get; set; } = nameof(BonfireOpenUIRebornEnemy);
        public override string DisplayName { get; set; }= "在火堆重生敌人";
    }
}