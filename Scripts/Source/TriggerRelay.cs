using UnityEngine;
using VRC.SDKBase;
using UdonSharp;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class TriggerRelay : UdonSharpBehaviour
{
    [Header("Make your colliders Trigger & on the layer IgnoreRaycast")]
    [Header("Make sure your colliders are on the same gameObject as this script")]
    //[SerializeField] private Collider[] TriggerColliders;
    [Header("Event settings")]
    [SerializeField] private bool onEnter = false;
    [SerializeField] private bool onExit = false;
    [SerializeField] private UdonBehaviour eventTarget;
    [SerializeField] private string eventName = "_interact";
    private bool valid = false;

    void Start()
    {
        /*foreach(Collider col in TriggerColliders)
        {
            if(col != null)
            {
                valid = true;
                continue;
            }
            else
            {
                Debug.Log("[UwUtils/TriggerRelay.cs] Collider array is invalid for object '" + gameObject.name + "'");
                break;
            }
        }*/
        if(/*valid = true && */eventTarget != null)
        {
            valid = true;
        }
        else
        {
            valid = false;
            Debug.Log("[UwUtils/TriggerRelay.cs] Setup is invalid, check your references for object '" + gameObject.name + "'");
        }
    }

    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        if (valid)
        {
            eventTarget.SendCustomEvent("_interact");
        }
    }
    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (valid && onEnter)
        {
            eventTarget.SendCustomEvent("_interact");
        }
    }
}