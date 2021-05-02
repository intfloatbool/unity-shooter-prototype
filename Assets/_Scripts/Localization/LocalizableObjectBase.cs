using UnityEngine;

namespace _Scripts.Localization
{
    public abstract class LocalizableObjectBase : MonoBehaviour
    {
        [SerializeField] protected string _localizeKey = "undefined";
        public string LocalizeKey => _localizeKey;
        
        public abstract void Localize(string value);
    }
}
