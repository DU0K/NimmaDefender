using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private int money = 150;

    [SerializeField] private TMP_Text moneyText;

    private void Start()
    {
        moneyText.text = ("Money: " + money.ToString());
    }
    public void addMoney(int add)
    {
        money += add;
        moneyText.text = ("Money: " + money.ToString());
    }
    public void removeMoney(int remove) {
        money -= remove;
        moneyText.text = ("Money: " + money.ToString());
    }
}
