using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawnerHUD : MonoBehaviour
{
    [SerializeField] private Slider _spawnSlider;

    private bool _shouldCount = false;

    private void Update()
    {
        if (!_shouldCount)
            return;

        SetSpawnTime();
    }

    public void SetHUD(float spawnTime)
    {
        _spawnSlider.maxValue = spawnTime;
        _spawnSlider.value = 0;

        _shouldCount = true;
    }

    public void SetSpawnTime()
    {
        _spawnSlider.value += Time.deltaTime;

        if (_spawnSlider.value == _spawnSlider.maxValue)
            _spawnSlider.value = 0f;
    }
}
