using UdonSharp;
using UnityEngine;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Object Teleporter")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ObjectTeleporter : UdonSharpBehaviour
    {
        //[Tooltip("Using local trasnform will save and apply transforms related to its parent. Only use if you know what you're doing.")]
        //[SerializeField] private bool useLocalSpace = false;
        [Header("Settings"), Tooltip("If disabled, this will only teleport one way.")]
        [SerializeField] private bool teleportBackOnSecondInteract = true;
        [Tooltip("If enabled, the object will get its *local* scaled copied from the target it gets teleported to.")]
        [SerializeField] private bool matchLocalScaleOnTeleport = false;
        [Tooltip("If one object but multiple targets, will cycle between them, if one object and one target, will allow teleporting back and forth, if arrays match, will teleport each object to the corresponding target.")]
        [SerializeField] private GameObject[] ObjectsToTeleport;
        [Header("Teleport destinations"), Tooltip("If arrays Length does not match, objects without a target will not get teleported.")]
        [SerializeField] private Transform[] TeleportTargets;
        [Space]
        [SerializeField] private bool enableLogging = true;

        private Vector3[] savedPositions;
        private Vector3[] savedScales;
        private Quaternion[] savedRotations;
        private int Index = 0;
        private int behaviorMode = 0;
        private bool wasTeleported = false;

        void Start()
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
                savedPositions = new Vector3[1] { ObjectsToTeleport[0].transform.position };
                savedRotations = new Quaternion[1] { ObjectsToTeleport[0].transform.rotation };
                savedScales = new Vector3[1] { ObjectsToTeleport[0].transform.localScale };
                behaviorMode = 0;
                if (enableLogging) Debug.Log("[Reava_/UwUtils/ObjectTeleporter.cs]: One object with single targets found, teleport back is: " + teleportBackOnSecondInteract, gameObject);
                return;
            }

            // If only one object but multiple targets are found, cycle positions on every new function call.
            if (objectCount == 1 && targetsCount > 1)
            {
                savedPositions = new Vector3[1] { ObjectsToTeleport[0].transform.position };
                savedRotations = new Quaternion[1] { ObjectsToTeleport[0].transform.rotation };
                savedScales = new Vector3[1] { ObjectsToTeleport[0].transform.localScale };
                behaviorMode = 1;
                Index = 0;
                if (enableLogging) Debug.Log("[Reava_/UwUtils/ObjectTeleporter.cs]: One object with multiple targets found, object tranform will be cycled between transforms in a loop!", gameObject);
                return;
            }

            // If arrays lengths match, teleport back and force between matching ones.
            if (objectCount == targetsCount)
            {
                savedPositions = new Vector3[objectCount];
                savedRotations = new Quaternion[objectCount];
                savedScales = new Vector3[objectCount];
                for (int i = 0; i < ObjectsToTeleport.Length; i++)
                {
                    savedPositions[i] = ObjectsToTeleport[i].transform.position;
                    savedRotations[i] = ObjectsToTeleport[i].transform.rotation;
                    savedScales[i] = ObjectsToTeleport[i].transform.localScale;
                }
                behaviorMode = 2;
                if (enableLogging) Debug.Log("[Reava_/UwUtils/ObjectTeleporter.cs]: Arrays length match, objects will get teleported to corresponding targets!", gameObject);
                return;
            }

            // If array lengths do not match but more than one object, teleport objects with a target and restore their old location, but ignore any targets that do not have an object and vice versa.
            if (objectCount != targetsCount)
            {
                if (objectCount < targetsCount)
                {
                    savedPositions = new Vector3[objectCount];
                    savedRotations = new Quaternion[objectCount];
                    for (int i = 0; i < ObjectsToTeleport.Length; i++)
                    {
                        savedPositions[i] = ObjectsToTeleport[i].transform.position;
                        savedRotations[i] = ObjectsToTeleport[i].transform.rotation;
                        savedScales[i] = ObjectsToTeleport[i].transform.localScale;
                    }
                }
                else
                {
                    savedPositions = new Vector3[targetsCount];
                    savedRotations = new Quaternion[targetsCount];
                    for (int i = 0; i < TeleportTargets.Length; i++)
                    {
                        savedPositions[i] = ObjectsToTeleport[i].transform.position;
                        savedRotations[i] = ObjectsToTeleport[i].transform.rotation;
                        savedScales[i] = ObjectsToTeleport[i].transform.localScale;
                    }
                }
                behaviorMode = 2;
                if (enableLogging) Debug.LogWarning("[Reava_/UwUtils/ObjectTeleporter.cs]: Arrays length do not match, only objects with a target will get teleported! Check: '" + gameObject.name + "' (Did you mean this?)", gameObject);
                return;
            }
        }

        public void _resetLocations()
        {
            for(int i = 0;i < savedPositions.Length; i++) // this accounts for any behavior mode since we only initialize this array with valid entries.
            {
                ObjectsToTeleport[i].transform.SetPositionAndRotation(savedPositions[i], savedRotations[i]);
                ObjectsToTeleport[i].transform.localScale = savedScales[i];
            }
        }

        public override void Interact()
        {
            switch (behaviorMode)
            {
                case 0:
                    if (!wasTeleported) 
                    {
                        ObjectsToTeleport[0].transform.SetPositionAndRotation(TeleportTargets[0].position, TeleportTargets[0].rotation);
                        if(matchLocalScaleOnTeleport) ObjectsToTeleport[0].transform.localScale = savedScales[0];
                        wasTeleported = true;
                    }
                    else
                    {
                        if (!teleportBackOnSecondInteract) return;
                        ObjectsToTeleport[0].transform.SetPositionAndRotation(savedPositions[0], savedRotations[0]);
                        if (matchLocalScaleOnTeleport) ObjectsToTeleport[0].transform.localScale = savedScales[0];
                        wasTeleported = false;
                    }
                    break;
                case 1:
                    if (Index + 1 == TeleportTargets.Length + 1)
                    {
                        ObjectsToTeleport[0].transform.SetPositionAndRotation(savedPositions[0], savedRotations[0]);
                        if (matchLocalScaleOnTeleport) ObjectsToTeleport[0].transform.localScale = savedScales[0];
                        Index = 0;
                        break;
                    }
                    else
                    {
                        ObjectsToTeleport[0].transform.SetPositionAndRotation(TeleportTargets[Index].transform.position, TeleportTargets[Index].transform.rotation);
                        if (matchLocalScaleOnTeleport) ObjectsToTeleport[0].transform.localScale = savedScales[Index];
                    }
                    Index += 1;
                    break;
                case 2:
                    if (!wasTeleported)
                    {
                        for (int i = 0; i < savedPositions.Length; i++)
                        {
                            ObjectsToTeleport[i].transform.SetPositionAndRotation(TeleportTargets[i].position, TeleportTargets[i].rotation);
                            if (matchLocalScaleOnTeleport) ObjectsToTeleport[i].transform.localScale = savedScales[i];
                        }
                        wasTeleported = true;
                    }
                    else
                    {
                        if (!teleportBackOnSecondInteract) return;
                        for (int i = 0; i < savedPositions.Length; i++)
                        {
                            ObjectsToTeleport[i].transform.SetPositionAndRotation(savedPositions[i], savedRotations[i]);
                            if (matchLocalScaleOnTeleport) ObjectsToTeleport[i].transform.localScale = savedScales[i];
                        }
                        wasTeleported = false;
                    }
                    break;
            }
        }
    }
}