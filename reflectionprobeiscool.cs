using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class reflectionprobeiscool : UdonSharpBehaviour
{
    #region References

    [Header("References")]

    #endregion
    [Range(0, 30)]
    public float updateInterval = 7f;
    public ReflectionProbe reflectionProbeSource;

    public void OnEnable()
    {
        UpdateReflections();
    }

    public void UpdateReflections()
    {
        reflectionProbeSource.RenderProbe();

        SendCustomEventDelayedSeconds(nameof(UpdateReflections), updateInterval);
    }
}
