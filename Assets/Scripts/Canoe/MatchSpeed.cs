using UnityEngine;

namespace Canoe
{
    public class MatchSpeed : MonoBehaviour
    {
        [SerializeField] private PlaceholderCanoeMove _move;
        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_move._move);
        }
    }
}
