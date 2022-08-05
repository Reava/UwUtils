using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class PlayercountToAnimator : UdonSharpBehaviour
{
    [Header("Float min/max values")]
    [Range(0.01f, 20f)]
    [SerializeField] private float DefaultValue = 1.0f;
    [Range(0.1f, 100f)]
    [SerializeField] private float MaxValue = 4.0f;
    [Range(1, 80)]
    [SerializeField] private int playersCountCap = 80;
    [Header("Parameter name to drive")]
    [SerializeField] private string ParameterName = "speed";
    [Header("Enable to convert to Int parameter (Will round up values)")]
    [SerializeField] private bool typeIsInteger = false;
    [SerializeField] private Animator[] AnimatorsToDrive;
    private float currentValue = 1f;
    private int playerCount = 0;
    private bool cooldown = false;
    private bool abort = false;
    //    [Header("Scale float with Curve")]
    //    [SerializeField] private bool useCurve = false;
    //    [SerializeField] private AnimationCurve ScalePerPlayer = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 0.4f));

    public void OnEnable()
    {
        if (AnimatorsToDrive.Length < 1 | ParameterName == null)
        {
            abort = true;
            SendCustomEventDelayedSeconds(nameof(_sendDebugError), 1f);
            return;
        }
        currentValue = DefaultValue;
        if (!typeIsInteger)
        {
            SendCustomEventDelayedSeconds(nameof(_UpdateFloat), 1.5f);

        }
        else
        {
            SendCustomEventDelayedSeconds(nameof(_UpdateInt), 1.5f);
        }
    }

    public void _UpdateFloat()
    {
        if (abort) return;
        playerCount = VRCPlayerApi.GetPlayerCount();
        if (playerCount > playersCountCap)
        {
            playerCount = playersCountCap;
        }
        currentValue = Mathf.Lerp(DefaultValue, MaxValue, playerCount / (float)playersCountCap);
        float v = (float)currentValue;
        foreach (Animator Animatori in AnimatorsToDrive)
        {
            Animatori.SetFloat(ParameterName, v);
        }
    }

    public void _UpdateInt()
    {
        if (abort) return;
        playerCount = VRCPlayerApi.GetPlayerCount();
        currentValue = Mathf.Lerp(DefaultValue, MaxValue, playerCount / (float)playersCountCap);
        int v = Mathf.RoundToInt(currentValue);
        foreach (Animator Animatori in AnimatorsToDrive)
        {
            Animatori.SetInteger(ParameterName, v);
        }
    }

    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        if (abort | cooldown) return;
        cooldown = true;
        SendCustomEventDelayedSeconds(nameof(_CooldownRemover), 1);
        if (!typeIsInteger)
        {
            SendCustomEventDelayedSeconds(nameof(_UpdateFloat), 0.6f);

        }
        else
        {
            SendCustomEventDelayedSeconds(nameof(_UpdateInt), 0.6f);
        }
    }

    public override void OnPlayerLeft(VRCPlayerApi player)
    {
        if (abort | cooldown) return;
        cooldown = true;
        SendCustomEventDelayedSeconds(nameof(_CooldownRemover), 1);
        if (!typeIsInteger)
        {
            SendCustomEventDelayedSeconds(nameof(_UpdateFloat), 0.6f);

        }
        else
        {
            SendCustomEventDelayedSeconds(nameof(_UpdateInt), 0.6f);
        }
    }

    public void _CooldownRemover() => cooldown = false;
    public void _sendDebugError() => Debug.LogError("Reava_UwUtils:<color=red> <b>Invalid references</b></color>, please review <color=orange>Animator references</color> / <color=orange>Parameter name</color>. (" + gameObject + ")", gameObject);
}