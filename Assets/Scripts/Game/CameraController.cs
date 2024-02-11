using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Camera _hideCamera;
        [Space]
        [SerializeField] private Animator _animator;

        private Coroutine _finishHidingCoroutine;

        private void Start()
        {
            _mainCamera.gameObject.SetActive(true);
            _hideCamera.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (transform.position.x < 21.2f)
            {
                var pos = _mainCamera.transform.localPosition;
                pos.x = 21.2f - transform.position.x;
                _mainCamera.transform.localPosition = pos;
            }
            else if (transform.position.x > 145f)
            {
                var pos = _mainCamera.transform.localPosition;
                pos.x = 145f - transform.position.x;
                _mainCamera.transform.localPosition = pos;
            }
        }

        public void StartHiding()
        {
            if (_finishHidingCoroutine != null)
            {
                StopCoroutine(_finishHidingCoroutine);
                _finishHidingCoroutine = null;
            }

            _hideCamera.gameObject.SetActive(true);
            _mainCamera.gameObject.SetActive(false);
            _animator.SetTrigger("Start");
        }

        public void FinishHiding()
        {
            if (_finishHidingCoroutine != null)
            {
                StopCoroutine(_finishHidingCoroutine);
                _finishHidingCoroutine = null;
            }

            _animator.SetTrigger("Finish");
            StartCoroutine(FinishHidingCoroutine());
        }

        private IEnumerator FinishHidingCoroutine()
        {
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

            _mainCamera.gameObject.SetActive(true);
            _hideCamera.gameObject.SetActive(false);
        }

#if UNITY_EDITOR

        [CustomEditor(typeof(CameraController))]
        class CameraControllerEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                if (GUILayout.Button("Start"))
                    ((CameraController)target).StartHiding();
                if (GUILayout.Button("Finish"))
                    ((CameraController)target).FinishHiding();
            }
        }

#endif

    }
}