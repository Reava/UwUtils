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
        [SerializeField] private GameObject[] ObjectsToTeleport;
        [Header("Teleport destinations"), Tooltip("If arrays Length does not match, objects without a target will not get teleported.")]
        [SerializeField] private Transform[] TeleportTargets;
        private Transform OriginalTransform = null;

        void Start() //Check if setup contains at least one valid Object and Target each, otherwise disables self
        {
            if(ObjectsToTeleport.Length == 0 || TeleportTargets.Length == 0)
            {
                Debug.LogError("[UwUtils/ObjectTeleporter.cs] No objects found to teleport or teleport targets on '" + gameObject.name + "'");
                return;
            }
            if(ObjectsToTeleport.Length == 1) OriginalTransform = ObjectsToTeleport[0].transform;
        }

        public override void Interact()
        {
            _TeleportToTargets();
        }

        public void _TeleportToTargets()
        {
            for(int i = 0; i < ObjectsToTeleport.Length; i++)
            {
                if (!TeleportTargets[i]) return;
                ObjectsToTeleport[i].transform.position = TeleportTargets[i].position;
                ObjectsToTeleport[i].transform.rotation = TeleportTargets[i].rotation;
            }
        }

        public void _TeleportBack()
        {
            if(ObjectsToTeleport.Length > 1) return;
            ObjectsToTeleport[0].transform.position = OriginalTransform.position;
            ObjectsToTeleport[0].transform.rotation = OriginalTransform.rotation;
        }
    }
}