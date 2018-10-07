using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform _knightStartSpawnPoint;
    [SerializeField] private Vector3 _spawnStep;

    [SerializeField] private PrefabData _knightPrefab;


    private Vector3 _currentPosition;


    private void Start()
    {
        _currentPosition = _knightStartSpawnPoint.position;
    }

    
    public void SpawnKnight()
    {
        Instantiate(_knightPrefab.prefab, 
                    _currentPosition,
                    Quaternion.identity,
                    _knightPrefab.parent);

        _currentPosition += _spawnStep;
    }
}
