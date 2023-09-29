using System;
using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 血量条
    /// </summary>
    public class HPBar : AbstractBonfireRule
    {
        Lazy<IPlayerModel> mPlayerModel = new Lazy<IPlayerModel>(() => ApplePlatformer2D.Interface.GetModel<IPlayerModel>());
        public override int NeedSeconds { get; } = 30;
        public override string Key { get; } = nameof(HPBar);
        public override string DisplayName { get; } = "血量条";

        public override void OnTopRightGUI()
        {
            if (Unlocked)
            {
                GUILayout.Label($"血量:{mPlayerModel.Value.HP}/{mPlayerModel.Value.MaxHP}", Styles.label.Value);
            }
        }
    }
}