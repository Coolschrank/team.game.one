using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropRate : MonoBehaviour
{
    public int score, itemDropRate, itemDrop, itemDropCount,currentItem, upgradeDropRate, upgradeDrop, upgradeDropCount, upgradeItem, itemRise, upgradeRise;
    public bool ItemDropped, upgradeDropped;
    public GameObject[] itemDrops, upgradeDrops;
    GameObject currentItemDrop, currentUpgradwDrop, lastUpgrade;
    public Text text;

    void Start()
    {
        NextItemDrop();
        NextUpgradeDrop();
    }

    void Update()
    {
        text.text = score.ToString();
    }

    public GameObject ScoreEnemy(int enemyScore)
    {
        score += enemyScore;
        itemDropCount += enemyScore;
        upgradeDropCount += enemyScore;
        if (itemDropCount >= itemDrop && ItemDropped==false)
        {
            DropItem();
            return currentItemDrop;
        }
        else if (upgradeDropCount >= upgradeDrop && upgradeDropped == false)
        {
            DropUpgrade();
            return currentUpgradwDrop;
        }
        if (itemDropCount >= itemDropRate)
        {
            ResetDropI();
        }
        else if (upgradeDropCount >= upgradeDropRate)
        {
            ResetDropU();
        }
        return null;
    }

    public void NextItemDrop()
    {
        
        itemDrop = (int)UnityEngine.Random.Range(0, itemDropRate);
    }
    public void NextUpgradeDrop()
    {
        
        upgradeDrop = (int)UnityEngine.Random.Range(0, upgradeDropRate);
    }
    public void ResetDropI()
    {
        itemDropCount = 0;
        NextItemDrop();
        ItemDropped = false;
    }
    public void ResetDropU()
    {
        upgradeDropCount = 0;
        NextUpgradeDrop();
        upgradeDropped = false;
    }
    public void DropItem()
    {
        ItemDropped = true;
        if(currentItem >= itemDrops.Length)
        {
            currentItem = 0;
        }
        currentItemDrop = itemDrops[currentItem];
        currentItem++;
        itemDropRate += itemRise;
    }
    public void DropUpgrade()
    {
        upgradeDropped = true;
        currentUpgradwDrop = upgradeDrops[UnityEngine.Random.Range(0, upgradeDrops.Length)];
        if(currentUpgradwDrop == lastUpgrade)
        {
            DropUpgrade();
                return;
        }
        upgradeDropRate += upgradeRise;
        lastUpgrade = currentUpgradwDrop;
    }
}
