using System;
using System.Collections;
using Control;
using UnityEngine;

namespace Enemies.NewPatrol
{
    public class FakeJumpBot : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _noticeRange = 2.0f; // Расстояние обнаружения
        [SerializeField] private float _attackRange = 1.0f; // Расстояние атаки
        [SerializeField] private Transform _player; // Трансформ игрока

        private Coroutine _attackCoroutine;

        private void Update()
        {
            if (_attackCoroutine != null)
                return;

            // Расстояние до игрока
            float distanceToPlayer = Math.Abs(transform.position.x - _player.position.x);

            // Атака игрока
            if (distanceToPlayer <= _noticeRange && PlayerInput.Instance.IsSprint)
            {
                _attackCoroutine = StartCoroutine(Attack());
            }
        }

        private IEnumerator Attack()
        {
            // Анимация атаки
            _animator.SetTrigger("Attack");
            yield return new WaitForSeconds(1.0f); // Задержка атаки

            float timer = 5;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;

                float distanceToPlayer = Math.Abs(transform.position.x - _player.position.x);
                if (distanceToPlayer <= _attackRange)
                {
                    EventSystem.OnRabbitKill?.Invoke();
                    yield break;
                }
            }

            Destroy(gameObject);
        }
    }
}