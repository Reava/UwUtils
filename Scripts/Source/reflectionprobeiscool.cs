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
