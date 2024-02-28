using System;
using System.Collections;
using Control;
using Rabbit;
using UnityEngine;

namespace Enemies.NewPatrol
{
    public class PatrolBot : MonoBehaviour
    {
        [SerializeField] private float _speed = 2.0f; // Скорость монстра
        [SerializeField] private Transform _pointA; // Точка A
        [SerializeField] private Transform _pointB; // Точка B
        [SerializeField] private float _noticeRange = 4.0f; // Расстояние обнаружения
        [SerializeField] private float _attackRange = 1.0f; // Расстояние атаки
        [SerializeField] private Transform _player; // Трансформ игрока
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private AudioSource _attackAudioSource;

        private Coroutine _noticeCoroutine;

        private void Start()
        {
            // Начальная позиция монстра
            transform.position = _pointA.position;
        }

        private void Update()
        {
            if (_noticeCoroutine != null)
                return;

            // Перемещение монстра между точками A и B
            transform.position = Vector3.MoveTowards(transform.position, _pointB.position, _speed * Time.deltaTime);

            // Достижение точки B
            if (transform.position == _pointB.position)
            {
                // Переключение на точку A
                (_pointA, _pointB) = (_pointB, _pointA);
                _renderer.flipX = !_renderer.flipX;
            }

            // Расстояние до игрока
            float distanceToPlayer = Math.Abs(transform.position.x - _player.position.x);

            // Проверка видимости игрока
            bool isPlayerVisible = !PlayerInput.Instance.IsHide;

            // Атака игрока
            if (distanceToPlayer <= _noticeRange && isPlayerVisible)
            {
                _noticeCoroutine = StartCoroutine(Notice());
            }
        }

        private IEnumerator Notice()
        {
            float distanceToPlayer = Math.Abs(transform.position.x - _player.position.x);
            bool isPlayerVisible = !PlayerInput.Instance.IsHide;
            while (distanceToPlayer <= _noticeRange && isPlayerVisible)
            {
                transform.position = Vector3.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
                yield return null;

                distanceToPlayer = Math.Abs(transform.position.x - _player.position.x);
                isPlayerVisible = !PlayerInput.Instance.IsHide;

                if (distanceToPlayer <= _attackRange)
                {
                    // Анимация атаки
                    _animator.SetTrigger("Attack");
                    _attackAudioSource.Play();
                    yield return new WaitForSeconds(2.0f);

                    distanceToPlayer = Math.Abs(transform.position.x - _player.position.x);
                    if (distanceToPlayer <= _attackRange)
                    {
                        // Нанесение урона игроку
                        EventSystem.OnRabbitKill?.Invoke();
                        yield break;
                    }
                    _animator.SetTrigger("Idle");
                }
            }

            _noticeCoroutine = null;
        }
    }
}