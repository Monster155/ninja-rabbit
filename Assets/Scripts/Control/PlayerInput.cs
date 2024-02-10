using UnityEngine;

namespace Control
{
    public class PlayerInput : MonoBehaviour
    {
        public float Movement { get; private set; }
        public bool IsHide { get; private set; }

        public static PlayerInput Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }

        private void Update()
        {
            Movement = Input.GetAxis("Horizontal");
            IsHide = Input.GetKeyDown(KeyCode.Space);
        }
    }
}