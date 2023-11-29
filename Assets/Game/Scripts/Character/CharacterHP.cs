using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    /// <summary>
    /// 角色HP
    /// </summary>
    public class CharacterHP : MonoBehaviour
    {
        public float HP = 1; // 角色的血量值（当前）
        public float MaxHP = 1; // 最大血量

        public UnityEvent OnDeath = new UnityEvent(); // 对外提供
        public UnityEvent<float, float,float> OnHPChanged = new UnityEvent<float, float,float>(); // 对外提供血量变更事件,HP，MaxHP,MinusHP

        /// <summary>
        /// 减少HP
        /// </summary>
        public void MinusHP(float minusHP)
        {
            HP -= minusHP;
            if (HP <= 0)
            {
                HP = 0;
                OnDeath?.Invoke(); // 调用死亡事件
            }
            OnHPChanged?.Invoke(HP,MaxHP,-minusHP); // 调用血量变更事件
        }
    }
}