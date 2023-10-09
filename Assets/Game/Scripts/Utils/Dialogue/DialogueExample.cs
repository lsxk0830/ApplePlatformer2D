using System.Collections.Generic;
using UnityEngine;

namespace Blue
{
    public class DialogueExample : MonoBehaviour
    {
        private Queue<string> mSentenceQueue = new Queue<string>();
        private void Awake()
        {
            mSentenceQueue.Enqueue("Hello World~");
            mSentenceQueue.Enqueue("I'm Blue~");
            mSentenceQueue.Enqueue("Welcome to my world~");
            mSentenceQueue.Enqueue("Happy learning~");

            Debug.Log(mSentenceQueue.Dequeue());
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && mSentenceQueue.Count > 0)
            {
                Debug.Log(mSentenceQueue.Dequeue());
            }
        }
    }
}