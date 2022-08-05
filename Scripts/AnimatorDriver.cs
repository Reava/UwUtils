using UdonSharp;
using UnityEngine;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class AnimatorDriver : UdonSharpBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private string ParameterName;
    [SerializeField] private bool defaultValue;

    public void Start()
    {
        Animator.SetBool(ParameterName, defaultValue);
    }

    public override void Interact()
    {
        Animator.SetBool(ParameterName, !defaultValue);
    }
}