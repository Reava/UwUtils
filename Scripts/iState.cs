using UdonSharp;
using Unity.Collections;
using UnityEngine;
using VRC.SDK3.Components;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class iState : UdonSharpBehaviour
{
    [Header("List of objects")]
    public GameObject[] toggleObjects;

    public override void Interact()
    {
        foreach (GameObject toggleObject in toggleObjects)
        {
            toggleObject.SetActive(!toggleObject.activeSelf);
        }
    }

    public void _Enable()
    {
        foreach (GameObject toggleObject in toggleObjects)
        {
            toggleObject.SetActive(true);
        }
    }

    public void _Disable()
    {
        foreach (GameObject toggleObject in toggleObjects)
        {
            toggleObject.SetActive(false);
        }
    }
}
