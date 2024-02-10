using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Game.PauseMenu
{
    public class SettingsMenu : MonoBehaviour
    {
        [Header("Volume")]
        [SerializeField] private Slider _slider;
        [SerializeField] private AudioMixer _audioMixer;
        [Space]
        [Header("Menu")]
        [SerializeField] private Button _closeButton;

        private void Start()
        {
            _slider.onValueChanged.AddListener(Slider_OnValueChanged);
            _audioMixer.GetFloat("MainVolume", out float power);
            _slider.SetValueWithoutNotify((power + 80f) / 100f);

            _closeButton.onClick.AddListener(CloseButton_OnClick);
            
            gameObject.SetActive(false);
        }

        private void Slider_OnValueChanged(float power)
        {
            _audioMixer.SetFloat("MainVolume", power * 100 - 80);
        }

        private void CloseButton_OnClick()
        {
            gameObject.SetActive(false);
        }
    }
}