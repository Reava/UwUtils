using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using UwUtils;

namespace UwUtils
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class FadingTP : UdonSharpBehaviour
    {
        [Range(0f, 3f)]
        [SerializeField] private float FadeSpeed = 1f;
        [SerializeField] private DynamicCallback Dyncallback;
        [SerializeField] private Animator AnimatorTarget;
        [SerializeField] private string ParameterName = "TP";
        [SerializeField] private Transform targetLocation;

        public void Start()
        {
            if (!Dyncallback || !targetLocation) _sendDebugError();
            AnimatorTarget.SetBool(ParameterName, false);
        }

        public override void Interact()
        {
            AnimatorTarget.SetFloat("fadeSpeed", FadeSpeed);
            AnimatorTarget.SetBool(ParameterName, true);
            Dyncallback._selectedOutput(this.gameObject);
        }

        public void _teleportPlayer()
        {
            if (Networking.LocalPlayer != null) Networking.LocalPlayer.TeleportTo(targetLocation.position, targetLocation.rotation);
            AnimatorTarget.SetBool(ParameterName, false);
        }

        private void _sendDebugError() => Debug.LogError("[Reava_/UwUtils/FadingTP.cs]:<color=red> <b>Invalid references</b></color>, please review <color=orange>references</color> on: " + gameObject + ".", gameObject);
    }
}