using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SpawnManager _spawnManager;
    
    private Health _lastAddedHealth;


#if UNITY_EDITOR
    private void OnValidate()
    {
        _spawnManager = GetComponent<SpawnManager>();
        if (_spawnManager is null)
        {
            Debug.LogError("SpawnManager not found!", this);
        }
    }
#endif
    
    
    private void OnEnable()
    {
        Health.HealthAdded += SetLastAddedHealth;
    }

    private void OnDisable()
    {
        Health.HealthAdded -= SetLastAddedHealth;
    }
    
    
    private void SetLastAddedHealth(Health health)
    {
        _lastAddedHealth = health;
    }

    
    
    public void Heal(float count)
    {
        _lastAddedHealth.Heal(count);
    }
    
    public void Hit(float count)
    {
        _lastAddedHealth.TakeDamage(count);
    }
    
    

    public void CreateNewKnight()
    {
        _spawnManager.SpawnKnight();
    }
}
