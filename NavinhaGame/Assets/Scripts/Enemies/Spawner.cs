using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _timeBetweenSpawns = 2f;
    private float _timer;

    [SerializeField] private SpawnArea _defaultArea;

    [SerializeField] private SpawnArea[] _areas;

    [SerializeField] private string[] _enemyToSpawn;
    
    [SerializeField] private int _maxEnemiesOnScreen = 6;

    [SerializeField]private List<GameObject> _currentSpawnedEnemies = new List<GameObject>();

    private void OnEnable()
    {
        AEnemy.OnDespawn += ListenOnDespawn;
    }

    private void OnDisable()
    {
        AEnemy.OnDespawn -= ListenOnDespawn;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _timer = _timeBetweenSpawns;
            SpawnEnemy(_enemyToSpawn[Random.Range(0,_enemyToSpawn.Length)]);
        }
        
        Debug.Log(_currentSpawnedEnemies.Count);
    }

    private void SpawnEnemy(string enemyTag)
    {
        if (_currentSpawnedEnemies.Count >= _maxEnemiesOnScreen) return;
        
        int randomNumber = Random.Range(0, _areas.Length);
        _currentSpawnedEnemies.Add(PoolingSystem.Instance.SpawnObject(enemyTag, _areas[randomNumber].RandomPoint, _areas[randomNumber].Rotation));
    }
    
    private void ListenOnDespawn(GameObject enemyObj)
    {
        if (_currentSpawnedEnemies.Contains(enemyObj))
        {
            _currentSpawnedEnemies.Remove(enemyObj);
        }
    }

    private void OnValidate()
    {
        if (!_defaultArea) _defaultArea = this.GetComponentInChildren<SpawnArea>();
    }
}