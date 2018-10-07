using System.Collections.Generic;
using UnityEngine;

public static class PoolableExtensions 
{
    #region PrefabData

    public static List<PoolablePrefabData> Install(this PrefabData prefabData)
    {
        return PrefabPoolingSystem.Prespawn(
            prefabData.prefab, 
            prefabData.count, 
            prefabData.parent);
    }

    public static PoolablePrefabData Spawn(this PrefabData prefabData)
    {
        return Spawn(prefabData, Vector2.zero, Quaternion.identity);
    }

    public static PoolablePrefabData Spawn(this PrefabData prefabData, Vector2 position)
    {
        return Spawn(prefabData, position, Quaternion.identity);
    }

    public static PoolablePrefabData Spawn(this PrefabData prefabData, Vector2 position, Quaternion rotation)
    {
        return PrefabPoolingSystem.Spawn(
            prefabData.prefab, 
            position, 
            rotation);
    }   

    #endregion
}
