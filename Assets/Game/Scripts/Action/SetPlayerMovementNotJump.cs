using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 设置玩家不能跳跃
    /// </summary>
    public class SetPlayerMovementNotJump : MonoBehaviour
    {
        public void Execute()
        {
            var player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerMovement>().JumpState = PlayerMovement.JumpStates.NotJump;
        }
    }
}