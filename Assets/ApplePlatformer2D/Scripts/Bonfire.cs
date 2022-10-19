using UnityEngine;
using UnityEngine.SceneManagement;
using UTGM;
using QFramework;

public class Bonfire : MonoBehaviour
{
    public GameObject KeyTips;

    private bool mPlayerEntered = false;

    private IBonfireSystem mBonfireSystem;
    private void Awake()
    {
        mBonfireSystem = ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>();

        ApplePlatformer2D.OnOpenBonfireUI.Register(() =>
        {
            var rule = mBonfireSystem.GetRuleByKey(nameof(BonfireOpenUIRecoverHP));
            if (rule.Unlocked)
            {
                var playerModel = ApplePlatformer2D.Interface.GetModel<IPlayerModel>();
                playerModel.HP = playerModel.MaxHP;
            }
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    private void OnDestroy()
    {
        mBonfireSystem = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mPlayerEntered = true;

            KeyTips.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mPlayerEntered=false;

            KeyTips.SetActive(false);   
        }
    }

    private bool mOpenBonfireUI = false;

    /// <summary>
    /// 寿命
    /// </summary>
    public static float RemainSeconds = 60;
    private void Update()
    {
        if (ApplePlatformer2D.IsGameOver) return;

        RemainSeconds-=Time.deltaTime;
        if (RemainSeconds <= 0)
        {
            ApplePlatformer2D.IsGameOver = true;
        }

        if (mPlayerEntered && !mOpenBonfireUI)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                mOpenBonfireUI = true;
                ApplePlatformer2D.OnOpenBonfireUI.Trigger();
                AudioSystem.PlayerUIFeedback();
            }
        }
        else if(mOpenBonfireUI)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                mOpenBonfireUI = false;
                AudioSystem.PlayerUIFeedback();
            }
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 200, 0, 200, 200)); // 在一个固定的屏幕区域中开始 GUI 控件的 GUILayout 块
        
        GUILayout.Label("寿命："+(int)RemainSeconds+"s",Styles.label.Value);
        foreach (var bonfireRule in mBonfireSystem.Rules)
        {
            bonfireRule.OnTopRightGUI();
        }

        GUILayout.EndArea();

        foreach (var bonfireRule in mBonfireSystem.Rules)
        {
            bonfireRule.OnGUI();
        }

        if (mOpenBonfireUI)
        {
            GUILayout.Label("火堆UI", Styles.label.Value);

            foreach (var bonfireRule in mBonfireSystem.Rules)
            {
                bonfireRule.OnBonfireGUI();
            }
        }

        if (ApplePlatformer2D.IsGameOver)
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                GUILayout.Label("游戏结束", Styles.BigLabel.Value);
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(50);

            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("重新开始", Styles.BigButton.Value))
                {
                    ApplePlatformer2D.ResetGameData();
                    AudioSystem.PlayerUIFeedback();
                    SceneManager.LoadScene("Game");
                }
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("回到主页", Styles.BigButton.Value))
                {
                    ApplePlatformer2D.ResetGameData();
                    AudioSystem.PlayerUIFeedback();
                    SceneManager.LoadScene("GameStart");
                }
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();

            GUILayout.EndArea();

        }
    }
}
