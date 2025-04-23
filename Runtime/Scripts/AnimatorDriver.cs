using UdonSharp;
using UnityEngine;
using VRC.SDK3.Persistence;
using VRC.SDKBase;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/AnimatorDriver")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class AnimatorDriver : UdonSharpBehaviour
    {
        [SerializeField] private Animator Animator;
        [SerializeField] private string ParameterName;
        [SerializeField] private bool defaultValue;
        [Header("Save state with persistence?")]
        [SerializeField] private bool saveState = false;
        [Header("Use a unique Parameter per toggle!")]
        [SerializeField] private string persistenceParameter = "animator_toggle_example";
        private bool abort;

        public void Start()
        {
            if (Animator == null || ParameterName == null)
            {
                abort = true;
                SendCustomEventDelayedSeconds(nameof(_sendDebugError), 1f);
                return;
            }
            Animator.SetBool(ParameterName, defaultValue);
            if (saveState) PlayerData.SetBool(persistenceParameter, defaultValue);
        }

        public override void Interact()
        {
            _Toggle();
        }

        public void _Toggle()
        {
            if (abort) return;
            defaultValue = !defaultValue;
            Animator.SetBool(ParameterName, defaultValue);
            if (saveState) PlayerData.SetBool(persistenceParameter, defaultValue);
        }

        public override void OnPlayerRestored(VRCPlayerApi player)
        {
            if (!abort && saveState)
            {
                if (Networking.LocalPlayer != player) return;
                if (!PlayerData.HasKey(player, persistenceParameter)) return;
                if (PlayerData.GetType(player, persistenceParameter) != typeof(bool)) return;

                bool recoveredState = PlayerData.GetBool(player, persistenceParameter);
                Animator.SetBool(ParameterName, recoveredState);
                defaultValue = recoveredState;
            }
        }

        public void _sendDebugError() => Debug.LogError("[Reava/UwUtils/AnimatorDriver.cs] Invalid references, please review Animator reference / Parameter name on: '" + gameObject + "'", gameObject);
    }
}