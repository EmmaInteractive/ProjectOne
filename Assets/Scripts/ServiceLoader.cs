using System;
using System.Collections.Generic;
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

        List<IBaseService> services = new List<IBaseService>();
        foreach(var item in list)
        {
            services.Add(Activator.CreateInstance(item) as IBaseService);
        }
        // We have to init the services after all of them are created as
        // some depend on the ObjectService on initialization.
        foreach(var item in services)
        {
            item.Init();
        }
    }
}
