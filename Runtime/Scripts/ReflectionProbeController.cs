using UdonSharp;
using UnityEngine;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Reflection Probe Controller")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ReflectionProbeController : UdonSharpBehaviour
    {
        public bool updateProbeOnStart = true;

        #region References

        [Header("References")]

        #endregion
        [Range(0, 30)]
        public float updateInterval = 1.5f;
        public bool updateLoop = true;
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
            if(updateProbeOnStart) _UpdateReflections();
        }

        public void _UpdateReflections()
        {
            reflectionProbeSource.RenderProbe();
            if (updateLoop) SendCustomEventDelayedSeconds(nameof(_UpdateReflections), updateInterval);
        }

        public void _ToggleLoop()
        {
            updateLoop = !updateLoop;
            if (updateLoop) _UpdateReflections();
        }

        public override void Interact()
        {
            _UpdateReflections();
        }
    }
}