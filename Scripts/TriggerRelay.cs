using UnityEngine;
using VRC.SDKBase;
using UdonSharp;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class TriggerRelay : UdonSharpBehaviour
{
    [Header("Make your colliders Trigger & on the layer IgnoreRaycast")]
    [Header("Make sure your colliders are on the same gameObject as this script")]
    [SerializeField] private string eventNameOnExit = "_interact";
    [SerializeField] private string eventNameOnEnter = "_interact";
    //[SerializeField] private Collider[] TriggerColliders;
    [Header("Event settings")]
    [SerializeField] private bool onEnter = true;
    [SerializeField] private bool onExit = false;
    [SerializeField] private UdonBehaviour[] eventTargets;
    private bool valid = false;

    void Start()
    {
        if(eventTargets != null)
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
            foreach(UdonBehaviour program in eventTargets)
            {
                program.SendCustomEvent(eventNameOnExit);
            }
        }
    }
    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (valid && onEnter)
        {
            foreach (UdonBehaviour program in eventTargets)
            {
                program.SendCustomEvent(eventNameOnEnter);
            }
        }
    }
}