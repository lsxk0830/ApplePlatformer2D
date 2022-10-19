using UnityEngine;
using UnityEngine.Events;

public class PlayerAudioSource : MonoBehaviour
{
    public UnityEvent OnFinish = new UnityEvent();
    public void Execute()
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        var seconds = audioSource.clip.length;
        Invoke(nameof(OnplayFinished), seconds); ;
    }

    void OnplayFinished()
    {
        OnFinish?.Invoke();
    }
}
