using UnityEngine;

public class CSharpWrapper : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        var addition = new CSharpManagedPlugin.Addition();
        var add = addition.Addify(5, 2);
        Debug.Log(add);
    }
}
