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

            float moveDeltaX = _speed * PlayerInput.Instance.Movement
                                          * (PlayerInput.Instance.IsSprint ? 2 : 1);
            if (transform.position.x + moveDeltaX < 9.5f)
                return;

            transform.position += new Vector3(moveDeltaX, 0);
        }

        public void SetCanMove(bool isCanMove) => _isCanMove = isCanMove;
    }
}