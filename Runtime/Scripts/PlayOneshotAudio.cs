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

        public void _PlayAudio()
        {
            if (!AudioSource)
            {
                Debug.LogWarning($"[Reava_/UwUtils/PlayOneshotAudio.cs] {nameof(AudioSource)} is not set");
                return;
            }

            AudioSource.time = 0;
            AudioSource.PlayOneShot(AudioSource.clip);
        }
    }
}