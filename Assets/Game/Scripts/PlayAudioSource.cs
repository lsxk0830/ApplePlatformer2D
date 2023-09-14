using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    public class PlayAudioSource : MonoBehaviour
    {
        public UnityEvent OnFinish = new UnityEvent();
        public void Execute()
        {
            var audioSource = GetComponent<AudioSource>();
            audioSource.Play();

            var seconds = audioSource.clip.length;

            Invoke(nameof(OnPlayFinished),seconds);
        }

        private void OnPlayFinished()
        {
            OnFinish?.Invoke();
        }
    }
}