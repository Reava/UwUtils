using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class JoinBell : UdonSharpBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip JoinSound;
    [SerializeField] private AudioClip LeaveSound;
    [Header("Defaults")]
    [SerializeField] private bool JoinEnable = true;
    private int playerCount;
    private int JoinsCount;
    private bool abort = false;

    private void Start()
    {
        if (JoinSound == null | AudioSource == null| LeaveSound == null)
        {
            abort = true;
            SendCustomEventDelayedSeconds(nameof(_sendDebugError), 1f);
            return;
        }
        playerCount = VRCPlayerApi.GetPlayerCount();
    }
    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        if (abort) return;
        JoinsCount = JoinsCount + 1;
        if (JoinSound != null && JoinEnable && JoinsCount > playerCount)
        {
            AudioSource.clip = JoinSound;
            AudioSource.Play();
        }
    }
    public override void OnPlayerLeft(VRCPlayerApi player)
    {
        if (abort) return;
        if (LeaveSound != null && JoinEnable)
        {
            AudioSource.clip = LeaveSound;
            AudioSource.Play();
        }
    }

    public void _JoinToggle()
    {
        JoinEnable = !JoinEnable;
    }

    public void _sendDebugError() => Debug.LogError("Reava_UwUtils:<color=red> No Target script found</color>. (" + gameObject + ")", gameObject);
}