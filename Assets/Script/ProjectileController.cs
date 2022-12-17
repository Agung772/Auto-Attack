using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public int damageProjectile;
    public bool criticalProjectile;
    public ParticleSystem efectSentuhan, projectile;
    public ParticleSystem efectSpawn, efectParticle, beam;

    private void Start()
    {
        efectSpawn.Play();
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Untagged"))
        {
            StartCoroutine(Particle());
            IEnumerator Particle()
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.isKinematic = true;
                CapsuleCollider capsule = GetComponent<CapsuleCollider>();
                capsule.enabled = false;

                efectSentuhan.Play();
                yield return new WaitForSeconds(1);
                beam.Stop();
                projectile.Stop();
                efectParticle.Stop();
                Destroy(gameObject, 3f);
            }
        }
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(Particle());
            IEnumerator Particle()
            {
                //Hit
                other.GetComponent<EnemyController>().Hit(damageProjectile, criticalProjectile);

                //Particle
                CapsuleCollider capsule = GetComponent<CapsuleCollider>();
                capsule.enabled = false;
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.isKinematic = true;
                projectile.Stop();
                beam.Stop();
                efectSentuhan.Play();
                yield return new WaitForSeconds(1);
                efectParticle.Stop();


                Destroy(gameObject, 3f);
            }
        }
    }
}
