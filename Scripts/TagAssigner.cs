using UnityEngine;
using VRC.SDKBase;
using UdonSharp;
using VRC.Udon;
using VRC.SDK3.StringLoading;
using VRC.Udon.Common.Interfaces;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/TagAssigner")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class TagAssigner : UdonSharpBehaviour
    {
        [Header("Name of the tag to assign to VIPs")]
        [SerializeField] private string playerTag = "vip";
        [Header("List of users who gets 'VIP'")]
        [SerializeField] private string[] userArray;
        [Space]
        [Header("List of objects to toggle ON for VIPs")]
        [SerializeField] private GameObject[] toggleObjectsON;
        [Header("List of objects to toggle OFF for VIPs")]
        [SerializeField] private GameObject[] toggleObjectsOFF;
        [Space]
        [Header("Send custom event to these behaviors if local user is VIP")]
        [SerializeField] private UdonBehaviour[] programsSuccess;
        [Tooltip("Name of the custom event")]
        [SerializeField] private string eventName = "_interact";
        [Space]
        [SerializeField] private bool teleportVipOnJoin = false;
        [SerializeField] private Transform tpLocation;
        [Space]
        [Header("Consider instance creator as VIP")]
        [SerializeField] private bool EmpowerInstanceCreator = false;
        [Space]
        [Header("Remote String loading")]
        [Space]
        [SerializeField] private bool LoadUsersFromURL = false;
        [Tooltip("Character to use to split the string with")]
        [SerializeField] private char SplitStringWithCharacter = ',';
        [Tooltip("Use Pastebin RAW or Gist RAW links !")]
        [SerializeField] private VRCUrl linkToString;
        [HideInInspector] public string loadedString;
        [HideInInspector] public string[] strArr;
        private bool empoweredUser = false;
        private float delay = 0.6f;
        private VRCPlayerApi localPlayer;

        void Start()
        {
            if (playerTag == null) _sendDebugError(); //Checks for valid tag
            localPlayer = Networking.LocalPlayer;
#if !UNITY_EDITOR //Just here to remove the warning in editor
        if (Networking.LocalPlayer.isMaster && EmpowerInstanceCreator) //Empowers instance creator if enabled
        {
            localPlayer.SetPlayerTag("rank", playerTag);
            _updateState();
            return;
        }
#endif
            if (linkToString != null && LoadUsersFromURL) _LoadUrl();
            if (userArray != null) _checkUserArray(); else _sendDebugError();
        }

        public void _checkUserArray() //Checks static user array for matches with local user to empower
        {
            for (int i = 0; i < userArray.Length; i++)
            {
                if (userArray[i] == localPlayer.displayName || empoweredUser)
                {
                    if (playerTag != null) localPlayer.SetPlayerTag("rank", playerTag);
                    if (teleportVipOnJoin)
                    {
                        SendCustomEventDelayedSeconds(nameof(_initTeleport), delay);
                    }
                    _updateState();
                    break;
                }
                else
                {
                    if (Networking.LocalPlayer.GetPlayerTag("rank") == null)
                    {
                        localPlayer.SetPlayerTag("rank", "Visitor");
                    }
                }
            }
        }

        public void _LoadUrl() => VRCStringDownloader.LoadUrl(linkToString, (IUdonEventReceiver)this);

        public void _addNewUser() => _updateState(); // Alias.
        public void _updateState() //adds the local user to VIPs on calling this function
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (playerTag != null) localPlayer.SetPlayerTag("rank", playerTag);
            foreach (GameObject o in toggleObjectsON)
            {
                o.SetActive(true);
            }
            foreach (GameObject o in toggleObjectsOFF)
            {
                o.SetActive(false);
            }
            if (programsSuccess[0] != null && !programsSuccess[0].Equals(null))
            {
                foreach (UdonBehaviour b in programsSuccess)
                {
                    b.SendCustomEvent(eventName);
                }
            }
        }

        public override void OnStringLoadSuccess(IVRCStringDownload result) //This will generally only happen a few seconds after Start() happens because of VRChat limitations.
        {
            loadedString += result.Result;
            strArr = loadedString.Split(SplitStringWithCharacter);
            for (int i = 0; i < strArr.Length; i++)
            {
                if (strArr[i] == localPlayer.displayName || empoweredUser)
                {
                    if (playerTag != null) localPlayer.SetPlayerTag("rank", playerTag);
                    if (teleportVipOnJoin)
                    {
                        SendCustomEventDelayedSeconds(nameof(_initTeleport), delay);
                    }
                    _updateState();
                    break;
                }
                else
                {
                    if (Networking.LocalPlayer.GetPlayerTag("rank") == null)
                    {
                        localPlayer.SetPlayerTag("rank", "Visitor");
                    }
                }
            }
        }

        public override void OnStringLoadError(IVRCStringDownload result) => Debug.LogError("<b>Reava_UwUtils: <color=red> String failed to load</b></color>: " + result.Error + "| Error Code: " + result.ErrorCode + "On: " + gameObject.name, gameObject);

        public void _initTeleport()
        {
            if (Networking.IsNetworkSettled && teleportVipOnJoin)
            {
                Networking.LocalPlayer.TeleportTo(tpLocation.position, tpLocation.rotation);
                teleportVipOnJoin = false;
            }
            else
            {
                SendCustomEventDelayedSeconds(nameof(_initTeleport), 0.6f);
            }
        }

        public void _sendDebugError() => Debug.LogError("Reava_UwUtils:<color=red> <b>Invalid values found.</b></color> (" + gameObject.name + ")", gameObject);
    }
}