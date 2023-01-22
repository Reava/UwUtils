using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class OnSpawnFade : UdonSharpBehaviour
{
    [Range(0f, 3f)]
    [SerializeField] private float FadeSpeed = 1f;
    [SerializeField] private GameObject AnimatorParent;
    [SerializeField] private bool FadeOnRespawn = true;
    private Animator AnimatorTarget;

    public void Start()
    {
        if (!AnimatorParent) _sendDebugError();
        AnimatorTarget = AnimatorParent.GetComponent<Animator>();
        AnimatorTarget.SetFloat("fadeSpeed", FadeSpeed);
        AnimatorTarget.SetBool("fade", true);
    }

    public void OnPlayerRespawn(VRCPlayerApi player)
    {
        AnimatorTarget.SetFloat("fadeSpeed", FadeSpeed);
        if (FadeOnRespawn) AnimatorTarget.SetBool("fade", true);
    }

    public void _setValueback()
    {
        AnimatorTarget.SetBool("fade", false);
    }
    private void _sendDebugError() => Debug.LogError("Reava_UwUtils:<color=red> <b>Invalid references</b></color>, please review <color=orange>references</color> on: " + gameObject + ".", gameObject);
}