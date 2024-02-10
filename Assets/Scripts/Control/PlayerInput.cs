using System;
using UnityEngine;

namespace Control
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action OnSpaceButtonUp;
        public event Action OnSpaceButtonDown;
        public event Action OnEscButtonClicked;
        
        public float Movement { get; private set; }
        public bool IsHide { get; private set; }
        public bool IsSprint { get; private set; }

        public static PlayerInput Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        private void Update()
        {
            Movement = Input.GetAxis("Horizontal");
            IsSprint = Input.GetKey(KeyCode.LeftShift);

            if (Input.GetKeyUp(KeyCode.Space))
                OnSpaceButtonUp?.Invoke();
            if (Input.GetKeyDown(KeyCode.Space))
                OnSpaceButtonDown?.Invoke();

            if (Input.GetKeyDown(KeyCode.Escape))
                OnEscButtonClicked?.Invoke();
        }
    }
}