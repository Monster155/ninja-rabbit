using Control;
using UnityEngine;

namespace Rabbit
{
    public class RabbitMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;

        private void FixedUpdate()
        {
            transform.position += new Vector3(1, 0) * (_speed * PlayerInput.Instance.Movement);
        }
    }
}