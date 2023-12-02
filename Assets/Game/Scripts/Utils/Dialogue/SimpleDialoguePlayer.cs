using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    /// <summary>
    /// 简单的对话播放（）
    /// </summary>
    public class SimpleDialoguePlayer : MonoBehaviour
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

        /// <summary>
        /// 可以让用户配置的对话
        /// </summary>
        public List<string> Sentences = new List<string>();
        public DialogueStates State = DialogueStates.NotStart;
        private Queue<string> mSentenceQueue;

        // 状态事件回调
        public UnityEvent OnDialogueNotStart = new UnityEvent();
        public UnityEvent OnDialogueStart = new UnityEvent();
        public UnityEvent OnDialogueFinish = new UnityEvent();
        // 对外提供设置文本的方法
        public UnityEvent<string> OnPlayText = new UnityEvent<string>();

        private void Awake()
        {
            Reset();
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
                    State = DialogueStates.Finished;
                    OnDialogueFinish?.Invoke();
                }
            }
        }

        /// <summary>
        /// 对外提供一个重置的方法
        /// </summary>
        public void Reset()
        {
            State = DialogueStates.NotStart;
            // 数组转成队列
            mSentenceQueue = new Queue<string>(Sentences);
            OnDialogueNotStart?.Invoke();
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