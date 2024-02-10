using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioSource _audioSource2;
        [Space]
        [SerializeField] private AudioClip _startClip;
        [SerializeField] private AudioClip _loopClip;

        private void Awake()
        {
            _audioSource2.PlayDelayed(_startClip.length);
        }
    }
}