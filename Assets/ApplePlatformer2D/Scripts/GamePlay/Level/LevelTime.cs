using UnityEngine;
using UnityEngine.UI;

public class LevelTime : MonoBehaviour
{
    private Text mText;
    void Start()
    {
        mText = GetComponent<Text>();
    }

    private float mCurrentSeconds = 0f;

    void Update()
    {
        mCurrentSeconds+=Time.deltaTime;
        if (Time.frameCount % 20 == 0) // Time.frameCount:自游戏开始以来的总帧数（只读）
            mText.text = ((int)mCurrentSeconds).ToString();   
    }
}
