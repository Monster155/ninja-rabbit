using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class WinMenu : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;

        private void Start()
        {
            _continueButton.onClick.AddListener(ContinueButton_OnClick);
        }

        private void ContinueButton_OnClick()
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}