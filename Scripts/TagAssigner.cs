using UnityEngine;
using VRC.SDKBase;
using UdonSharp;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class TagAssigner : UdonSharpBehaviour
{
    [Header("Name of the tag to assign to VIPs")]
    [SerializeField] private string playerTag = "vip";
    [Header("List of users who gets 'VIP'")]
    [SerializeField] private string[] userArray;
    [Header("List of objects to toggle ON for VIPs")]
    [SerializeField] private GameObject[] toggleObjectsON;
    [Header("List of objects to toggle OFF for VIPs")]
    [SerializeField] private GameObject[] toggleObjectsOFF;
    [Header("Send custom event to these behaviors if local user is VIP")]
    [SerializeField] private UdonBehaviour[] programsSuccess;
    [Header("Name of the custom event")]
    [SerializeField] private string eventName = "_interact";
    [SerializeField] private bool tpPlayerOnJoin = true;
    [SerializeField] private Transform tpLocation;
    [Header("Consider instance creator as VIP")]
    [SerializeField] private bool EmpowerInstanceCreator = false;
    private bool empoweredUser = false;
    private float delay = 0.6f;
    private bool abort;
    void Start()
    {
        if (playerTag == null) //Checks for valid tag
        {
            abort = true;
            SendCustomEventDelayedSeconds(nameof(_sendDebugError), 1f);
            return;
        }
        VRCPlayerApi localPlayer = Networking.LocalPlayer;
        if (Networking.LocalPlayer.isMaster && EmpowerInstanceCreator) //Empowers instance creator if enabled
        {
            localPlayer.SetPlayerTag("rank", playerTag);
            foreach (GameObject toggleObjectON in toggleObjectsON)
            {
                toggleObjectON.SetActive(true);
            }
            foreach (GameObject toggleObjectOFF in toggleObjectsOFF)
            {
                toggleObjectOFF.SetActive(false);
            }
            if (programsSuccess[0] != null && !programsSuccess[0].Equals(null))
            {
                foreach (UdonBehaviour b in programsSuccess)
                {
                    b.SendCustomEvent(eventName);
                }
            }
            return;
        }
        for (int i = 0; i < userArray.Length; i++) //Checks user array for matches with local user to empower
        {
            if (userArray[i] == localPlayer.displayName || empoweredUser)
            {
                localPlayer.SetPlayerTag("rank", playerTag);
                if (tpPlayerOnJoin)
                {
                    SendCustomEventDelayedSeconds(nameof(_initTeleport), delay);
                }
                foreach (GameObject toggleObjectON in toggleObjectsON)
                {
                    toggleObjectON.SetActive(true);
                }
                foreach (GameObject toggleObjectOFF in toggleObjectsOFF)
                {
                    toggleObjectOFF.SetActive(false);
                }
                if (programsSuccess[0] != null && !programsSuccess[0].Equals(null))
                {
                    foreach (UdonBehaviour b in programsSuccess)
                    {
                        b.SendCustomEvent(eventName);
                    }
                }
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
    public void _addNewUser() //just an alias
    {
        _updateState();
    }

    public void _updateState() //adds a user to VIPs on calling this function
    {
        if (abort) return;
        VRCPlayerApi localPlayer = Networking.LocalPlayer;
        localPlayer.SetPlayerTag("rank", playerTag);
        foreach (GameObject toggleObject in toggleObjectsON)
        {
            toggleObject.SetActive(true);
        }
        foreach (GameObject toggleObject in toggleObjectsOFF)
        {
            toggleObject.SetActive(false);
        }
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

    public void _sendDebugError() => Debug.LogError("Reava_UwUtils:<color=red> <b>Invalid values</b></color>, no User / Tag found. (" + gameObject + ")", gameObject);
}