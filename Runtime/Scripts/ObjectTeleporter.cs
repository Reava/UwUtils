using UdonSharp;
using UnityEngine;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Object Teleporter")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ObjectTeleporter : UdonSharpBehaviour
    {
        [Header("Settings"), Tooltip("If disabled, this will only teleport one way.")]
        [SerializeField] private bool teleportBackOnSecondInteract = true;
        [Tooltip("If one object but multiple targets, will cycle between them, if one object and one target, will allow teleporting back and forth, if arrays match, will teleport each object to the corresponding target.")]
        [SerializeField] private GameObject[] ObjectsToTeleport;
        [Header("Teleport destinations"), Tooltip("If arrays Length does not match, objects without a target will not get teleported.")]
        [SerializeField] private Transform[] TeleportTargets;
        [Space]
        [SerializeField] private bool enableLogging = true;

        private Transform[] OriginalTransforms;
        private int loopCurrentIndex = 0;
        private int behaviorMode = 0;
        private bool wasTeleported = false;

        void Start() //Check if setup contains at least one valid Object and Target each, otherwise disables self
        {
            if(ObjectsToTeleport.Length == 0 || TeleportTargets.Length == 0)
            {
                if(enableLogging) Debug.LogError("[Reava_/UwUtils/ObjectTeleporter.cs] No objects found to teleport or teleport targets on '" + gameObject.name + "'");
                return;
            }

            int objectCount = ObjectsToTeleport.Length;
            int targetsCount = TeleportTargets.Length;

            // If only one object and target is found, cycle positions between the target and original location.
            if (objectCount == 1 && targetsCount == 1)
            {
                OriginalTransforms = new Transform[1] {ObjectsToTeleport[0].transform };
                behaviorMode = 0;
                if (enableLogging) Debug.Log("[Reava_/UwUtils/ObjectTeleporter.cs]: One object with single targets found, teleport back is: " + teleportBackOnSecondInteract, gameObject);
                return;
            }

            // If only one object but multiple targets are found, cycle positions on every new function call.
            if (objectCount == 1 && targetsCount > 1)
            {
                OriginalTransforms[0] = ObjectsToTeleport[0].transform;
                behaviorMode = 1;
                loopCurrentIndex = 0;
                if (enableLogging) Debug.Log("[Reava_/UwUtils/ObjectTeleporter.cs]: One object with multiple targets found, object tranform will be cycled between transforms in a loop!", gameObject);
                return;
            }

            // If arrays lengths match, teleport bakc and force between matching ones.
            if (objectCount == targetsCount)
            {
                OriginalTransforms = new Transform[ObjectsToTeleport.Length];
                for (int i = 0; i < ObjectsToTeleport.Length; i++)
                {
                    OriginalTransforms[i] = ObjectsToTeleport[i].transform;
                }
                behaviorMode = 2;
                if (enableLogging) Debug.Log("[Reava_/UwUtils/ObjectTeleporter.cs]: Arrays length match, objects will get teleported to corresponding targets!", gameObject);
                return;
            }

            // If array lengths do not match but more than one object, teleport objects with a target and restore their old location, but ignore any targets that do not have an object and vice versa.
            if (objectCount != targetsCount)
            {
                if (objectCount > targetsCount)
                {
                    OriginalTransforms = new Transform[objectCount];
                    for (int i = 0; i < ObjectsToTeleport.Length; i++)
                    {
                        OriginalTransforms[i] = ObjectsToTeleport[i].transform;
                    }
                }
                else
                {
                    OriginalTransforms = new Transform[targetsCount];
                    for (int i = 0; i < ObjectsToTeleport.Length; i++)
                    {
                        OriginalTransforms[i] = ObjectsToTeleport[i].transform;
                    }
                }
                    behaviorMode = 2;
                if (enableLogging) Debug.LogWarning("[Reava_/UwUtils/ObjectTeleporter.cs]: Arrays length do not match, only objects with a target will get teleported! Check: '" + gameObject.name + "' (Did you mean this?)", gameObject);
                return;
            }
        }

        public void _resetLocations()
        {
            for(int i = 0;i < OriginalTransforms.Length; i++) // this accounts for any behavior mode since we only initialize this array with valid entries.
            {
                ObjectsToTeleport[i].transform.position = OriginalTransforms[i].position;
                ObjectsToTeleport[i].transform.rotation = OriginalTransforms[i].rotation;
            }
        }

        public override void Interact()
        {
            switch (behaviorMode)
            {
                case 0:
                    if (!wasTeleported) 
                    {
                        ObjectsToTeleport[0].transform.position = TeleportTargets[0].position;
                        ObjectsToTeleport[0].transform.rotation = TeleportTargets[0].rotation;
                    }
                    else
                    {
                        if (!teleportBackOnSecondInteract) return;
                        ObjectsToTeleport[0].transform.position = OriginalTransforms[0].position;
                        ObjectsToTeleport[0].transform.rotation = OriginalTransforms[0].rotation;
                    }
                    break;
                case 1:
                    ObjectsToTeleport[0].transform.position = TeleportTargets[loopCurrentIndex].position;
                    ObjectsToTeleport[0].transform.rotation = TeleportTargets[loopCurrentIndex].rotation;
                    // Check that we dont exit Target array
                    if (loopCurrentIndex == TeleportTargets.Length)
                    {
                        loopCurrentIndex = 0;
                        return;
                    }
                    else
                    {
                        loopCurrentIndex += 1;
                    }
                    break;
                case 2:
                    if (!wasTeleported)
                    {
                        for (int i = 0; i < ObjectsToTeleport.Length; i++)
                        {
                            ObjectsToTeleport[i].transform.position = TeleportTargets[i].position;
                            ObjectsToTeleport[i].transform.rotation = TeleportTargets[i].rotation;
                        }
                        wasTeleported = true;
                    }
                    else
                    {
                        if (!teleportBackOnSecondInteract) return;
                        for (int i = 0; i < ObjectsToTeleport.Length; i++)
                        {
                            ObjectsToTeleport[i].transform.position = OriginalTransforms[i].position;
                            ObjectsToTeleport[i].transform.rotation = OriginalTransforms[i].rotation;
                        }
                        wasTeleported = false;
                    }
                    break;
            }
        }
    }
}