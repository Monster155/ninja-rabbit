using System;
using System.Collections;
using Control;
using UnityEngine;

namespace Monsters.AfkMonster
{
    public class AfkMonster : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _playerXtoStart;
        [Space]
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationClip _preparingClip;
        [SerializeField] private AnimationClip _attackClip;

        private Coroutine _activatingCoroutine;
        
        private bool _isStarted = false;
        private bool _isPaused = false;

        private void Start()
        {
            PauseController.Instance.PauseStateChanged += isPaused => _isPaused = isPaused;
        }

        private void Update()
        {
            if (_isPaused)
                return;

            if (_playerTransform.position.x < _playerXtoStart)
                return;

            transform.position = _playerTransform.position;

            if (Math.Abs(PlayerInput.Instance.Movement) > 0.001f)
            {
                if (!_isStarted)
                    return;

                _isStarted = false;

                _animator.speed = 1f;
                _animator.SetTrigger("Hide");

                if (_activatingCoroutine != null)
                {
                    StopCoroutine(_activatingCoroutine);
                    _activatingCoroutine = null;
                }
            }
            else
            {
                if (_isStarted)
                    return;

                _isStarted = true;

                _activatingCoroutine = StartCoroutine(ActivatingCoroutine());
            }
        }

        private IEnumerator ActivatingCoroutine()
        {
            yield return TimerCoroutine(3f);

            float time = 10f;

            _animator.SetTrigger("Preparing");
            _animator.speed = _preparingClip.length / time;

            yield return TimerCoroutine(time);

            _animator.speed = 1f;
            _animator.SetTrigger("Attack");

            EventSystem.OnRabbitKill?.Invoke();
        }

        private IEnumerator TimerCoroutine(float time)
        {
            for (float i = 0; i < time; i += Time.deltaTime)
            {
                yield return null;

                float currentSpeed = _animator.speed;

                while (_isPaused)
                {
                    _animator.speed = 0;
                    yield return null;
                }

                _animator.speed = currentSpeed;
            }
        }
    }
}