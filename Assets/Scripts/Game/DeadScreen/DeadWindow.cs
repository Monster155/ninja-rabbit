using System;
using Control;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.DeadScreen
{
    public class DeadWindow : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;

        private void Start()
        {
            EventSystem.OnRabbitKill += OnRabbitKill;

            _restartButton.onClick.AddListener(RestartButton_OnClick);
            
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            EventSystem.OnRabbitKill -= OnRabbitKill;
        }

        private void OnRabbitKill()
        {
            PauseController.Instance.SetIsRabbitDeadActive(true);
            gameObject.SetActive(true);
        }

        private void RestartButton_OnClick()
        {
            gameObject.SetActive(false);
            PauseController.Instance.SetIsRabbitDeadActive(false);

            // EventSystem.OnRabbitAlive?.Invoke();
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}