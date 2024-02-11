using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Menu
{
    public class SettingsMenu : MonoBehaviour
    {
        [Header("Music Volume")]
        [SerializeField] private Slider _sliderMusic;
        [SerializeField] private AudioMixer _audioMixerMusic;
        [Space]
        [Header("Sound Volume")]
        [SerializeField] private Slider _sliderSound;
        [SerializeField] private AudioMixer _audioMixerSound;
        [Space]
        [Header("Fullscreen")]
        [SerializeField] private Toggle _fullscreenToggle;
        [Space]
        [Header("Resolution")]
        [SerializeField] private TMP_Dropdown _resolutionsDropdown;
        [Space]
        [Header("Menu")]
        [SerializeField] private Button _closeButton;

        private Resolution[] _resolutions;

        private void Start()
        {
            _sliderMusic.onValueChanged.AddListener(SliderMusic_OnValueChanged);
            _audioMixerMusic.GetFloat("volume", out float powerMusic);
            _sliderMusic.SetValueWithoutNotify((powerMusic + 80f) / 100f);
            
            _sliderSound.onValueChanged.AddListener(SliderSound_OnValueChanged);
            _audioMixerSound.GetFloat("volume", out float powerSound);
            _sliderSound.SetValueWithoutNotify((powerSound + 80f) / 100f);

            _fullscreenToggle.onValueChanged.AddListener(FullscreenToggle_OnValueChanged);
            _fullscreenToggle.isOn = Screen.fullScreen;

            _resolutions = Screen.resolutions;
            _resolutionsDropdown.ClearOptions();
            List<string> options = new List<string>();
            int currentResolutionIndex = 0;
            for (int i = 0; i < _resolutions.Length; i++)
            {
                options.Add(_resolutions[i].width + "x" + _resolutions[i].height);
                if (_resolutions[i].width == Screen.currentResolution.width &&
                    _resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            _resolutionsDropdown.AddOptions(options);
            _resolutionsDropdown.value = currentResolutionIndex;
            _resolutionsDropdown.RefreshShownValue();
            _resolutionsDropdown.onValueChanged.AddListener(ResolutionsDropdown_OnValueChanged);

            _closeButton.onClick.AddListener(CloseButton_OnClick);
            
            gameObject.SetActive(false);
        }

        private void SliderMusic_OnValueChanged(float power) => _audioMixerMusic.SetFloat("volume", power * 100 - 80);
        private void SliderSound_OnValueChanged(float power) => _audioMixerSound.SetFloat("volume", power * 100 - 80);
        private void FullscreenToggle_OnValueChanged(bool isEnabled) => Screen.fullScreen = isEnabled;
        
        private void ResolutionsDropdown_OnValueChanged(int resolutionIndex)
        {
            Resolution resolution = _resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        private void CloseButton_OnClick() => gameObject.SetActive(false);
    }
}