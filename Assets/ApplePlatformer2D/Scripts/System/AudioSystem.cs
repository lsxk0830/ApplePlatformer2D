using UnityEngine;

namespace UTGM
{
    public class AudioSystem : MonoBehaviour
    {
        public AudioSource UIFeeback;

        private static AudioSystem mAudioSystem;

        private void Awake()
        {
            mAudioSystem = this;
        }

        private void OnDestroy()
        {
            mAudioSystem = null;
        }
        public static void PlayerUIFeedback()
        {
            mAudioSystem.UIFeeback?.Play();
        }
    }
}