using _Scripts.Static;
using UnityEngine;

namespace _Scripts.Localization
{
    public class SceneLocalizer : MonoBehaviour
    {
        [SerializeField] private GameLocalization _gameLocalization;
        [SerializeField] private LocalizableObjectBase[] _localizableObjects;
        private void OnValidate()
        {
            if (_gameLocalization == null)
            {
                _gameLocalization = FindObjectOfType<GameLocalization>();
            }
            
            _localizableObjects = FindObjectsOfType<LocalizableObjectBase>(true);
        }

        private void Awake()
        {
            GameHelper.CheckForNull(_gameLocalization, this);

            if (_gameLocalization != null)
            {
                _gameLocalization.OnLocalizationLoadingDone += GameLocalizationOnLocalizationLoadingDone;
            }
        }

        private void OnDestroy()
        {
            if (_gameLocalization != null)
            {
                _gameLocalization.OnLocalizationLoadingDone -= GameLocalizationOnLocalizationLoadingDone;
            }
        }

        private void GameLocalizationOnLocalizationLoadingDone()
        {
            foreach (var localizable in _localizableObjects)
            {
                if (localizable != null)
                {
                    var key = localizable.LocalizeKey;
                    var value = _gameLocalization.GetLocalizedString(key);
                    localizable.Localize(value);
                }
            }
        }
    }
}
