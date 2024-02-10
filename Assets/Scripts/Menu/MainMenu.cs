using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _continuesButton;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;
        [Space]
        [SerializeField] private GameObject _settingsMenu;

        private void Start()
        {
            _continuesButton.onClick.AddListener(ContinuesButton_OnClick);
            _startButton.onClick.AddListener(StartButton_OnClick);
            _settingsButton.onClick.AddListener(SettingsButton_OnClick);
            _exitButton.onClick.AddListener(ExitButton_OnClick);
        }

        private void ContinuesButton_OnClick() { }
        private void StartButton_OnClick() => SceneManager.LoadScene("GameScene");
        private void SettingsButton_OnClick() => _settingsMenu.SetActive(true);
        private void ExitButton_OnClick() => Application.Quit();
    }
}