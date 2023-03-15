using UnityEngine;
using VRC.SDKBase;
using UdonSharp;
using VRC.Udon;
using VRC.SDK3.StringLoading;
using VRC.Udon.Common.Interfaces;

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
    [Header("Name of the custom event")]
    [SerializeField] private string eventName = "_interact";
    [Space]
    [SerializeField] private bool tpPlayerOnJoin = false;
    [SerializeField] private Transform tpLocation;
    [Space]
    [Header("Consider instance creator as VIP")]
    [SerializeField] private bool EmpowerInstanceCreator = false;
    [Space]
    [Header("Remote String loading")]
    [Space]
    [SerializeField] private bool LoadUsersFromURL = false;
    [Tooltip("Character to use to split the string with")]
    public char SplitStringWithCharacter = ',';
    [Tooltip("Use Pastebin RAW or Gist RAW links !")]
    [SerializeField] private VRCUrl linkToString;
    private bool empoweredUser = false;
    private float delay = 0.6f;
    private bool abort;
    private string loadedString;
    private string[] strArr;
    private VRCPlayerApi localPlayer;
    void Start()
    {
        if (playerTag == null) //Checks for valid tag
        {
            abort = true;
            SendCustomEventDelayedSeconds(nameof(_sendDebugError), 1f);
            return;
        }
        localPlayer = Networking.LocalPlayer;
#if !UNITY_EDITOR
        if (Networking.LocalPlayer.isMaster && EmpowerInstanceCreator) //Empowers instance creator if enabled
        {
            localPlayer.SetPlayerTag("rank", playerTag);
            _updateState();
            return;
        }
#endif
        for (int i = 0; i < userArray.Length; i++) //Checks user array for matches with local user to empower
        {
            if (userArray[i] == localPlayer.displayName || empoweredUser)
            {
                localPlayer.SetPlayerTag("rank", playerTag);
                if (tpPlayerOnJoin)
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
        _LoadUrl();
    }

    public void _LoadUrl()
    {
        VRCStringDownloader.LoadUrl(linkToString, (IUdonEventReceiver)this);
    }


    public void _addNewUser() //just an alias
    {
        _updateState();
    }

    public void _updateState() //adds a user to VIPs on calling this function
    {
        if (abort) return;
        VRCPlayerApi localPlayer = Networking.LocalPlayer;
        localPlayer.SetPlayerTag("rank", playerTag);
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

    public override void OnStringLoadSuccess(IVRCStringDownload result)
    {
        loadedString += result.Result;
        strArr = loadedString.Split(SplitStringWithCharacter);
        for (int i = 0; i < strArr.Length; i++)
        {
            if (userArray[i] == localPlayer.displayName || empoweredUser)
            {
                localPlayer.SetPlayerTag("rank", playerTag);
                if (tpPlayerOnJoin)
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
    public override void OnStringLoadError(IVRCStringDownload result)
    {
        Debug.LogError("Reava_UwUtils: <color=red> <b>String failed to load</b></color>: " + result.Error + "| Error Code: " + result.ErrorCode + "On: " + gameObject.name, gameObject);
    }

    public void _initTeleport()
    {
        if (abort) return;
        if (Networking.IsNetworkSettled)
        {
            Networking.LocalPlayer.TeleportTo(tpLocation.position, tpLocation.rotation);
        }
        else
        {
            SendCustomEventDelayedSeconds(nameof(_initTeleport), 0.6f);
        }
    }

    public void _sendDebugError() => Debug.LogError("Reava_UwUtils:<color=red> <b>Invalid values</b></color> or no User / Tag found. (" + gameObject + ")", gameObject);
}