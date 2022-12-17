using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpController : MonoBehaviour
{
    public int exp;
    public GameObject playerTarget;
    public float speedFollow;
    public bool follow;
    public Material materialExp;

    public new Rigidbody rigidbody;
    private IEnumerator Start()
    {
        RandomColor();

        rigidbody.velocity = Vector3.up * 5;

        playerTarget = GameObject.FindGameObjectWithTag("Player");
        yield return new WaitForSeconds(2);
        follow = true;
    }
    void Update()
    {
        if (follow)
        {
            rigidbody.useGravity = false;
            speedFollow += Time.deltaTime * 3;
            transform.position = Vector3.Lerp(transform.position, playerTarget.transform.position, speedFollow * Time.deltaTime);
        }
    }

    void RandomColor()
    {
        float random = Random.Range(0, 5);
        materialExp = gameObject.GetComponent<MeshRenderer>().materials[0];

        if (random == 0)
        {
            materialExp.SetColor("_EmissionColor", Color.red * 7);
        }
        else if (random == 1)
        {
            materialExp.SetColor("_EmissionColor", Color.blue * 7);
        }
        else if (random == 2)
        {
            materialExp.SetColor("_EmissionColor", Color.green * 7);
        }
        else if (random == 3)
        {
            materialExp.SetColor("_EmissionColor", Color.yellow * 7);
        }
        else if (random == 4)
        {
            materialExp.SetColor("_EmissionColor", Color.magenta * 7);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<PlayerController>().AddExp(exp);
            GameManager.instance.UpdateStat();
            Destroy(gameObject);
        }
    }
}
