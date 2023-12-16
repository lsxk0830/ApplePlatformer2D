using UnityEngine;

namespace Blue
{
    [CreateAssetMenu(fileName ="AudioConfig",menuName ="Config/AudioConfig")]
    public class AudioConfig : ScriptableObject
    {
        public static AudioConfig Load()
        {
            return Resources.Load<AudioConfig>("AudioConfig");
        }

        public AudioClip DestructTileClip;
    }
}