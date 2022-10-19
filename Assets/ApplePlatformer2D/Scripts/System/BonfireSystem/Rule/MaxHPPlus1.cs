﻿using System;
using UTGM;

public class MaxHPPlus1 : AbstractBonfireRule
{
    public override int NeedSeconds { get; } = 10;
    public override string Key { get; } = nameof(MaxHPPlus1);
    public override string DisplayName { get; } = "最大HP+1";

    Lazy<IBonfireRule> mHPBarRule = new Lazy<IBonfireRule>(() =>
       ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>().GetRuleByKey(nameof(HPBar)));

    public override void OnBonfireGUI()
    {
        if (mHPBarRule.Value.Unlocked) 
        {
            base.OnBonfireGUI();
        }
    }
    protected override void OnUnlock()
    {
        var playerModel = ApplePlatformer2D.Interface.GetModel<IPlayerModel>();
        playerModel.HP++;
        playerModel.MaxHP++;
    }
}