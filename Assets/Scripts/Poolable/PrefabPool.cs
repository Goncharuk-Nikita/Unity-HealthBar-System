using System.Collections.Generic;
using UnityEngine;

public class PrefabPool {

    private readonly Dictionary<GameObject, PoolablePrefabData> _activeList =
        new Dictionary<GameObject, PoolablePrefabData>();

    private readonly Queue<PoolablePrefabData> _inactiveList =
        new Queue<PoolablePrefabData>();


    public PoolablePrefabData Spawn(GameObject prefab, Vector2 position, 
                            Quaternion rotation, Transform parent = null)
    {
        PoolablePrefabData data = _inactiveList.Count > 0 
                                  ? _inactiveList.Dequeue() 
                                  : Instantiate(prefab, position, Quaternion.identity);

        // loop for IPoolableComponents
        data.poolableComponent.Spawned();
    
        data.go.transform.position = position;
        data.go.transform.rotation = rotation;        

        // Add to active list
        _activeList.Add(data.go, data);
        data.go.SetActive(true);

        return data;
    }

    public bool Despawn(GameObject prefab)
    {
        // Check 
        PoolablePrefabData data;
        if (!_activeList.TryGetValue(prefab, out data))
        {
            Debug.LogError("This Object is not managed by this object pool!");
            return false;
        }

        // loop for IPoolableComponents
        data.poolableComponent.Despawned();

        // Deactivate object
        data.go.SetActive(false);
        _inactiveList.Enqueue(data);

        return _activeList.Remove(prefab);
    }

    public PoolablePrefabData Prespawn(GameObject prefab, Transform parent = null)
    {
        PoolablePrefabData data = Instantiate(prefab, Vector2.zero, 
                                  Quaternion.identity, parent);
        data.go.SetActive(false);

        _inactiveList.Enqueue(data);
        
        return data;
    }


    private PoolablePrefabData Instantiate(GameObject prefab, Vector2 position, 
                                           Quaternion rotation, Transform parent = null)
    {   
        var newGo = Object.Instantiate(prefab, position, rotation, parent);
        var prefabData = new PoolablePrefabData
        {
            go = newGo,
            poolableComponent = newGo.GetComponent<IPoolableComponent>()
        };

        prefabData.poolableComponent.Init();
        return prefabData;
    }
}
