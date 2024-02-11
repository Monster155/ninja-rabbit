using System;
using UnityEngine;

namespace Control
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action OnSpaceButtonUp;
        public event Action OnSpaceButtonDown;
        public event Action OnEscButtonClicked;
        public event Action OnShiftButtonUp;
        public event Action OnShiftButtonDown;

        public float Movement { get; private set; }
        public bool IsHide { get; private set; }
        public bool IsSprint { get; private set; }

        private bool _canControl = true;

        public static PlayerInput Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        private void Start()
        {
            PauseController.Instance.PauseStateChanged += isPaused => _canControl = !isPaused;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                OnEscButtonClicked?.Invoke();

            Movement = 0;
            IsSprint = false;
            
            if (!_canControl)
                return;

            Movement = Input.GetAxis("Horizontal");
            IsSprint = Input.GetKey(KeyCode.LeftShift);
            
            if(Input.GetKeyUp(KeyCode.LeftShift))
                OnShiftButtonUp?.Invoke();
            if(Input.GetKeyDown(KeyCode.LeftShift))
                OnShiftButtonDown?.Invoke();

            if (Input.GetKeyUp(KeyCode.Space))
                OnSpaceButtonUp?.Invoke();
            if (Input.GetKeyDown(KeyCode.Space))
                OnSpaceButtonDown?.Invoke();
        }
    }
}