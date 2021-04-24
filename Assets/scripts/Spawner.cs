using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _secondsBetweenSpawn;

    private Transform[] _points;

    private void OnValidate()
    {
        if (_secondsBetweenSpawn < 0.5f)
        {
            _secondsBetweenSpawn = 0.5f;
        }
    }

    private void Start()
    {
        _points = GetComponentsInChildren<Transform>();
        var spawnEnemyJob = StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        var waitForSeconds = new WaitForSeconds(_secondsBetweenSpawn);

        for (int i = 1; i < _points.Length; i++)
        {
            Instantiate(_enemy, _points[i].position, Quaternion.identity);
            yield return waitForSeconds;
        }
    }
}
