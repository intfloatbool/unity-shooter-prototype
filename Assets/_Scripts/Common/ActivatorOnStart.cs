using UnityEngine;

namespace _Scripts.Common
{
    public class ActivatorOnStart : MonoBehaviour
    {
        [SerializeField] private GameObject[] _gameObjects;

        private void Start()
        {
            foreach (var go in _gameObjects)
            {
                if (go != null)
                {
                    go.SetActive(true);
                }
            }
        }
    }
}
