using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using _Scripts.Static;
using Newtonsoft.Json;
using UnityEngine;
namespace _Scripts.Localization
{
    public class GameLocalization : MonoBehaviour
    {
        
        [System.Serializable]
        private class LocalizationDefinition
        {
            [SerializeField] private SystemLanguage _language;
            public SystemLanguage Language => _language;
            
            [SerializeField] private string _localizationPath;
            public string LocalizationPath => _localizationPath;
        }

        [SerializeField] private SystemLanguage _currentLocalization;
        [SerializeField] private LocalizationDefinition[] _localizationDefinitions;
        private Dictionary<string, string> _currentLocDict;

        public event Action OnLocalizationLoadingDone;
        
        private void Start()
        {
            StartCoroutine(InitLocalizationCoroutine());
        }

        private IEnumerator InitLocalizationCoroutine()
        {
            LocalizationDefinition localizationDefinition = default;
            foreach (var locDef in _localizationDefinitions)
            {
                if (locDef == null)
                {
                    continue;
                }

                if (locDef.Language == _currentLocalization)
                {
                    localizationDefinition = locDef;
                    break;
                }
            } 
            
            GameHelper.CheckForNull(localizationDefinition, this);

            if (localizationDefinition == null)
                yield break;
            
            
#if UNITY_EDITOR
            string filePath = Path.Combine (Application.streamingAssetsPath, localizationDefinition.LocalizationPath);
 
#elif UNITY_IOS
        string filePath = Path.Combine (Application.streamingAssetsPath + "/Raw", localizationDefinition.LocalizationPath);
 
#elif UNITY_ANDROID
        string filePath = Path.Combine ("jar:file://" + Application.streamingAssetsPath + "!assets/", localizationDefinition.LocalizationPath);
#endif

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText (filePath);
                _currentLocDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataAsJson);
                
                Debug.Log($"Localization data has been loaded! Keys: {_currentLocDict.Keys.Count}");
                
                OnLocalizationLoadingDone?.Invoke();
            }
            else
            {
                Debug.LogError ($"Cannot find the file! At path: {filePath}");
            }
        }

        public string GetLocalizedString(string key)
        {
            if (_currentLocDict.TryGetValue(key, out string localized))
            {
                return localized;
            }
            
            Debug.LogError($"Cannot find localization with key {key}!");

            return key;
        }
    }
}
