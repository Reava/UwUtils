using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Sequencial Toggle")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SequencialToggle : UdonSharpBehaviour
    {
        [Space]
        [Header("Sequencial Toggle, Interact for next, _Toggle to toggle entirely")]
        [SerializeField] GameObject[] Targets;
        [Space]
        [SerializeField] bool enableLogging = true;

        private int arrayLength = 0;
        private int currentToggle = 0;
        private bool systemState = true;

        void Start()
        {
            if(Targets == null)
            {
                if (enableLogging) Debug.Log("[Reava_/UwUtils/SequentialToggle.cs]: No target objects found, disabling behaviour on " + gameObject.name + "", gameObject);
                return;
            }
            foreach (GameObject t in Targets)
            {
                if (t == null) continue;
                t.SetActive(false);
            }
            Targets[0].SetActive(true);
            arrayLength = Targets.Length + 1;
        }

        public override void Interact()
        {
            currentToggle += 1;
            for (int i = 0; i < Targets.Length; i++)
            {
                if (i == currentToggle) Targets[i].SetActive(true);
                Targets[i].SetActive(false);
            }
        }

        public void _Toggle()
        {
            systemState = !systemState;
            foreach (GameObject t in Targets)
            {
                if (t == null) continue;
                t.SetActive(systemState);
            }
        }
    }
}