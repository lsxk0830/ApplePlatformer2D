namespace UTGM
{
    public class BonfireOpenUIRecoverHP : AbstractBonfireRule
    {
        public override int NeedSeconds { get; } = 30;

        public override string Key { get;  } =  nameof(BonfireOpenUIRecoverHP);

        public override string DisplayName { get; } = "回到火堆回复 HP";
    }
}