using System;
using System.Collections;
using Control;
using Game;
using UnityEditor;
using UnityEngine;

namespace Rabbit
{
    public class RabbitController : MonoBehaviour
    {
        [SerializeField] private RabbitMovement _movement;
        [SerializeField] private CameraController _cameraController;

        private Coroutine _hideCoroutine;

        private void Start()
        {
            PlayerInput.Instance.OnSpaceButtonUp += PlayerInput_OnSpaceButtonUp;
            PlayerInput.Instance.OnSpaceButtonDown += PlayerInput_OnSpaceButtonDown;
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
    }
}