using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class GoNameRandomizer : MonoBehaviour
    {
        [SerializeField] private string[] _possibleNames;

        private void Start()
        {
            if(_possibleNames != null && _possibleNames.Length > 0)
                name = _possibleNames[Random.Range(0, _possibleNames.Length)];
        }
    }
}
