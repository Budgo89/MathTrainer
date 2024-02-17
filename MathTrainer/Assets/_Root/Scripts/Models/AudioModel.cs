using _Root.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.Audio;

namespace _Root.Scripts.Models
{
    public class AudioModel
    {
        private AudioMixer _audioMixer;
        private AudioEffectsManager _audioEffectsManager;
        private AudioSource _audioEffects;
        
        public AudioMixer AudioMixer => _audioMixer;
        public AudioEffectsManager AudioEffectsManager => _audioEffectsManager;
        public AudioSource AudioEffects => _audioEffects;

        public AudioModel(AudioMixer audioMixer, AudioEffectsManager audioEffectsManager, AudioSource audioSource)
        {
            _audioMixer = audioMixer;
            _audioEffectsManager = audioEffectsManager;
            _audioEffects = audioSource;
        }
    }
}