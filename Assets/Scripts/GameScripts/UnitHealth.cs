using System;
using System.Collections;
using UnityEngine;

public class UnitHealth : MonoBehaviour, IDamagable
{
    public static event Action<int> OnPlayerHealthChanged;
    public static event Action OnPlayerDied;

    [SerializeField] private UnitHud _playerHUD;

    [SerializeField] private int _maxHealth;

    [SerializeField] private float _invincibilityTime = .5f;

    private int _currentHealth;

    private bool _isInvincible;

    public int CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;

            OnPlayerHealthChanged(_currentHealth);

            if (_currentHealth <= 0)
            {
                OnPlayerDied?.Invoke();
            }
        }
    }

    private void Start()
    {
        CurrentHealth = _maxHealth;
        _playerHUD.SetHUD(_maxHealth, _currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (_isInvincible) return;

        CurrentHealth -= damage;

        StartCoroutine(EnterAndExitInvincibility());
    }

    private IEnumerator EnterAndExitInvincibility()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(_invincibilityTime);
        _isInvincible = false;
    }
}