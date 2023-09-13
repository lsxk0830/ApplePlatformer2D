using UnityEngine;
using UnityEngine.UI;

namespace Blue
{
    public class LevelTime : MonoBehaviour
    {
        private Text mText;
        private float mCurrentSeconds = 0f;

        void Start()
        {
            mText = GetComponent<Text>();
        }

        void Update()
        {
            mCurrentSeconds += Time.deltaTime;

            if (Time.frameCount % 20 == 0)
                mText.text = ((int)mCurrentSeconds).ToString();
        }
    }
}