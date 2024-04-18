using UnityEngine;
using UnityEngine.UI;

public class GoldButton : MonoBehaviour
{
    [SerializeField] private int _buildingPrice = 30;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        GoldManager.Instance.OnGoldAmountChanged += ToggleButton;
    }

    private void OnDisable()
    {
        GoldManager.Instance.OnGoldAmountChanged -= ToggleButton;
    }

    public void SpendGold()
    {
        GoldManager.Instance.SpendGold(_buildingPrice);
    } 

    private void ToggleButton(int gold) => _button.interactable = CanBuy(gold);

    private bool CanBuy(int gold) => _buildingPrice <= gold;
}
