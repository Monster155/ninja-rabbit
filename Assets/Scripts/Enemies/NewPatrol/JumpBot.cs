using System;
using System.Collections;
using Control;
using UnityEngine;

namespace Enemies.NewPatrol
{
    public class JumpBot : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _attackRange = 1.0f; // Расстояние атаки
        [SerializeField] private Transform _player; // Трансформ игрока

        private Coroutine _attackCoroutine;

        private void Update()
        {
            if (_attackCoroutine != null)
                return;

            // Расстояние до игрока
            float distanceToPlayer = Math.Abs(transform.position.x - _player.position.x);

            // Проверка, находится ли игрок в радиусе атаки
            bool isPlayerInRange = distanceToPlayer <= _attackRange;

            // Проверка, находится ли игрок в режиме бега
            bool isPlayerRunning = PlayerInput.Instance.IsSprint;

            // Атака игрока
            if (isPlayerInRange && isPlayerRunning)
            {
                _attackCoroutine = StartCoroutine(Attack());
            }
        }

        private IEnumerator Attack()
        {
            // Анимация атаки
            _animator.SetTrigger("Attack");
            yield return new WaitForSeconds(1.0f); // Задержка атаки

            // Нанесение урона игроку
            EventSystem.OnRabbitKill?.Invoke();
        }
    }
}