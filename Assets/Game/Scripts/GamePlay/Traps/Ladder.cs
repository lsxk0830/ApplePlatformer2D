using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 梯子
    /// </summary>
    public class Ladder : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                other.GetComponent<PlayerClimbLadder>().CanClimb();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                other.GetComponent<PlayerClimbLadder>().CantClimb();
            }
        }
    }
}