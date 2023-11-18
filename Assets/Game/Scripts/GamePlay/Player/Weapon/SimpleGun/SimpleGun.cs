namespace Blue
{
    public class SimpleGun : Weapon
    {
        /// <summary>
        /// 子弹的模版
        /// </summary>
        public SimpleButtle BulletTempleta;

        public override void ShootDown()
        {
            var bullet = Instantiate(BulletTempleta);
            bullet.transform.position = BulletTempleta.transform.position;
            bullet.gameObject.SetActive(true);
        }
    }
}