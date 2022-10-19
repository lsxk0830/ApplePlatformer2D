using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelPass : MonoBehaviour
{
    public Text levelPassText;

    public UnityEvent OnLevelPass;
    public UnityEvent OnLevelDelayFinish;

    public bool ResetPlayer2OriginPoint =false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelPassText.gameObject.SetActive(true);

            OnLevelPass?.Invoke();

            StartCoroutine(Delay(2.0f, () =>
            {
                if (ResetPlayer2OriginPoint)
                {
                    var player = GameObject.FindWithTag("Player");
                    player.transform.position = Vector2.zero;
                    var trigger2D = player.transform.Find("GroundCheck").GetComponent<Trigger2D>();
                    trigger2D.Reset();
                }
                OnLevelDelayFinish?.Invoke();
            }));
        }
    }

    IEnumerator Delay(float seconds,Action OnFinish)
    {
        yield return new WaitForSeconds(seconds);
        OnFinish();
    }
}
