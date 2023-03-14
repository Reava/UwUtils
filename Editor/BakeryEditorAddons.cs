#if !COMPILER_UDONSHARP && UNITY_EDITOR &&BAKERY_INCLUDED
using UnityEngine;
using UnityEditor;
using System.Globalization;

public class BakeryEditorAddons : Editor
{
    [MenuItem("GameObject/Bakery/Scale Intensity", false, 0)]
    static void SetIntensity(MenuCommand MC)
    {
        // Prevent method from executing more than once due to multiple selected gameobjects in hierarchy (possible unity bug? :c)
        if ((!MC.context?.Equals(Selection.activeObject)) ?? false) return;
        BakeryQuickSetIntensity.Open();
    }
    [MenuItem("GameObject/Bakery/Scale Indirect Intensity", false, 0)]
    static void SetIndirectIntensity(MenuCommand MC)
    {
        // Prevent method from executing more than once due to multiple selected gameobjects in hierarchy (possible unity bug? :c)
        if ((!MC.context?.Equals(Selection.activeObject)) ?? false) return;
        BakeryQuickSetIndirectIntensity.Open();
    }
    [MenuItem("GameObject/Bakery/Scale Range", false, 0)]
    static void SetRange(MenuCommand MC)
    {
        // Prevent method from executing more than once due to multiple selected gameobjects in hierarchy (possible unity bug? :c)
        if ((!MC.context?.Equals(Selection.activeObject)) ?? false) return;
        BakeryQuickSetRange.Open();
    }
    [MenuItem("GameObject/Bakery/Scale Samples", false, 0)]
    static void SetSamples(MenuCommand MC)
    {
        // Prevent method from executing more than once due to multiple selected gameobjects in hierarchy (possible unity bug? :c)
        if ((!MC.context?.Equals(Selection.activeObject)) ?? false) return;
        BakeryQuickSetSamples.Open();
    }
    [MenuItem("GameObject/Bakery/Scale Shadowspread", false, 0)]
    static void SetShadowspread(MenuCommand MC)
    {
        // Prevent method from executing more than once due to multiple selected gameobjects in hierarchy (possible unity bug? :c)
        if ((!MC.context?.Equals(Selection.activeObject)) ?? false) return;
        BakeryQuickSetShadowspread.Open();
    }

}

public class BakeryQuickSetIntensity : EditorWindow
{
    public static void Open()
    {
        BakeryQuickSetIntensity Instance = CreateInstance<BakeryQuickSetIntensity>();
        Instance.ShowModal();
    }
    [SerializeField] public static string value;
    public void OnGUI()
    {
        GUILayout.Space(10);
        value = EditorGUILayout.TextField("Operation :",value);
        GUILayout.Space(10);
        if (GUILayout.Button("Set"))
        {
            value.Replace(" ", "");
            char DetectedOperand = value[0];
            value = value.Remove(0,1);
            Debug.Log(value);
            GameObject[] Parents = Selection.gameObjects;
            switch (DetectedOperand) 
            {
                case '+':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].intensity += float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Addition");
                    }
                    break;
                case '-':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].intensity -= float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Subtraction");
                    }
                    break;
                case '*':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].intensity *= float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Multiply");
                    }
                    break;
                case '/':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].intensity /= float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Division");
                    }
                    break;
                case '=':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].intensity = float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Division");
                    }
                    break;
            }
            value = null;
            Close();
        }
    }
}

public class BakeryQuickSetIndirectIntensity : EditorWindow
{
    public static void Open()
    {
        BakeryQuickSetIndirectIntensity Instance = CreateInstance<BakeryQuickSetIndirectIntensity>();
        Instance.ShowModal();
    }
    [SerializeField] public static string value;
    public void OnGUI()
    {
        GUILayout.Space(10);
        value = EditorGUILayout.TextField("Operation :", value);
        GUILayout.Space(10);
        if (GUILayout.Button("Set"))
        {
            value.Replace(" ", "");
            char DetectedOperand = value[0];
            value = value.Remove(0, 1);
            Debug.Log(value);
            GameObject[] Parents = Selection.gameObjects;
            switch (DetectedOperand)
            {
                case '+':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].indirectIntensity += float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Addition");
                    }
                    break;
                case '-':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].indirectIntensity -= float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Subtraction");
                    }
                    break;
                case '*':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].indirectIntensity *= float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Multiply");
                    }
                    break;
                case '/':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].indirectIntensity /= float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Division");
                    }
                    break;
                case '=':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].indirectIntensity = float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Division");
                    }
                    break;
            }
            value = null;
            Close();
        }
    }
}
public class BakeryQuickSetRange : EditorWindow
{
    public static void Open()
    {
        BakeryQuickSetRange Instance = CreateInstance<BakeryQuickSetRange>();
        Instance.ShowModal();
    }
    [SerializeField] public static string value;
    public void OnGUI()
    {
        GUILayout.Space(10);
        value = EditorGUILayout.TextField("Operation :", value);
        GUILayout.Space(10);
        if (GUILayout.Button("Set"))
        {
            value.Replace(" ", "");
            char DetectedOperand = value[0];
            value = value.Remove(0, 1);
            Debug.Log(value);
            GameObject[] Parents = Selection.gameObjects;
            switch (DetectedOperand)
            {
                case '+':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].cutoff += float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Addition");
                    }
                    break;
                case '-':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].cutoff -= float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Subtraction");
                    }
                    break;
                case '*':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].cutoff *= float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Multiply");
                    }
                    break;
                case '/':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].cutoff /= float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Division");
                    }
                    break;
                case '=':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].cutoff = float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Division");
                    }
                    break;
            }
            value = null;
            Close();
        }
    }
}

public class BakeryQuickSetSamples : EditorWindow
{
    public static void Open()
    {
        BakeryQuickSetSamples Instance = CreateInstance<BakeryQuickSetSamples>();
        Instance.ShowModal();
    }
    [SerializeField] public static string value;
    public void OnGUI()
    {
        GUILayout.Space(10);
        value = EditorGUILayout.TextField("Operation :", value);
        GUILayout.Space(10);
        if (GUILayout.Button("Set"))
        {
            value.Replace(" ", "");
            char DetectedOperand = value[0];
            value = value.Remove(0, 1);
            Debug.Log(value);
            GameObject[] Parents = Selection.gameObjects;
            switch (DetectedOperand)
            {
                case '+':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].samples += int.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Addition");
                    }
                    break;
                case '-':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].samples -= int.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Subtraction");
                    }
                    break;
                case '*':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].samples *= int.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Multiply");
                    }
                    break;
                case '/':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].samples /= int.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Division");
                    }
                    break;
                case '=':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].samples = int.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Division");
                    }
                    break;
            }
            value = null;
            Close();
        }
    }
}

public class BakeryQuickSetShadowspread : EditorWindow
{
    public static void Open()
    {
        BakeryQuickSetShadowspread Instance = CreateInstance<BakeryQuickSetShadowspread>();
        Instance.ShowModal();
    }
    [SerializeField] public static string value;
    public void OnGUI()
    {
        GUILayout.Space(10);
        value = EditorGUILayout.TextField("Operation :", value);
        GUILayout.Space(10);
        if (GUILayout.Button("Set"))
        {
            value.Replace(" ", "");
            char DetectedOperand = value[0];
            value = value.Remove(0, 1);
            Debug.Log(value);
            GameObject[] Parents = Selection.gameObjects;
            switch (DetectedOperand)
            {
                case '+':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].shadowSpread += float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Addition");
                    }
                    break;
                case '-':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].shadowSpread -= float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Subtraction");
                    }
                    break;
                case '*':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].shadowSpread *= float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Multiply");
                    }
                    break;
                case '/':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].shadowSpread /= float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Division");
                    }
                    break;
                case '=':
                    {
                        foreach (GameObject Parent in Parents)
                        {
                            BakeryPointLight[] ChildernPointLights = Parent.GetComponentsInChildren<BakeryPointLight>();
                            for (int i = 0; i < ChildernPointLights.Length; i++)
                            {
                                ChildernPointLights[i].shadowSpread = float.Parse(value, CultureInfo.InvariantCulture);
                            }
                        }
                        Debug.Log("Division");
                    }
                    break;
            }
            value = null;
            Close();
        }
    }
}
#endif