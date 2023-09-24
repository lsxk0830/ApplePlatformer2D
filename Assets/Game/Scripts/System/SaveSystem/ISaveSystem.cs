using System.Linq;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace Blue
{
    public interface ISaveSystem : ISystem
    {
        bool HasSavedKey(string key);
        void AddSaveKey(string key);
        void Save();
        void Load();
        void Clear();
    }
    public class SaveSystem : AbstractSystem, ISaveSystem
    {
        private HashSet<string> mSavedKeys = new HashSet<string>();

        protected override void OnInit()
        {

        }

        public bool HasSavedKey(string key)
        {
            return mSavedKeys.Contains(key);
        }

        public void AddSaveKey(string key)
        {
            mSavedKeys.Add(key);
        }

        public void Save()
        {
            var savedString = string.Join(";@;", mSavedKeys); // 连接指定数组的元素或集合的成员，在每个元素或成员之间使用指定的分隔符
            PlayerPrefs.SetString(nameof(mSavedKeys), savedString);
        }

        public void Load()
        {
            var savedString = PlayerPrefs.GetString(nameof(mSavedKeys), string.Empty);
            mSavedKeys = savedString == string.Empty ? new HashSet<string>() : savedString.Split(";@;").ToHashSet();
        }

        public void Clear()
        {
            PlayerPrefs.SetString(nameof(mSavedKeys), string.Empty);
            mSavedKeys.Clear();
        }

    }
}