using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Game.PauseMenu
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
        [Header("Menu")]
        [SerializeField] private Button _closeButton;

        private void Start()
        {
            _sliderMusic.onValueChanged.AddListener(SliderMusic_OnValueChanged);
            _audioMixerMusic.GetFloat("volume", out float powerMusic);
            _sliderMusic.SetValueWithoutNotify((powerMusic + 80f) / 100f);

            _sliderSound.onValueChanged.AddListener(SliderSound_OnValueChanged);
            _audioMixerSound.GetFloat("volume", out float powerSound);
            _sliderSound.SetValueWithoutNotify((powerSound + 80f) / 100f);

            _closeButton.onClick.AddListener(CloseButton_OnClick);
            
            gameObject.SetActive(false);
        }

        private void SliderMusic_OnValueChanged(float power) => _audioMixerMusic.SetFloat("volume", power * 100 - 80);
        private void SliderSound_OnValueChanged(float power) => _audioMixerSound.SetFloat("volume", power * 100 - 80);
        private void CloseButton_OnClick() => gameObject.SetActive(false);
    }
}