using UnityEngine;

namespace Canoe
{
    public class KeyboardMovement : MonoBehaviour
    {
        CharacterController _controller;
        [SerializeField] float _speed = 1;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _controller.Move(move * Time.deltaTime * _speed);
        }
    }
}
