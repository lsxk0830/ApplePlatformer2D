using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    /// <summary>
    /// HP-1
    /// </summary>
    public class MinusHP : MonoBehaviour
    {
        //public UnityEvent OnDeath;
        public void Execute(int hp)
        {
            var playerModel = ApplePlatformer2D.Interface.GetModel<IPlayerModel>();

            playerModel.HP--;

            if(playerModel.HP<=0)
            {
                //OnDeath?.Invoke();

                ApplePlatformer2D.IsGameOver = true;
            }
        }
    }
}