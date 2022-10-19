using QFramework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UTGM
{
    public interface ISaveSystem : ISystem
    {
        bool HasSaveKey(string key);
        void AddSavedKey(string key);
        void Load();
        void Save();
        void Clear();
    }

    public class SaveSystem : AbstractSystem, ISaveSystem
    {
        protected override void OnInit()
        {
            
        }

        HashSet<string> mSaveKeys = new HashSet<string>();
        public bool HasSaveKey(string key)
        {
            return mSaveKeys.Contains(key);
        }
        public void AddSavedKey(string key)
        {
            mSaveKeys.Add(key);
        }
        public void Load()
        {
            var savedString = PlayerPrefs.GetString(nameof(mSaveKeys),string.Empty);  // string.Empty: 空字符串

            mSaveKeys = savedString == string.Empty? new HashSet<string>(): savedString.Split(";@;").ToHashSet();
        }
        public void Save()
        {
            var savedString = string.Join(";@;", mSaveKeys);
            PlayerPrefs.SetString(nameof(mSaveKeys),savedString);
        }
        public void Clear()
        {
            PlayerPrefs.SetString(nameof(mSaveKeys), string.Empty);
            mSaveKeys.Clear();
        }
    }
}
