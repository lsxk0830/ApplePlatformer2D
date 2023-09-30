using UnityEngine;

namespace Blue
{
    public class SaveHideState : MonoBehaviour
    {
        public string SaveKey;

        public void Execute()
        {
            ApplePlatformer2D.Interface.GetSystem<ISaveSystem>().AddSaveKey(SaveKey);
        }
    }
}