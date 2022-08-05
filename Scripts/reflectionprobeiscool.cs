using UdonSharp;
using UnityEngine;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class reflectionprobeiscool : UdonSharpBehaviour
{
    #region References

    [Header("References")]

    #endregion
    [Range(0, 30)]
    [SerializeField] private float updateInterval = 1.5f;
    [SerializeField] private ReflectionProbe reflectionProbeSource;
    private bool abort;

    public void Start()
    {
        if (reflectionProbeSource == null)
        {
            abort = true;
            SendCustomEventDelayedSeconds(nameof(Animator), 1f);
            return;
        }
        UpdateReflections();
    }

    public void UpdateReflections()
    {
        if (abort) return;
        reflectionProbeSource.RenderProbe();
        SendCustomEventDelayedSeconds(nameof(UpdateReflections), updateInterval);
    }

    public void _sendDebugError() => Debug.LogError("Reava_UwUtils:<color=red> <b>Invalid reference</b></color>, please review on: " + gameObject, gameObject);
}
