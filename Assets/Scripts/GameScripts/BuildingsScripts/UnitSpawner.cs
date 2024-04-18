using System.Collections;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private float _unitPerSecondSpawn;

    [SerializeField] private Transform _spawnPoint;

    private UnitSpawnerHUD _hud;

    private void Awake()
    {
        _hud = GetComponent<UnitSpawnerHUD>();
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnAfterTime());
        _hud.SetHUD(1 /_unitPerSecondSpawn);
    }
    
    private void OnDisable()
    {
        StopCoroutine(SpawnAfterTime());
    }

    private IEnumerator SpawnAfterTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / _unitPerSecondSpawn);
            SpawnUnit();
        }
    }

    private void SpawnUnit()
    {
        Instantiate(_unitPrefab, _spawnPoint.position, Quaternion.identity);
    }
}
