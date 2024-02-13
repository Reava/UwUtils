using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/JoinBell")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class JoinBell : UdonSharpBehaviour
    {
        [Header("References")]
        [SerializeField] private AudioSource AudioSource;
        [SerializeField] private AudioClip JoinSound;
        [SerializeField] private AudioClip LeaveSound;
        [Header("Defaults")]
        [SerializeField] private bool JoinEnable = true;
        private bool abort = false;

        private void Start()
        {
            if (AudioSource == null)
            {
                _sendDebugError("No audio source found, disabling script");
                abort = true;
                return;
            }
        }
        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            if (abort) return;
            if (JoinSound != null && JoinEnable)
            {
                AudioSource.clip = JoinSound;
                AudioSource.Play();
            }
        }
        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            if (abort) return;
            if (LeaveSound != null && JoinEnable)
            {
                AudioSource.clip = LeaveSound;
                AudioSource.Play();
            }
        }

        public void _JoinToggle()
        {
            JoinEnable = !JoinEnable;
        }

        public void _sendDebugError(string e)
        {
            Debug.LogError("Reava_UwUtils:<color=red> "+e+"</color>. (" + gameObject + ")", gameObject);
        }
    }
}