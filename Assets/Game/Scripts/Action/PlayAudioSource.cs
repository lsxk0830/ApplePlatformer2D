using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    /// <summary>
    /// 播放音频，音频播放完毕触发OnFinish事件
    /// </summary>
    public class PlayAudioSource : MonoBehaviour
    {
        public UnityEvent OnFinish = new UnityEvent();
        public void Execute()
        {
            var audioSource = GetComponent<AudioSource>();
            audioSource.Play();

            var seconds = audioSource.clip.length;

            Invoke(nameof(OnPlayFinished), seconds);
        }

        private void OnPlayFinished()
        {
            OnFinish?.Invoke();
        }
    }
}