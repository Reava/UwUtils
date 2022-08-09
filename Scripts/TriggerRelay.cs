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
    [SerializeField] private UdonBehaviour[] eventTarget;
    [SerializeField] private string eventName = "_interact";
    private bool valid = false;

    void Start()
    {
        if(eventTarget != null)
        {
            valid = true;
        }
        else
        {
            valid = false;
            Debug.LogError("[UwUtils/TriggerRelay.cs] Setup is invalid, check your references for object '" + gameObject.name + "'");
        }
    }

    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        if (valid && onExit)
        {
            foreach(UdonBehaviour program in eventTarget)
            {
                program.SendCustomEvent(eventName);
            }
        }
    }
    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (valid && onEnter)
        {
            foreach (UdonBehaviour program in eventTarget)
            {
                program.SendCustomEvent(eventName);
            }
        }
    }
}