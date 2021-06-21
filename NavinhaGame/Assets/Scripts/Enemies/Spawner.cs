using System.Collections.Generic;
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

    private bool _canSpawn = true;

    private void OnEnable()
    {
        AEnemy.OnDespawn += ListenOnDespawn;
        GameManager.OnGameStateChanged += ListenOnStateChanged;
    }
    
    private void OnDisable()
    {
        AEnemy.OnDespawn -= ListenOnDespawn;
        GameManager.OnGameStateChanged -= ListenOnStateChanged;
    }

    private void Update()
    {
        if (!_canSpawn) return;
        
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _timer = _timeBetweenSpawns;
            SpawnEnemy(_enemyToSpawn[Random.Range(0,_enemyToSpawn.Length)]);
        }
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
    
    private void ListenOnStateChanged(IState state)
    {
        _canSpawn = state is Play;

        if (state is LoadLevel || state is Menu)
        {
            PoolingSystem.Instance.DespawnEveryone();
        }
    }

    private void OnValidate()
    {
        if (!_defaultArea) _defaultArea = this.GetComponentInChildren<SpawnArea>();
    }
}