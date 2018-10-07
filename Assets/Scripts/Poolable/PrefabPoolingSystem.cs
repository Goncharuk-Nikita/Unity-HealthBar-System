using System.Collections.Generic;
using UnityEngine;

public static class PrefabPoolingSystem
{
    private static readonly Dictionary<GameObject, PrefabPool> _prefabToPoolMap =
        new Dictionary<GameObject, PrefabPool>();

    private static readonly Dictionary<GameObject, PrefabPool> _goToPoolMap =
        new Dictionary<GameObject, PrefabPool>();


    
    
    
    public static PoolablePrefabData Spawn(GameObject prefab, Vector3 position, 
                                   Quaternion rotation, Transform parent = null)
    {
        var pool = GetPrefabPool(prefab);

        var data = pool.Spawn(prefab, position, rotation, parent);

        // Add to poolMap
        _goToPoolMap.Add(data.go, pool);
        return data;
    }

    public static PoolablePrefabData Spawn(GameObject prefab, Transform parent = null)
    {
        return Spawn(prefab, Vector3.zero, Quaternion.identity, parent);
    }

    public static bool Despawn(GameObject prefab)
    {
        PrefabPool pool;
        if (!_goToPoolMap.TryGetValue(prefab, out pool))
        {
            Debug.LogError
                (string.Format("Prefab {0} not managed by pool system", prefab.name));
            return false;
        }

        if (pool == null || !pool.Despawn(prefab))
            return false;

        return _goToPoolMap.Remove(prefab);
    }

    public static List<PoolablePrefabData> Prespawn(GameObject prefab, int count, 
        Transform parent = null)
    {
        var prefabs = new List<PoolablePrefabData>(count);
        PrefabPool pool = GetPrefabPool(prefab);

        for (int i = 0; i < count; i++)
        {
            prefabs.Add(pool.Prespawn(prefab, parent));
        }

        return prefabs;
    }

    public static void Reset()
    {
        _prefabToPoolMap.Clear();
        _goToPoolMap.Clear();
    }




    private static PrefabPool GetPrefabPool(GameObject prefab)
    {
        PrefabPool pool;
        if (!_prefabToPoolMap.TryGetValue(prefab, out pool))
        {
            _prefabToPoolMap.Add(prefab, pool = new PrefabPool());
        }
        
        return pool;
    }
}
