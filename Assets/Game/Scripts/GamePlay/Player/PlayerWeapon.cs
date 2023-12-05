using QFramework;
using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 玩家武器
    /// </summary>
    public class PlayerWeapon : MonoBehaviour
    {
        private IInputSystem mInputSystem;

        private void Awake()
        {
            mInputSystem = ApplePlatformer2D.Interface.GetSystem<IInputSystem>();
            CurrentWeapon = transform.GetComponentInChildren<SimpleGun>(); // 默认自带简单枪
        }

        private void OnDestroy()
        {
            mInputSystem = null;
        }

        /// <summary>
        /// 当前的武器
        /// </summary>
        public Weapon CurrentWeapon;

        private void Update()
        {
            if (mInputSystem.ShootDown)
            {
                CurrentWeapon?.ShootDown();
            }

            if (mInputSystem.Shoot)
            {
                CurrentWeapon?.Shoot();
            }

            if (mInputSystem.ShootUp)
            {
                CurrentWeapon?.ShootUp();
            }
        }
    }
}