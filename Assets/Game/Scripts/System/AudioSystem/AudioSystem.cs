using UnityEngine;

namespace Blue
{
    public class AudioSystem : MonoBehaviour
    {
        public AudioSource UIFeedback;
        private static AudioSystem mAudioSystem;

        private static AudioConfig mAudioConfig;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            mAudioSystem = this;
        }

        public static void PlayUIFeedback()
        {
            mAudioSystem?.UIFeedback?.Play();
        }

        private void OnDestroy()
        {
            mAudioSystem = null;
        }

        /// <summary>
        /// 播放销毁地块音效
        /// </summary>
        public static void PlayDestrutcTile(Vector3 point)
        {
            MakeSureConfig();
            AudioSource.PlayClipAtPoint(mAudioConfig.DestructTileClip,point);
        }

        private static void MakeSureConfig()
        {
            if(!mAudioConfig)
                mAudioConfig = AudioConfig.Load();
        }
    }
}