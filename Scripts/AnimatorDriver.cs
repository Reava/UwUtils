using UdonSharp;
using UnityEngine;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class AnimatorDriver : UdonSharpBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private string ParameterName;
    [SerializeField] private bool defaultValue;
    private bool abort;

    public void Start()
    {
        if (Animator == null | ParameterName == null)
        {
            abort = true;
            SendCustomEventDelayedSeconds(nameof(Animator), 1f);
            return;
        }
        Animator.SetBool(ParameterName, defaultValue);
    }

    public override void Interact()
    {
        if (abort) return;
        Animator.SetBool(ParameterName, !defaultValue);
    }

    public void _sendDebugError() => Debug.LogError("Reava_UwUtils:<color=red> <b>Invalid references</b></color>, please review <color=orange>Animator reference</color> / <color=orange>Parameter name</color>. (" + gameObject + ")", gameObject);
}