namespace UTGM
{
    public class Level3 : AbstractBonfireRule
    {
        public override int NeedSeconds { get; } = 10;

        public override string Key { get; } = nameof(Level3);

        public override string DisplayName { get; } = "关卡 3";

        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked?.Trigger(Key);
        }
    }

}
