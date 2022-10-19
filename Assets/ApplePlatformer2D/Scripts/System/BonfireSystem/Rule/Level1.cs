namespace UTGM
{
    public class Level1 : AbstractBonfireRule
    {
        public override int NeedSeconds { get; } = 10;

        public override string Key { get; } = nameof(Level1);

        public override string DisplayName { get; } = "关卡 1";

        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked?.Trigger(Key);
        }
    }

}
