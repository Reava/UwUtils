using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
public class InstanceTimeActions : UdonSharpBehaviour
{
    [Header("References")]
    [Header("Defaults")]
    [SerializeField] private int perActionDelaySeconds = 30;
    private bool abort = false;
    [UdonSynced]
    /*[NonSerialized] */public int InstanceTimeSeconds = 0;
    public float currentTime;

    private void Start()
    {
        if (false)
        {
            abort = true;
            SendCustomEventDelayedSeconds(nameof(_sendDebugError), 1f);
            return;
        }
        currentTime = Time.time;
        /*VRCPlayerApi localPlayer = Networking.LocalPlayer;
        if (localPlayer.isMaster)
        {
            InstanceTimeSeconds = Mathf.RoundToInt(currentTime);
        }*/
        SendCustomEventDelayedSeconds(nameof(_nextState), 1f);
    }

    public void Update()
    {
        currentTime = Time.time;
        InstanceTimeSeconds = Mathf.FloorToInt(currentTime);
    }

    public void _nextState()
    {
        if (abort) return;
        currentTime = Time.time;
        int stage = 0;
        stage = Mathf.FloorToInt(currentTime/1800);
        switch (stage)
        {
            case 0:
                //do smth
                return;
            case 1:
                //do smth
                return;
            case 2:
                //do smth
                return;
            case 3:
                //do smth
                return;
            case 4:
                //do smth
                return;
            case 5:
                //do smth
                return;
            case 6:
                //do smth
                return;
            case 7:
                //do smth
                return;
            case 8:
                //do smth
                return;
            default:
                Debug.Log("Unknown instance time");
                return;
        }
    }

    public void _sendDebugError() => Debug.LogError("Reava_UwUtils:<color=red> No Target script found</color>. (" + gameObject + ")", gameObject);
}