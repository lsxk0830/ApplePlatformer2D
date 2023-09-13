using UnityEngine;

namespace Blue
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform Player;

        void Update()
        {
            var camPos = transform.position;

            camPos.x = Player.position.x;

            camPos.y = Player.position.y + 2;

            transform.position = camPos;
        }
    }
}