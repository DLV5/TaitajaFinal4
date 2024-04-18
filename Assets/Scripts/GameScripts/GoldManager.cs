using System;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public event Action<int> OnGoldAmountChanged;

    [SerializeField] private int _goldPerSecond = 1;

    private int _currentGold = 0;

    public int CurrentGold
    {
        get => _currentGold;
        private set
        {
            _currentGold = value;
            OnGoldAmountChanged?.Invoke(_currentGold);
        }
    }

    public static GoldManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        InvokeRepeating("AddGold", 0, 1f);
    }

    private void AddGold() => CurrentGold += _goldPerSecond;
}
