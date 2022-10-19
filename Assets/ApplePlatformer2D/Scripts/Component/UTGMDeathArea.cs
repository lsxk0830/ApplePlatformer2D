using UnityEngine;
using UnityEngine.SceneManagement;

namespace UTGM
{
    public class UTGMDeathArea : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                var playerModel = ApplePlatformer2D.Interface.GetModel<IPlayerModel>();

                playerModel.HP--;

                if (playerModel.HP <= 0)
                    ApplePlatformer2D.IsGameOver = true;
                else
                    SceneManager.LoadScene("Game");
            }
        }
    }
}