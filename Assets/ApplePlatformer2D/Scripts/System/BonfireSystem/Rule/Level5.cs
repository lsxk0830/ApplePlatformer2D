namespace UTGM
{
    public class Level5 : AbstractBonfireRule
    {
        public override int NeedSeconds { get; } = 10;

        public override string Key { get; } = nameof(Level5);

        public override string DisplayName { get; } = "关卡 5";

        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked?.Trigger(Key);
        }
    }

}
