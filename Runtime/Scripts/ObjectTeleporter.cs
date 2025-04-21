using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Object Teleporter")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ObjectTeleporter : UdonSharpBehaviour
    {
        [SerializeField] private GameObject ObjectToTeleport;
        [SerializeField] private Transform TeleportTarget;
        private Transform OriginalTransform = null;
        private bool teleported = false;
        void Start() //Check if setup contains at least one valid Object and Target each, otherwise disables self
        {
            if(!ObjectToTeleport || !TeleportTarget)
            {
                Debug.LogError("[UwUtils/ObjectTeleporter.cs] No objects found to teleport or teleport target, disabling script '" + gameObject.name + "'");
                gameObject.SetActive(false);
            }
            else
            {
                OriginalTransform = ObjectToTeleport.transform;
                return;
            }
        }

        public override void Interact()
        {
            if (!teleported)
            {
                ObjectToTeleport.transform.position = TeleportTarget.position;
                ObjectToTeleport.transform.rotation = TeleportTarget.rotation;
            }
            else
            {
                ObjectToTeleport.transform.position = OriginalTransform.position;
                ObjectToTeleport.transform.rotation = OriginalTransform.rotation;
            }
        }

        public void TeleportToTarget()
        {
            ObjectToTeleport.transform.position = TeleportTarget.position;
            ObjectToTeleport.transform.rotation = TeleportTarget.rotation;
        }

        public void TeleportBack()
        {
            ObjectToTeleport.transform.position = OriginalTransform.position;
            ObjectToTeleport.transform.rotation = OriginalTransform.rotation;
        }
    }
}