using UdonSharp;
using UnityEngine;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class AnimatorDriver : UdonSharpBehaviour
{
    public Animator Animator;
    public string ParameterName;
    public bool defaultValue;

    public void Start()
    {
        Animator.SetBool(ParameterName, defaultValue);
    }

    public override void Interact()
    {
        Animator.SetBool(ParameterName, !defaultValue);
    }
}