using System;
using System.Collections;
using Control;
using Game;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rabbit
{
    public class RabbitController : MonoBehaviour
    {
        [SerializeField] private RabbitMovement _movement;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Animator _animator;

        private Coroutine _hideCoroutine;

        private void Start()
        {
            PlayerInput.Instance.OnSpaceButtonUp += PlayerInput_OnSpaceButtonUp;
            PlayerInput.Instance.OnSpaceButtonDown += PlayerInput_OnSpaceButtonDown;
        }

        private void Update()
        {
            if (transform.position.x > 157f)
                switch (PlayerPrefs.GetInt("Progress", 0))
                {
                    case 1:
                        SceneManager.LoadScene("GameScene2");
                        break;
                    case 2:
                        SceneManager.LoadScene("GameWinScene");
                        break;
                }

            if (PlayerInput.Instance.Movement > 0.01f)
                _renderer.flipX = true;
            else if (PlayerInput.Instance.Movement < -0.01f)
                _renderer.flipX = false;

            _animator.SetFloat("Speed", Math.Abs(PlayerInput.Instance.Movement));
            _animator.speed = PlayerInput.Instance.IsSprint ? 2 : 0.7f;
        }

        private void PlayerInput_OnSpaceButtonUp()
        {
            if (_hideCoroutine != null)
            {
                StopCoroutine(_hideCoroutine);
                _hideCoroutine = null;
            }

            _movement.SetCanMove(true);
            _cameraController.FinishHiding();
        }

        private void PlayerInput_OnSpaceButtonDown()
        {
            _movement.SetCanMove(false);

            if (_hideCoroutine != null)
                StopCoroutine(_hideCoroutine);
            _hideCoroutine = StartCoroutine(HideCoroutine());
        }

        private IEnumerator HideCoroutine()
        {
            yield return new WaitForSeconds(0.2f);

            _cameraController.StartHiding();
        }

#if UNITY_EDITOR

        [CustomEditor(typeof(RabbitController))]
        class RabbitControllerEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                if (GUILayout.Button("Start"))
                    ((RabbitController)target).PlayerInput_OnSpaceButtonUp();
                if (GUILayout.Button("Finish"))
                    ((RabbitController)target).PlayerInput_OnSpaceButtonDown();
            }
        }

#endif

    }
}