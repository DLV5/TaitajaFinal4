using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _goldText;

    private void OnEnable()
    {
        GoldManager.Instance.OnGoldAmountChanged += UpdateGoldPanel;
    } 
    
    private void OnDisable()
    {
        GoldManager.Instance.OnGoldAmountChanged -= UpdateGoldPanel;
    }

    private void UpdateGoldPanel(int gold)
    {
        _goldText.text = gold.ToString();
    }
}
