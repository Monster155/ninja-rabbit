using System;
using Control;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.PauseMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;
        [Space]
        [SerializeField] private GameObject _settingsMenu;

        private void Start()
        {
            PlayerInput.Instance.OnEscButtonClicked += PlayerInput_OnEscButtonClicked;
            
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(ContinueButton_OnClick);
            _settingsButton.onClick.AddListener(SettingsButton_OnClick);
            _exitButton.onClick.AddListener(ExitButton_OnClick);

            PauseController.Instance.SetIsPauseMenuActive(true);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(ContinueButton_OnClick);
            _settingsButton.onClick.RemoveListener(SettingsButton_OnClick);
            _exitButton.onClick.RemoveListener(ExitButton_OnClick);

            PauseController.Instance.SetIsPauseMenuActive(false);
        }
        
        private void PlayerInput_OnEscButtonClicked() => gameObject.SetActive(true);

        private void ContinueButton_OnClick() => gameObject.SetActive(false);
        private void SettingsButton_OnClick() => _settingsMenu.SetActive(true);
        private void ExitButton_OnClick() => SceneManager.LoadScene("MenuScene");
    }
}