using System;
using _Scripts.Static;
using TMPro;
using UnityEngine;

namespace _Scripts.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizableTextMeshUGUI : LocalizableObjectBase
    {
        [SerializeField] private TextMeshProUGUI _textMeshPro;

        private void OnValidate()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        private void Awake()
        {
            GameHelper.CheckForNull(_textMeshPro, this);
        }

        public override void Localize(string value)
        {
            if (_textMeshPro != null)
            {
                _textMeshPro.text = value;
            }
        }
    }
}
