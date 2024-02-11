using UnityEngine;

namespace Rabbit
{
    public class RabbitHeart : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _audioSource;

        private float _timer = 0;

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > 5)
                _timer = 0;

            _animator.speed = 1f + _timer;
        }
    }
}