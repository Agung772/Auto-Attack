using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int expPlayer;
    public int physicalAttack, criticalDamage, criticalRate;
    public int attackSpeed;

    public PlayerController playerController;
    public static GameManager instance;

    public TextMeshProUGUI expPlayerText, physicalAttText, attackSpeedText, criticalDamageText, criticalRateText;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        instance = this;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;

        StartStat();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            physicalAttack++;
            StartStat();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            attackSpeed++;
            StartStat();
        }
    }

    void StartStat()
    {
        playerController.expPlayer = expPlayer;
        playerController.physicalAtk = physicalAttack;
        playerController.atkSpeed = attackSpeed;
        playerController.criticalDamage = criticalDamage;
        playerController.criticalRate = criticalRate;

        expPlayerText.text = "Exp : " + expPlayer;
        physicalAttText.text = "Physical Attack : " + physicalAttack;
        attackSpeedText.text = "Attack Speed : " + attackSpeed + "%";
        criticalDamageText.text = "Critical Damage : " + criticalDamage;
        criticalRateText.text = "Critical Rate : " + criticalRate + "%";
    }

    public void UpdateStat()
    {
        expPlayer = playerController.expPlayer;
        physicalAttack = playerController.physicalAtk;
        attackSpeed = playerController.atkSpeed;
        criticalDamage = playerController.criticalDamage;
        criticalRate = playerController.criticalRate;

        expPlayerText.text = "Exp : " + expPlayer;
        physicalAttText.text = "Physical Attack : " + physicalAttack;
        attackSpeedText.text = "Attack Speed : " + attackSpeed + "%";
        criticalDamageText.text = "Critical Damage : " + criticalDamage;
        criticalRateText.text = "Critical Rate : " + criticalRate + "%";
    }
}
