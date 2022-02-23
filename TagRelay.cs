using UdonSharp;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
public class TagRelay : UdonSharpBehaviour
{
    public UdonBehaviour programGranted;
    public string tagAuthorized = "Visitor";
    public override void Interact()
    {
        if (Networking.LocalPlayer != null && Networking.LocalPlayer.GetPlayerTag("rank") != null)
        {
            if (Networking.LocalPlayer.GetPlayerTag("rank") == tagAuthorized)
            {
                programGranted.SendCustomEvent("_updateState");
            }
        }
    }
}
