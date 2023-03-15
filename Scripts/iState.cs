﻿using UdonSharp;
using Unity.Collections;
using UnityEngine;
using VRC.SDK3.Components;

[AddComponentMenu("UwUtils/iState")]
[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class iState : UdonSharpBehaviour
{
    [Header("List of objects to invert the state of")]
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

    public void _InvertState()
    {
        foreach (GameObject toggleObject in toggleObjects)
        {
            toggleObject.SetActive(!toggleObject.activeSelf);
        }
    }
}
