using UdonSharp;
using UnityEngine;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Spinny")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Spinny : UdonSharpBehaviour
    {
        #region References
        [Header("Objects to rotate")]

        [Header("Settings")]

        #endregion
        [Range(0.1f, 60f)]
        [SerializeField] private float rotation_Speed = 1.5f;
        [Range(1f, 2000f)]
        [SerializeField] private float update_Interval_Milliseconds = 1f;

        [Header("Target object rotation axis:")]
        [Header("[0] Y   [1] X   [2] Z")]
        [SerializeField] [Range(0, 2)] private int targetRotation;

        public void OnEnable()
        {
            UpdateRotation();
        }

        public void UpdateRotation()
        {
            if (targetRotation == 0)
            {
                transform.Rotate(Vector3.up, rotation_Speed);
            }
            else if (targetRotation == 1)
            {
                transform.Rotate(Vector3.right, rotation_Speed);
            }
            else if (targetRotation == 2)
            {
                transform.Rotate(Vector3.forward, rotation_Speed);
            }
            else
            {
                Debug.Log("Error: Function invalid");
            }

            SendCustomEventDelayedSeconds(nameof(UpdateRotation), update_Interval_Milliseconds / 100);
        }
    }
}