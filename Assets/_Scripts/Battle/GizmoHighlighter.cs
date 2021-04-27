using UnityEngine;

namespace _Scripts.Battle
{
    public class GizmoHighlighter : MonoBehaviour
    {
        [SerializeField] private Color _color = Color.red;
        [SerializeField] private bool _isWire = false;
        [SerializeField] private float _size = 1f;
        private void OnDrawGizmos()
        {
            Gizmos.color = _color;
            if (_isWire)
            {
                Gizmos.DrawWireSphere(transform.position, _size);
            }
            else
            {
                Gizmos.DrawSphere(transform.position, _size);
            }
        }
    }
}
