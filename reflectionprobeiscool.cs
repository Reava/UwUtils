using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class reflectionprobeiscool : UdonSharpBehaviour
{
public ReflectionProbe ReflectionProbeSource;
    private int framedelay = 5;
	private float currentDelay;

	void Update()
	{
	if (framedelay >= 5)
	{
	framedelay = 0;
	ReflectionProbeSource.RenderProbe();
	
	}
	else
	{
	framedelay++;
}
}
}
