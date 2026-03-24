using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Relay: User Platform")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class PlatformRelay : UdonSharpBehaviour
    {
        [Header("This only works if you build per platform (No multi platform build)")]
        public bool OK = false;
        [Header("Relays for PCVR")]
        [SerializeField] private UdonBehaviour[] PCVR_programRelay;
        [SerializeField] private string[] PCVR_eventNames;
        [Header("Relays for Desktop")]
        [SerializeField] private UdonBehaviour[] Desktop_programRelay;
        [SerializeField] private string[] Desktop_eventNames;
        [Header("Relays for Quest VR")]
        [SerializeField] private UdonBehaviour[] Quest_programRelay;
        [SerializeField] private string[] Quest_eventNames;
        [Header("Relays for Android Mobile")]
        [SerializeField] private UdonBehaviour[] Android_programRelay;
        [SerializeField] private string[] Android_eventNames;
        [Header("Relays for IOS Mobile")]
        [SerializeField] private UdonBehaviour[] IOS_programRelay;
        [SerializeField] private string[] IOS_eventNames;
        [Header("Delay")]
        [SerializeField] private bool delayAction = false;
        [SerializeField] private float delay = 0f;

        private int currentPlatform = 0;
        private VRCPlayerApi player;
        private bool isVR = false;

        void Start()
        {
            player = Networking.LocalPlayer;

            isVR = player.IsUserInVR();

#if UNITY_ANDROID
            currentPlatform = 3;
#endif
#if UNITY_IOS
            currentPlatform = 5;
#endif

            if(currentPlatform == 0 &&  !isVR)
            {
                currentPlatform = 2;
            }
            else if(currentPlatform == 0 && isVR)
            {
                currentPlatform = 1;
            }
            else if (currentPlatform == 3 && !isVR)
            {
                currentPlatform = 4;
            }
        }

        public void _ApplyCurrentPlatform()
        {
            _ApplyPlatform(currentPlatform);
        }

        public void _ApplyPlatform(int platform)
        {
            switch (platform)
            {
                case 0:
                    // None
                    Debug.LogWarning("[Reava_/UwUtils/PlatformRelay.cs]<color=orange>Invalid platform</color>. (" + gameObject + ")", gameObject);
                    break;
                case 1:
                    // PCVR
                    for (int i = 0; i < PCVR_programRelay.Length && i < PCVR_eventNames.Length; i++)
                    {
                        if(PCVR_programRelay[i] != null && PCVR_eventNames[i].Length > 0) PCVR_programRelay[i].SendCustomEvent(PCVR_eventNames[i]);
                    }
                    break;
                case 2:
                    // Desktop
                    for (int i = 0; i < Desktop_programRelay.Length && i < Desktop_eventNames.Length; i++)
                    {
                        if (Desktop_programRelay[i] != null && Desktop_eventNames[i].Length > 0) Desktop_programRelay[i].SendCustomEvent(Desktop_eventNames[i]);
                    }
                    break;
                case 3:
                    // Quest
                    for (int i = 0; i < Quest_programRelay.Length && i < Quest_eventNames.Length; i++)
                    {
                        if (Quest_programRelay[i] != null && Quest_eventNames[i].Length > 0) Quest_programRelay[i].SendCustomEvent(Quest_eventNames[i]);
                    }
                    break;
                case 4:
                    // Android Mobile
                    for (int i = 0; i < Android_programRelay.Length && i < Android_eventNames.Length; i++)
                    {
                        if (Android_programRelay[i] != null && Android_eventNames[i].Length > 0) Android_programRelay[i].SendCustomEvent(Android_eventNames[i]);
                    }
                    break;
                case 5:
                    // IOS Mobile
                    for (int i = 0; i < IOS_programRelay.Length && i < IOS_eventNames.Length; i++)
                    {
                        if (IOS_programRelay[i] != null && IOS_eventNames[i].Length > 0) IOS_programRelay[i].SendCustomEvent(IOS_eventNames[i]);
                    }
                    break;
            }
        }

        public void _ApplyPCVR()
        {
            _ApplyPlatform(1);
        }

        public void _ApplyDesktop()
        {
            _ApplyPlatform(2);
        }

        public void _ApplyQuest()
        {
            _ApplyPlatform(3);
        }

        public void _ApplyIOS()
        {
            _ApplyPlatform(4);
        }
    }
}