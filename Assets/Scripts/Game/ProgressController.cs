using UnityEngine;

namespace Game
{
    public class ProgressController : MonoBehaviour
    {
        [SerializeField] private int _currentProgress;

        private void Start()
        {
            PlayerPrefs.SetInt("Progress", _currentProgress);
        }
    }
}