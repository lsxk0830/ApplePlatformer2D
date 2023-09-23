using UnityEngine;

namespace Blue
{
    /// <summary>
    /// HP-1
    /// </summary>
    public class MinusHP : MonoBehaviour
    {
        public void Execute(int hp)
        {
            var playerModel = ApplePlatformer2D.Interface.GetModel<IPlayerModel>();

            playerModel.HP--;

            if (playerModel.HP <= 0)
            {
                ApplePlatformer2D.IsGameOver = true;
            }
        }
    }
}