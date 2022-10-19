using UnityEngine;

namespace UTGM
{
    public class SaveHideState : MonoBehaviour
    {
        public string SaveKey;

        public void Execute()
        {
            ApplePlatformer2D.Interface.GetSystem<ISaveSystem>().AddSavedKey(SaveKey);
        }
    }
}