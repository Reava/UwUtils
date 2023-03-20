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
        [Tooltip("Default object to have on, first in array is 0, -1 to disable all on start")]
        [SerializeField] private int defaultSelection = 0;
        [SerializeField] GameObject[] Targets;
        [Tooltip("After cycling through the array, has a time with all objects off")]
        [SerializeField] private bool OffAfterEachCycle = true;
        [Space]
        [SerializeField] bool enableLogging = true;

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
            if(defaultSelection > -1)
            {
                Targets[defaultSelection].SetActive(true);
                currentToggle = 1;
            }
            else
            {
                currentToggle = 0;
            }
        }

        public override void Interact()
        {
            if (!systemState) return;
            if (currentToggle >= Targets.Length && OffAfterEachCycle)
            {
                foreach (GameObject t in Targets)
                {
                    if (t == null) continue;
                    t.SetActive(false);
                }
                currentToggle = 0;
                return;
            }
            if(currentToggle >= Targets.Length) {
                currentToggle = 0;
            }
            foreach(GameObject t in Targets)
            {
                if (t == null) continue;
                if (systemState && t == Targets[currentToggle])
                {
                    t.SetActive(true);
                }
                else
                {
                    t.SetActive(false);
                }
            }
            currentToggle += 1;
        }

        public void _Toggle()
        {
            systemState = !systemState;
            foreach (GameObject t in Targets)
            {
                if (t == null) continue;
                if (systemState && t == Targets[currentToggle-1])
                {
                    t.SetActive(true);
                }
                else
                {
                    t.SetActive(false);
                }
            }
        }

        public void _setCurrentTarget(int Target)
        {
            foreach (GameObject t in Targets)
            {
                if (t == null) continue;
                if (systemState && t == Targets[Target])
                {
                    t.SetActive(true);
                }
                else
                {
                    t.SetActive(false);
                }
            }
        }
    }
}