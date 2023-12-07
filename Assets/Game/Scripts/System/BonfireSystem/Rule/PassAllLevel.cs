using UnityEngine.SceneManagement;

namespace Blue
{
    public class PassAllLevel : AbstractBonfireLevelRule
    {
        public override int NeedSeconds { get; set; } = 0;
        public override string Key { get; set; } = nameof(PassAllLevel);
        public override string DisplayName { get; set; }= "通关";

        protected override void OnUnlock()
        {
            base.OnUnlock();

            SceneManager.LoadScene("GamePass");
        }
    }
}