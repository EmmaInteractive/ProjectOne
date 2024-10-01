using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class ServiceLoader : MonoBehaviour
{
    [SerializeField]
    public static GameObject SignTextPrefab;

    // Start is called before the first frame update
    void Start()
    {
        var list = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(IBaseService).IsAssignableFrom(t) && !t.IsInterface)
            .Select(t => t)
            .ToList();

        foreach(var item in list)
        {
            Activator.CreateInstance(item);
        }
    }
}
