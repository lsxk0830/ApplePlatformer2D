public interface IBonfireRule
{
    int NeedSeconds { get; }
    string Key { get; }
    bool Unlocked { get; }
    void Reset();
    void OnBonfireGUI();
    void OnTopRightGUI();
    void OnGUI();

    void Save();
    IBonfireRule Load();
}