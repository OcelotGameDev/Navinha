using System.ComponentModel.Design;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _timeBetweenSpawns = 2f;
    private float _timer;

    [SerializeField] private SpawnArea _defaultArea;

    [SerializeField] private SpawnArea[] _areas;

    [SerializeField] private string[] _enemyToSpawn;

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _timer = _timeBetweenSpawns;
            SpawnEnemy(_enemyToSpawn[Random.Range(0,_enemyToSpawn.Length)]);
        }
    }

    private void SpawnEnemy(string enemyTag)
    {
        int randomNumber = Random.Range(0, _areas.Length);
        PoolingSystem.Instance.SpawnObject(enemyTag, _areas[randomNumber].RandomPoint, _areas[randomNumber].Rotation);
    }

    private void OnValidate()
    {
        if (!_defaultArea) _defaultArea = this.GetComponentInChildren<SpawnArea>();
    }
}