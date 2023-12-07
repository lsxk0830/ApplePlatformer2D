// namespace Blue
// {
//     public class Level2 : AbstractBonfireLevelRule
//     {
//         public override int NeedSeconds { get; set;} = 10;
//         public override string Key { get; set; } = nameof(Level2);
//         public override string DisplayName { get; } = "关卡 2";

//         protected override void OnUnlock()
//         {
//             ApplePlatformer2D.OnBonfireRuleUnlocked.Trigger(Key);
//         }
//     }
// }