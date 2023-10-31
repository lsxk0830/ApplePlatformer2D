namespace Blue
{
    /// <summary>
    /// 火堆规则
    /// </summary>
    public interface IBonfireRule
    {
        /// <summary>
        /// 需要的时间
        /// </summary>
        int NeedSeconds { get; }

        /// <summary>
        /// 火堆规则名，如 nameof(HPBar)
        /// </summary>
        string Key { get; }

        /// <summary>
        /// 是否解锁
        /// </summary>
        bool Unlocked { get; set; }

        /// <summary>
        /// 解锁
        /// </summary>
        void Unlock();

        /// <summary>
        /// 重置
        /// </summary>
        void Reset();

        /// <summary>
        /// 火堆未解锁时的UI，
        /// 例如 "HPBar"、"解锁"
        /// </summary>
        void OnBonfireOnGUI();

        /// <summary>
        /// 火堆解锁后的UI
        /// 例如 "血量:1/1"
        /// 在右上方绘制
        /// </summary>
        void OnTopRightGUI();

        /// <summary>
        /// 自定义绘制，在任意处绘制
        /// </summary>
        void OnGUI();

        /// <summary>
        /// 保存当前的火堆规则
        /// </summary>
        void Save();

        /// <summary>
        /// 加载保存的火堆规则
        /// </summary>
        IBonfireRule Load();
    }
}