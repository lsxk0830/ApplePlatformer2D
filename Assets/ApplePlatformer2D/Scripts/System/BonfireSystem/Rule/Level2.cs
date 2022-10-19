namespace UTGM
{
    public class Level2 : AbstractBonfireRule
    {
        public override int NeedSeconds { get; } = 10;

        public override string Key { get; } = nameof(Level2);

        public override string DisplayName { get; } = "关卡 2";

        protected override void OnUnlock()
        {
            ApplePlatformer2D.OnBonfireRuleUnlocked?.Trigger(Key);
        }
    }

}
