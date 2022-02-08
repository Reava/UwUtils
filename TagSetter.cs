using UnityEngine;
using UdonSharp;
using VRC.SDKBase;

public class TagSetter : UdonSharpBehaviour
{
    [Tooltip("Name of the tag")]
    public string playerTag;
    public override void Interact()
    {
        VRCPlayerApi localPlayer = Networking.LocalPlayer;
        localPlayer.SetPlayerTag("rank", playerTag);
    }
}
