using UnityEngine;
using UTGM;

public class MinusHP : MonoBehaviour
{
    //public UnityEvent OnDeath;
    public void Execute(int hp)
    {
        var playerModel = ApplePlatformer2D.Interface.GetModel<IPlayerModel>();

        playerModel.HP--;

        if (playerModel.HP <= 0)
        {
            //OnDeath?.Invoke();

            ApplePlatformer2D.IsGameOver = true;
        }
    }
}
