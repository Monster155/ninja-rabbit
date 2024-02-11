using System;
using Control;
using UnityEngine;

namespace Rabbit
{
    public class RabbitRunningSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [Space]
        [SerializeField] private AudioClip _running1;
        [SerializeField] private AudioClip _running2;
        [SerializeField] private AudioClip _running3;

        private int _currentId = 0;

        private void Start()
        {
            PlayerInput.Instance.OnShiftButtonDown += PlayerInput_OnShiftButtonDown;
        }

        private void Update()
        {
            if (Math.Abs(PlayerInput.Instance.Movement) > 0.001f)
            {
                if (_audioSource.isPlaying)
                    return;

                _audioSource.Play();
            }
            else
            {
                if (!_audioSource.isPlaying)
                    return;

                _audioSource.Stop();
            }
        }

        private void PlayerInput_OnShiftButtonDown()
        {
            _audioSource.clip = _currentId switch
            {
                0 => _running1,
                1 => _running2,
                _ => _running3
            };
            _currentId = (_currentId + 1) % 3;
        }
    }
}