using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _secondsBetweenSpawn;

    private Transform[] _points;
    private Transform _spawn;

    private void OnValidate()
    {
        if (_secondsBetweenSpawn < 0.5f)
        {
            _secondsBetweenSpawn = 0.5f;
        }
    }

    private void Start()
    {
        _spawn = GetComponent<Transform>();
        _points = new Transform[_spawn.childCount];

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i] = _spawn.GetChild(i);
        }

        var spawnEnemyJob = StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        var waitForSeconds = new WaitForSeconds(_secondsBetweenSpawn);

        for (int i = 0; i < _points.Length; i++)
        {
            Instantiate(_enemy, _points[i].position, Quaternion.identity);
            yield return waitForSeconds;
        }
    }
}
