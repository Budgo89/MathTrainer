using UnityEngine;

namespace _Root.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(AudioEffectsManager), menuName = "MathTrainer/AudioEffectsManager/" + nameof(AudioEffectsManager), order = 0)]
    public class AudioEffectsManager : ScriptableObject
    {
        [SerializeField] private AudioClip _fonClip;
        [SerializeField] private AudioClip _swaipClip;
        [SerializeField] private AudioClip _buttonClick;

        public AudioClip FonClip => _fonClip;
        public AudioClip SwaipClip => _swaipClip;
        public AudioClip ButtonClick => _buttonClick;
    }
}