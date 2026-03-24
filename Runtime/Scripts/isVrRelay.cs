using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Relay: is user in VR ?")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class isVrRelay : UdonSharpBehaviour
    {
        [Header("Relays for VR")]
        [SerializeField] private UdonBehaviour[] VR_programRelay;
        [SerializeField] private string[] VR_eventNames;
        [Header("Relays for Desktop")]
        [SerializeField] private UdonBehaviour[] Desktop_programRelay;
        [SerializeField] private string[] Desktop_eventNames;
        private VRCPlayerApi player;
        private bool isVR = false;

        void Start()
        {
            player = Networking.LocalPlayer;

            isVR = player.IsUserInVR();
        }

        public void _ApplyCurrentPlatform()
        {
            _ApplyPlatform(isVR);
        }

        public void _ApplyPlatform(bool platform)
        {
            switch (platform)
            {
                case true:
                    // VR
                    for (int i = 0; i < VR_programRelay.Length && i < VR_eventNames.Length; i++)
                    {
                        if (VR_programRelay[i] != null && VR_eventNames[i].Length > 0) VR_programRelay[i].SendCustomEvent(VR_eventNames[i]);
                    }
                    break;
                case false:
                    // Desktop
                    for (int i = 0; i < Desktop_programRelay.Length && i < Desktop_eventNames.Length; i++)
                    {
                        if (Desktop_programRelay[i] != null && Desktop_eventNames[i].Length > 0) Desktop_programRelay[i].SendCustomEvent(Desktop_eventNames[i]);
                    }
                    break;
            }
        }

        public void _ApplyVR()
        {
            _ApplyPlatform(true);
        }

        public void _ApplyDesktop()
        {
            _ApplyPlatform(false);
        }
    }
}