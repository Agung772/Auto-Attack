using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject zombiePrefab;
    public float jedaSpawn;
    private void Start()
    {
        InvokeRepeating("SpawnZombie", 0, jedaSpawn);
        InvokeRepeating("SpawnZombieLarge", 0, jedaSpawn * 2f);
    }

    void SpawnZombie()
    {
        Instantiate(zombiePrefab, gameObject.transform);
    }
    void SpawnZombieLarge()
    {
        GameObject zombie = Instantiate(zombiePrefab, gameObject.transform);
        zombie.transform.localScale = Vector3.one * 3;
        zombie.GetComponent<EnemyController>().hpEnemy = 2000;


    }
}
