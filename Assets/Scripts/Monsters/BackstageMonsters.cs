using System;
using UnityEngine;

namespace Monsters
{
    public class BackstageMonsters : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _finishX;
        [Space]
        [SerializeField] private Transform _rabbit;
        [SerializeField] private float _startRabbitX;
        [Space]
        [SerializeField] private AudioSource _audioSource;

        private bool _isActive = false;

        private void Update()
        {
            if (_rabbit.position.x > _startRabbitX)
            {
                _isActive = true;
                _audioSource.Play();
            }
        }

        private void FixedUpdate()
        {
            if (!_isActive)
                return;

            transform.position += new Vector3(_speed, 0);

            if (transform.position.x > _finishX)
                Destroy(gameObject);
        }
    }
}