using System;
using UnityEngine;

namespace Control
{
    public class PauseController : MonoBehaviour
    {
        public event Action<bool> PauseStateChanged; 
        
        public bool IsPaused => _isPauseMenuActive || _isTutorialActive || _isRabbitDead;

        private bool _isPauseMenuActive;
        private bool _isTutorialActive;
        private bool _isRabbitDead;
        
        public static PauseController Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        public void SetIsPauseMenuActive(bool isPauseMenuActive)
        {
            _isPauseMenuActive = isPauseMenuActive;
            PauseStateChanged?.Invoke(IsPaused);
        }

        public void SetIsTutorialActive(bool isTutorialActive)
        {
            _isTutorialActive = isTutorialActive;
            PauseStateChanged?.Invoke(IsPaused);
        }

        public void SetIsRabbitDeadActive(bool isRabbitDead)
        {
            _isRabbitDead = isRabbitDead;
            PauseStateChanged?.Invoke(IsPaused);
        }
    }
}