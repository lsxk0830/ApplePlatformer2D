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
        private IBonfireRule mSimpleGunRule;

        private void Awake()
        {
            mInputSystem = ApplePlatformer2D.Interface.GetSystem<IInputSystem>();

            mSimpleGunRule = ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>().GetRuleByKey(nameof(SimpleGunRule));
            if (mSimpleGunRule.Unlocked)
                CurrentWeapon = transform.GetComponentInChildren<SimpleGun>();
            else
                CurrentWeapon = null;
            ApplePlatformer2D.OnBonfireRuleUnlocked.Register(ruleName =>
            {
                if (ruleName == nameof(SimpleGunRule))
                    CurrentWeapon = transform.GetComponentInChildren<SimpleGun>();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnDestroy()
        {
            mInputSystem = null;
            mSimpleGunRule = null;
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