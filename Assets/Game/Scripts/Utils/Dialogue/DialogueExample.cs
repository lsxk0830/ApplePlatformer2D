using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Blue
{
    public class DialogueExample : MonoBehaviour
    {
        /// <summary>
        /// 对话播放状态
        /// </summary>
        public enum DialogueStates
        {
            /// <summary>
            /// 未开始
            /// </summary>
            NotStart,
            /// <summary>
            /// 正在播放
            /// </summary>
            Started,
            /// <summary>
            /// 已完成
            /// </summary>
            Finished
        }

        public DialogueStates State = DialogueStates.NotStart;
        private Queue<string> mSentenceQueue = new Queue<string>();

        // 状态事件回调
        public UnityEvent OnDialogueNotStart = new UnityEvent();
        public UnityEvent OnDialogueStart = new UnityEvent();
        public UnityEvent OnDialogueFinish = new UnityEvent();
        // 对外提供设置文本的方法
        public UnityEvent<string> OnPlayText = new UnityEvent<string>();

        private void Awake()
        {
            mSentenceQueue.Enqueue("Hello World~");
            mSentenceQueue.Enqueue("I'm Blue~");
            mSentenceQueue.Enqueue("Welcome to my world~");
            mSentenceQueue.Enqueue("Happy learning~");

            OnDialogueNotStart?.Invoke();
        }

        /// <summary>
        /// 对外提供一个开始播放的方法
        /// </summary>
        public void StartDialogue()
        {
            if (State == DialogueStates.NotStart)
            {
                State = DialogueStates.Started;
                OnDialogueStart?.Invoke();
                Next();
            }
        }

        /// <summary>
        /// 对外提供下一步方法
        /// </summary>
        public void Next()
        {
            if (State == DialogueStates.Started)
            {
                if (mSentenceQueue.Count > 0)
                {
                    StartCoroutine(StartPlayText(mSentenceQueue.Dequeue()));
                }
                else
                {
                    StartCoroutine(StartPlayText(string.Empty));
                    State = DialogueStates.Finished;
                    OnDialogueFinish?.Invoke();
                }
            }
        }

        private string mCurrentSentence = string.Empty;
        /// <summary>
        /// 文字播放效果
        /// </summary>
        /// <param name="Sentence">要播放的句子</param>
        private IEnumerator StartPlayText(string Sentence)
        {
            mCurrentSentence = Sentence;
            var sentenceToPlay = string.Empty;
            var length = mCurrentSentence.Length;
            for (int i = 0; i < length;i++)
            {
                yield return new WaitForSeconds(0.1f);
                sentenceToPlay = mCurrentSentence.Substring(0,i);
                OnPlayText?.Invoke(sentenceToPlay);
            }
            yield return new WaitForSeconds(0.1f);
            sentenceToPlay = mCurrentSentence;
            OnPlayText?.Invoke(sentenceToPlay);
        }
    }
}