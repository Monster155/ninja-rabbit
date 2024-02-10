using Control;
using UnityEngine;

namespace Rabbit
{
    public class RabbitMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;

        private bool _isCanMove = true;

        private void FixedUpdate()
        {
            if (!_isCanMove)
                return;

            transform.position += new Vector3(1, 0)
                                  * (_speed * PlayerInput.Instance.Movement
                                            * (PlayerInput.Instance.IsSprint ? 2 : 1));
        }

        public void SetCanMove(bool isCanMove) => _isCanMove = isCanMove;
    }
}