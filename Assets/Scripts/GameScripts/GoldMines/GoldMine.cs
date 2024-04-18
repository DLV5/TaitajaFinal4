using UnityEngine;

public class GoldMine : MonoBehaviour
{
    [SerializeField] private int _increaseGoldAmount = 2;

    private void OnEnable()
    {
        GoldManager.Instance.IncreaseGoldPerSecond(_increaseGoldAmount);
    }
    
    private void OnDisable()
    {
        GoldManager.Instance.DecreaseGoldPerSecond(_increaseGoldAmount);
    }
}
