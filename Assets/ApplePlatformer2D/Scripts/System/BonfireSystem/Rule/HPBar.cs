using System;
using UnityEngine;
using UTGM;

public class HPBar : AbstractBonfireRule
{
    Lazy<IPlayerModel> mPlayerModel= new Lazy<IPlayerModel>(()=>ApplePlatformer2D.Interface.GetModel<IPlayerModel>());

    public override int NeedSeconds { get; } = 30;

    public override string Key { get; } = nameof(HPBar);

    public override string DisplayName { get; } = "血量条";

    public override void OnTopRightGUI()
    {
        if (Unlocked)
        {
            GUILayout.Label($"血量：{mPlayerModel.Value.HP }/{ mPlayerModel.Value.MaxHP}", Styles.label.Value);
        }

    }
}