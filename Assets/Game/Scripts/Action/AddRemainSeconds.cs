using System.Collections.Generic;
using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 增加剩余时间,将AddRemainSeconds改成支持随机和不随机的方式
    /// </summary>
    public class AddRemainSeconds : MonoBehaviour
    {
        /// <summary>
        /// 指定类型
        /// </summary>
        public enum GivenTypes
        {
            Fixed, // 固定的方式
            RandomWithWeight // 权重随机
        }

        /// <summary>
        /// 默认是固定的方式
        /// </summary>
        public GivenTypes GivenType = GivenTypes.Fixed;

        /// <summary>
        /// 权重项
        /// </summary>
        public List<WeightItem> WeightItems = new List<WeightItem>();

        /// <summary>
        /// 权重的配置项
        /// </summary>
        [System.Serializable]
        public class WeightItem
        {
#if UNITY_EDITOR
            /// <summary>
            /// 注释
            /// </summary>
            [TextArea]
            public string Comment;
#endif
            public int MinSeconds;
            public int MaxSeconds;

            /// <summary>
            /// 权重
            /// </summary>
            [Range(0, 1)]
            public float Weight = 0.1f;
        }

        public void Execute(int seconds)
        {
            if (GivenType == GivenTypes.Fixed)
            {
                Bonfire.SetRemainSecondsWithChangerEvent(Bonfire.RemainSeconds + seconds);
            }
            else if (GivenType == GivenTypes.RandomWithWeight)
            {
                var random = UnityEngine.Random.Range(0f, 1f); // 随机 0-1
                var weightSum = 0.0f; // 权重总和
                foreach (var weightItem in WeightItems)
                {
                    // 如果随机出来的随机数命中了某一个权重项
                    if (random >= weightSum && random < weightSum + weightItem.Weight)
                    {
                        // 则进行该权重的随机操作
                        var randomSeconds = Random.Range(weightItem.MinSeconds, weightItem.MaxSeconds + 1);
                        Bonfire.SetRemainSecondsWithChangerEvent(Bonfire.RemainSeconds + randomSeconds);
                        break; // 命中之后退出即可
                    }
                    weightSum += weightItem.Weight;
                }
            }

        }
    }
}