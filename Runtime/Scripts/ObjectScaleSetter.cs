using UdonSharp;
using UnityEngine;
using VRC.Udon;

namespace UwUtils
{
    public class ObjectScaleSetter : UdonSharpBehaviour
    {
        [Header("Target to scale")]
        public Transform[] referenceTransforms;

        [Header("Scale to apply (MUST MATCH TRANSFORM ARRAY LENGTH)")]
        public Vector3[] targetScales;

        public float delay = 1f;
        public bool applyOnStart = true;

        void Start()
        {
            if (applyOnStart) SendCustomEventDelayedSeconds(nameof(_ApplyScale), delay);
        }

        public void _ApplyScale()
        {
            if (referenceTransforms.Length == 0)
            {
                Debug.LogWarning("[Reava_/UwUtils/ObjectScaleSetter.cs]: Reference Transform is not assigned.");
                return;
            }

            int max = Mathf.Min(referenceTransforms.Length, targetScales.Length);
            for (int i = 0; i < max; i++)
            {
                referenceTransforms[i].localScale = targetScales[i];
            }
        }
        private void OnDisable()
        {
            _ApplyScale();
        }

        private void OnEnable()
        {
            _ApplyScale();
        }
    }
}