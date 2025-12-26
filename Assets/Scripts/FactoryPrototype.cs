using UnityEngine;
using UnityEngine.UI;

public class FactoryPrototype : MonoBehaviour
{
    [Header("Stats")]
    public int level = 1;
    public int money = 0;
    public int resources = 999; // бесконечные ресурсы для прототипа

    [Header("Production")]
    public float productionTime = 2f;
    private float timer;

    [Header("Economy")]
    public int baseProductPrice = 10;
    public float productPriceMultiplier = 1.3f;

    public int baseUpgradeCost = 50;
    public float upgradeCostMultiplier = 1.6f;

    [Header("UI")]
    public Text levelText;
    public Text moneyText;
    public Text priceText;
    public Text upgradeCostText;

    void Update()
    {
        Produce();
        UpdateUI();
    }

    void Produce()
    {
        if (resources <= 0)
            return;

        timer += Time.deltaTime;

        if (timer >= productionTime)
        {
            timer = 0;
            SellProduct();
        }
    }

    void SellProduct()
    {
        int price = GetProductPrice();
        money += price;
    }

    public void Upgrade()
    {
        int cost = GetUpgradeCost();

        if (money < cost)
            return;

        money -= cost;
        level++;
    }

    int GetProductPrice()
    {
        return Mathf.RoundToInt(
            baseProductPrice *
            Mathf.Pow(productPriceMultiplier, level - 1)
        );
    }

    int GetUpgradeCost()
    {
        return Mathf.RoundToInt(
            baseUpgradeCost *
            Mathf.Pow(upgradeCostMultiplier, level - 1)
        );
    }

    void UpdateUI()
    {
        levelText.text = "Level: " + level;
        moneyText.text = "Money: " + money;
        priceText.text = "Product Price: " + GetProductPrice();
        upgradeCostText.text = "Upgrade Cost: " + GetUpgradeCost();
    }
}
