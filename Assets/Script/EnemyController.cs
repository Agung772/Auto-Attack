using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class EnemyController : MonoBehaviour
{
    public int hpEnemy;
    int totalHpEnemy;
    public NavMeshAgent agent;
    public GameObject playerTarget;
    public Animator animator;
    public GameObject textDamage, expPrefab;

    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        animator.SetBool("Run", true);

        totalHpEnemy = hpEnemy;
    }
    private void Update()
    {
        agent.SetDestination(playerTarget.transform.position);
    }
    public void Hit(int damage, bool critical)
    {
        hpEnemy -= damage;
        if (critical)
        {
            SpawnTextDamage(damage, true);
        }
        else if (!critical)
        {
            SpawnTextDamage(damage, false);
        }
        if (hpEnemy <= 0)
        {
            Death();
            SpawnExp();
        }
    }
    public void Death()
    {
        agent.speed = 0;
        animator.SetBool("Death", true);
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        this.enabled = false;

        playerTarget.GetComponent<PlayerController>().enemyTarget = null;
        StartCoroutine(DestroyDelay());
        IEnumerator DestroyDelay()
        {
            yield return new WaitForSeconds(10f);
            Destroy(gameObject);
        }
    }

    void SpawnTextDamage(int damageText, bool critical)
    {
        if (critical)
        {
            GameObject textObject = Instantiate(textDamage, transform.position + new Vector3(0, 2, 0), Quaternion.Euler(45, 0, 0));
            textObject.GetComponentInChildren<TextMeshPro>().fontSize = 25;
            textObject.GetComponentInChildren<TextMeshPro>().text = "" + damageText;
            Destroy(textObject, 0.6f);
        }
        else if (!critical)
        {
            GameObject textObject = Instantiate(textDamage, transform.position + new Vector3(0, 2, 0), Quaternion.Euler(45, 0, 0));
            textObject.GetComponentInChildren<TextMeshPro>().fontSize = 10;
            textObject.GetComponentInChildren<TextMeshPro>().text = "" + damageText;
            Destroy(textObject, 0.6f);
        }

    }

    void SpawnExp()
    {
        GameObject expObject = Instantiate(expPrefab, transform.position, transform.rotation);
        expObject.GetComponent<ExpController>().exp = totalHpEnemy / 4;
    }



}
