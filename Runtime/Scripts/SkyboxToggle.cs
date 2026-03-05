using UdonSharp;
using UnityEngine;

[AddComponentMenu("UwUtils/Skybox Toggle")]
[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class SkyboxToggle : UdonSharpBehaviour
{
    [Header("Skybox Materials")]
    public Material SkyboxDefault;
    public Material SkyboxToggled;
    private bool _isActive;
    private bool _isAllowed = true;
    [HideInInspector] public int selectionId = 0; // For ModernUI Usage

    private void Start()
    {
        if (SkyboxDefault == null) SkyboxDefault = RenderSettings.skybox;
    }

    public override void Interact()
    {
        ToggleSkybox();
    }

    private void OnDisable()
    {
        _isAllowed = false;
        RenderSettings.skybox = SkyboxDefault;
    }

    private void OnEnable()
    {
        _isAllowed = true;
    }

    public void _SelectionChanged()
    {
        if (!_isAllowed) return;
        _isActive = selectionId == 1;
        if (SkyboxToggled == null)
        {
            RenderSettings.skybox = SkyboxDefault;
        }
        else
        {
            RenderSettings.skybox = _isActive ? SkyboxToggled : SkyboxDefault;
        }
        DynamicGI.UpdateEnvironment();
    }

    public void ToggleSkybox()
    {
        if (!_isAllowed) return;
        _isActive = !_isActive;
        RenderSettings.skybox = _isActive ? SkyboxToggled : SkyboxDefault;
        DynamicGI.UpdateEnvironment();
    }
}