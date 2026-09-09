using UnityEngine;
using UdonSharp;
using VRC.SDKBase;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Play Oneshot Audio")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class PlayOneshotAudio : UdonSharpBehaviour
    {
        [Header("References")]
        [SerializeField] private AudioSource AudioSource;
        [SerializeField] private AudioClip clip;

        public override void Interact()
        {
            _PlayAudio();
        }

        public override void OnPickupUseDown()
        {
            _PlayAudio();
        }

        public void _PlayAudio()
        {
            if (!AudioSource)
            {
                Debug.LogWarning($"[Reava_/UwUtils/PlayOneshotAudio.cs] {nameof(AudioSource)} is not set");
                return;
            }

            AudioSource.PlayOneShot(clip);
        }
    }
}