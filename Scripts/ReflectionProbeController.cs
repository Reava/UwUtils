using UdonSharp;
using UnityEngine;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Reflection Probe Controller")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ReflectionProbeController : UdonSharpBehaviour
    {
        #region References

        [Header("References")]

        #endregion
        [Range(0, 30)]
        public float updateInterval = 1.5f;
        [HideInInspector] public bool updateLoop = true;
        [SerializeField] private ReflectionProbe reflectionProbeSource;

        public void Start()
        {
            if (reflectionProbeSource == null)
            {
                if (gameObject.GetComponent<ReflectionProbe>() != null)
                {
                    reflectionProbeSource = gameObject.GetComponent<ReflectionProbe>();
                }
                else
                {
                    return;
                }
            }
            reflectionProbeSource.mode = UnityEngine.Rendering.ReflectionProbeMode.Realtime;
            reflectionProbeSource.refreshMode = UnityEngine.Rendering.ReflectionProbeRefreshMode.ViaScripting;
            UpdateReflections();
        }

        public void UpdateReflections()
        {
            reflectionProbeSource.RenderProbe();
            if (updateLoop) SendCustomEventDelayedSeconds(nameof(UpdateReflections), updateInterval);
        }

        public void ToggleLoop()
        {
            updateLoop = !updateLoop;
            if (updateLoop) UpdateReflections();
        }
    }
}