
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using AudioLink;

namespace UwUtils
{
    public class AudiolinkedAnimator : UdonSharpBehaviour
    {
        [Header("Make sure readback is enabled!")]
        [SerializeField] private AudioLink.AudioLink audioLink;
        [Range(0, 3)]
        [SerializeField] private int band;
        [Range(0, 127)]
        [SerializeField] private int delay;
        [SerializeField] private bool loop;
        [Range((float)0.0, (float)5.0)]
        [SerializeField] private float pollRate;
        [SerializeField] private string parameterName;
        [SerializeField] private Animator controlledAnimator;
        [Space]
        [Tooltip("Support will not be given if logging is not enabled.")]
        [SerializeField] private bool enableLogging = true;
        private bool valid = true;

        void Start()
        {
            if (!audioLink || !controlledAnimator)
            {
                Debug.LogError("[Reava_/UwUtils/AudiolinkedAnimator.cs] Setup is invalid, check your references for object '" + gameObject.name + "'");
                valid = false;
                return;
            }

            _pullData();
        }

        private void _pullData()
        {
            if (!valid) return;
            if (audioLink.AudioDataIsAvailable())
            {
                float gimme = Vector3.Dot(audioLink.GetDataAtPixel(delay, band), new Vector3(0.299f, 0.587f, 0.114f));
                controlledAnimator.SetFloat(parameterName, gimme);
                if (loop) SendCustomEventDelayedSeconds(nameof(_pullData), pollRate);
            }
            else
            {
                Debug.LogError("[Reava_/UwUtils/AudiolinkedAnimator.cs] Audiodata not found, aborting. '" + gameObject.name + "'");
            }
        }
    }
}