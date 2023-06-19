using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Player player;
    private WaveSpawner waveSpawner;

    [SerializeField]
    private GameObject upgradeMenu;
    [SerializeField]
    private Text moneyText;
    [SerializeField]
    private GameObject hpUgradeButton;
    [SerializeField]
    private GameObject damageUgradeButton;
    [SerializeField]
    private GameObject firerateUgradeButton;
    [SerializeField]
    private GameObject startWaveText;
    [SerializeField]
    private GameObject waveProgressFill;
    [SerializeField]
    private Text waveInfoText;
    [SerializeField]
    private GameObject restartGamePanel;
    [SerializeField]
    private Text endText;

    private int hpLevel = 0;
    private int damageLevel = 0;
    private int firerateLevel = 0;

    private List<Enemy> enemies;
    public bool gameHasStarted = false;
    public bool waveHasStarted = false;
    public int mobValue = 10;
    public int enemyNum = 0;

    private void Awake()
    {
        restartGamePanel.SetActive(false);
        mobValue = 10;
        player = FindObjectOfType<Player>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
        upgradeMenu.SetActive(false);
        startWaveText.SetActive(false);
        moneyText.text = "50";
        waveInfoText.text = (waveSpawner.currentWaveIndex + 1).ToString() + "/10";
        hpUgradeButton.transform.GetChild(0).GetComponent<Text>().text = "Free";
        damageUgradeButton.transform.GetChild(0).GetComponent<Text>().text = "Free";
        firerateUgradeButton.transform.GetChild(0).GetComponent<Text>().text = "Free";
        enemyNum = waveSpawner.waves[waveSpawner.currentWaveIndex].enemies.Length;
        enemies = new List<Enemy>();
    }

    private void Update()
    {
        if (waveHasStarted && enemies.Count == 0)
        {
            waveHasStarted = false;
            waveSpawner.currentWaveIndex++;
            mobValue += 5;
            if (waveSpawner.currentWaveIndex != 10)
            {
                startWaveText.SetActive(true);
            }
            else
            {
                WinGame();
            }
            player.health = player.maxHealth;
        }

        if (gameHasStarted == true && waveHasStarted == false)
        {
            if (player.transform.position.x > 9 && player.transform.position.x < 29 && 
                player.transform.position.z > -80 && player.transform.position.z < -60)
            {
                waveInfoText.text = (waveSpawner.currentWaveIndex + 1).ToString() + "/10";
                waveSpawner.isWaveOngoing = false;
                startWaveText.SetActive(false);
                waveProgressFill.GetComponent<Image>().fillAmount = 1;
                enemyNum = waveSpawner.waves[waveSpawner.currentWaveIndex].enemies.Length;
            }
        }
    }

    public void HealthUpgrade()
    {
        if (hpUgradeButton.transform.GetChild(0).GetComponent<Text>().text == "Free")
        {
            hpLevel++;
            player.maxHealth += 1f;
            player.health += 1f;
            if (hpLevel == 1)
            {
                hpUgradeButton.transform.GetChild(0).GetComponent<Text>().text = "10";

            }
            else
            {
                hpUgradeButton.transform.GetChild(0).GetComponent<Text>().text = (int.Parse(hpUgradeButton.transform.GetChild(0).GetComponent<Text>().text) + Mathf.Pow(hpLevel, 2) * 5).ToString();
            }
        }
        else if (int.Parse(moneyText.text) >= int.Parse(hpUgradeButton.transform.GetChild(0).GetComponent<Text>().text))
        {
            moneyText.text = (int.Parse(moneyText.text) - int.Parse(hpUgradeButton.transform.GetChild(0).GetComponent<Text>().text)).ToString();
            hpLevel++;
            player.maxHealth += 1f;
            player.health += 1f;
            if (hpLevel == 1)
            {
                hpUgradeButton.transform.GetChild(0).GetComponent<Text>().text = "10";
            }
            else
            {
                hpUgradeButton.transform.GetChild(0).GetComponent<Text>().text = (int.Parse(hpUgradeButton.transform.GetChild(0).GetComponent<Text>().text) + Mathf.Pow(hpLevel, 2) * 5).ToString();
            }
        }
    }

    public void DamageUpgrade()
    {
        if (damageUgradeButton.transform.GetChild(0).GetComponent<Text>().text == "Free")
        {
            damageLevel++;
            player.playerGun.damage += 0.5f;
            if (damageLevel == 1)
            {
                damageUgradeButton.transform.GetChild(0).GetComponent<Text>().text = "10";
            }
            else
            {
                damageUgradeButton.transform.GetChild(0).GetComponent<Text>().text = (int.Parse(damageUgradeButton.transform.GetChild(0).GetComponent<Text>().text) + Mathf.Pow(damageLevel, 2) * 5).ToString();
            }
        }
        else if (int.Parse(moneyText.text) >= int.Parse(damageUgradeButton.transform.GetChild(0).GetComponent<Text>().text))
        {
            moneyText.text = (int.Parse(moneyText.text) - int.Parse(damageUgradeButton.transform.GetChild(0).GetComponent<Text>().text)).ToString();
            damageLevel++;
            player.playerGun.damage += 0.5f;
            if (damageLevel == 1)
            {
                damageUgradeButton.transform.GetChild(0).GetComponent<Text>().text = "10";
            }
            else
            {
                damageUgradeButton.transform.GetChild(0).GetComponent<Text>().text = (int.Parse(damageUgradeButton.transform.GetChild(0).GetComponent<Text>().text) + Mathf.Pow(damageLevel, 2) * 5).ToString();
            }
        }
    }

    public void FirerateUpgrade()
    {
        if (firerateUgradeButton.transform.GetChild(0).GetComponent<Text>().text == "Free")
        {
            firerateLevel++;
            player.fireRate += 0.2f;
            if (firerateLevel == 1)
            {
                firerateUgradeButton.transform.GetChild(0).GetComponent<Text>().text = "10";
            }
            else
            {
                firerateUgradeButton.transform.GetChild(0).GetComponent<Text>().text = (int.Parse(firerateUgradeButton.transform.GetChild(0).GetComponent<Text>().text) + Mathf.Pow(firerateLevel, 2) * 5).ToString();
            }
        }
        else if (int.Parse(moneyText.text) >= int.Parse(firerateUgradeButton.transform.GetChild(0).GetComponent<Text>().text))
        {
            moneyText.text = (int.Parse(moneyText.text) - int.Parse(firerateUgradeButton.transform.GetChild(0).GetComponent<Text>().text)).ToString();
            firerateLevel++;
            player.fireRate += 0.2f;
            if (firerateLevel == 1)
            {
                firerateUgradeButton.transform.GetChild(0).GetComponent<Text>().text = "10";
            }
            else
            {
                firerateUgradeButton.transform.GetChild(0).GetComponent<Text>().text = (int.Parse(firerateUgradeButton.transform.GetChild(0).GetComponent<Text>().text) + Mathf.Pow(firerateLevel, 2) * 5).ToString();
            }
        }
    }

    public void ChangeUpgradeMenuVisibility()
    {
        upgradeMenu.SetActive(!upgradeMenu.activeInHierarchy);
    }

    public void GetKillMoney(int amount)
    {
        moneyText.text = (int.Parse(moneyText.text) + amount).ToString();
    }

    public void AddEnemy(Enemy newEnemy)
    {
        enemies.Add(newEnemy);
    }

    public void RemoveEnemy(Enemy removeEnemy)
    {
        float fillAmount = (float)(enemyNum - 1) / (float)(waveSpawner.waves[waveSpawner.currentWaveIndex].enemies.Length);
        waveProgressFill.GetComponent<Image>().fillAmount = fillAmount;
        enemies.Remove(removeEnemy);
        enemyNum--;
    }

    public void PlayerDie()
    {
        endText.text = "You died. Do you want to restart the game?";
        StartCoroutine(DeadScreen());
    }

    IEnumerator DeadScreen()
    {
        yield return new WaitForSeconds(3);

        restartGamePanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WinGame()
    {
        player.WinAnim();
        endText.text = "You won! Do you want to restart the game?";
        StartCoroutine(DeadScreen());
    }
}
