using UnityEngine;

namespace Blue
{
    public class AudioSystem : MonoBehaviour
    {
        public AudioSource UIFeedback;
        private static AudioSystem mAudioSystem;

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
    }
}