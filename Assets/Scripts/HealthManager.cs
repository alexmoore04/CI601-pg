using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public GameObject deathPanel;
    public Image healthBar;
    public DificultyTracker healthAmmount;
    private GameObject PC;
    private bool dead = false;
    public GameObject winPanel;

    void Start()
    {
        PC = GameObject.Find("Player");
        healthAmmount = PC.GetComponent<DificultyTracker>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthBar.fillAmount = healthAmmount.health / 10f;
        if (healthAmmount.health <= 0 && dead == false)
        {
            healthAmmount.saveUpdater++;
            Destroy(PC);
            deathPanel.SetActive(true);
            dead = true;
        }
    }

    public void GameWon()
    {
        winPanel.SetActive(true);
        Destroy(PC);
    }
}
