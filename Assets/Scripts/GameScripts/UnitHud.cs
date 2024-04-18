using UnityEngine;
using UnityEngine.UI;

public class UnitHUD : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _easeHealthSlider;

    [SerializeField] private float _easeLerpSpeed = 0.05f;

    [SerializeField] private UnitHealth _healathScript;

    private void OnEnable()
    {
        _healathScript.OnUnitHealthChanged += SetHealth;
    }

    private void Update()
    {
        if(_healthSlider.value != _easeHealthSlider.value)
        {
            _easeHealthSlider.value = Mathf.Lerp(_easeHealthSlider.value, _healthSlider.value, _easeLerpSpeed);
        }
    }

    private void OnDisable()
    {
        _healathScript.OnUnitHealthChanged -= SetHealth;
    }

    public void SetHUD(int maxHealth, int currentHealth)
    {
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = currentHealth;

        _easeHealthSlider.maxValue = maxHealth;
        _easeHealthSlider.value = currentHealth;
    }

    public void SetHealth(int health) => _healthSlider.value = health;
}