using System;
using System.Collections;
using Control;
using Rabbit;
using UnityEngine;

namespace Enemies.NewPatrol
{
    public class JumpBot : MonoBehaviour
    {
        [SerializeField] private GameObject _hidden;
        [SerializeField] private GameObject _jumped;
        [SerializeField] private float _attackRange = 1.0f; // Расстояние атаки
        [SerializeField] private Transform _player; // Трансформ игрока
        [SerializeField] private RabbitMovement _rabbitMovement;

        private Coroutine _attackCoroutine;

        private void Start()
        {
            _hidden.SetActive(true);
            _jumped.SetActive(false);
        }

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
            _hidden.SetActive(false);
            _jumped.SetActive(true);
            _rabbitMovement.SetCanMove(false);
            yield return new WaitForSeconds(1.0f); // Задержка атаки

            // Нанесение урона игроку
            EventSystem.OnRabbitKill?.Invoke();
        }
    }
}