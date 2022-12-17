using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //PlayerStat
    public int expPlayer;
    public int physicalAtk, criticalDamage, criticalRate;
    public int atkSpeed;
    public int Damage;
    //PlayerController
    public CharacterController characterController;
    public Animator animator;
    public float speedPlayer, jumpForce;
    public float speedRotationTarget;
    public float gravity = -9.81f;
    public bool useGravity;
    float directionY;
    float moveX, moveZ;
    Vector3 direction;

    //
    [Space]
    public GameObject enemyTarget;

    [Space]
    public GameObject projectilePrefab;
    public GameObject spawnPoint;
    public float speedProjectile;

    private void Start()
    {
        cooldownAtt = true;
    }
    void Update()
    {
        MovePlayer();
        ShootProjectile();
    }

    void MovePlayer()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");   
        direction = new Vector3(moveX, 0, moveZ);

        //Jump ---------------------------------------------------------------------------
        if (characterController.isGrounded)
        {
            directionY = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                directionY = jumpForce;
            }
        }
        directionY += gravity * Time.deltaTime;
        if (useGravity) direction.y = directionY;
        characterController.Move(direction * speedPlayer * Time.deltaTime);

        if (enemyTarget != null)
        {
            AutoRotationTarget();

            //Animasi
            if (moveX == 0 && moveZ == 0)
            {
                animator.SetBool("Idle", true);
                animator.SetBool("Forward", false);
                animator.SetBool("Right", false);
                animator.SetBool("Left", false);
                animator.SetBool("Back", false);
            }
            if (moveX > 0)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Forward", false);
                animator.SetBool("Right", true);
                animator.SetBool("Left", false);
                animator.SetBool("Back", false);
            }
            else if (moveX < 0)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Forward", false);
                animator.SetBool("Right", false);
                animator.SetBool("Left", true);
                animator.SetBool("Back", false);
            }
            if (moveZ > 0)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Forward", true);
                animator.SetBool("Right", false);
                animator.SetBool("Left", false);
                animator.SetBool("Back", false);
            }
            else if (moveZ < 0)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Forward", false);
                animator.SetBool("Right", false);
                animator.SetBool("Left", false);
                animator.SetBool("Back", true);
            }

        }
        else if (enemyTarget == null)
        {
            NormalRotation();

            //Animasi
            animator.SetBool("Idle", false);
            animator.SetBool("Forward", true);
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
            animator.SetBool("Back", false);

            if (moveX == 0 && moveZ == 0)
            {
                animator.SetBool("Idle", true);
                animator.SetBool("Forward", false);
                animator.SetBool("Right", false);
                animator.SetBool("Left", false);
                animator.SetBool("Back", false);
            }
        }
    }

    void AutoRotationTarget()
    {
        Quaternion rotTarget = Quaternion.LookRotation(enemyTarget.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotTarget, speedRotationTarget * Time.deltaTime);
    }

    void NormalRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            float heading = Mathf.Atan2(moveX, moveZ);
            transform.rotation = Quaternion.Euler(0, heading * Mathf.Rad2Deg, 0);
        }
    }
    bool cooldownAtt, jedaAtt;
    public bool criticalRateBool;
    void ShootProjectile()
    {
        //Delay shoot awal
        if (enemyTarget != null)
        {
            if (!jedaAtt)
            {
                jedaAtt = true;
                StartCoroutine(JedaCoroutine());
                IEnumerator JedaCoroutine()
                {
                    yield return new WaitForSeconds(0.5f);
                    cooldownAtt = false;
                }
            }
        }

        else if (enemyTarget == null)
        {
            jedaAtt = false;
            cooldownAtt = true;
        }

        //shoot
        if (!cooldownAtt && enemyTarget != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                cooldownAtt = true;
                StartCoroutine(SpeedCoroutine());
                IEnumerator SpeedCoroutine()
                {
                    //CondisitionCryticalRate
                    criticalRateBool = (Random.value < (float)criticalRate / 100);
                    if (criticalRateBool)
                    {
                        Damage = 0;
                        Damage = physicalAtk;
                        Damage += Damage * criticalDamage / 100;
                    }
                    else if (!criticalRateBool)
                    {
                        Damage = 0;
                        Damage = physicalAtk;
                    }

                    //Projectile
                    GameObject projectile = Instantiate(projectilePrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    projectile.GetComponent<Rigidbody>().AddForce(spawnPoint.transform.forward * speedProjectile, ForceMode.Impulse);
                    projectile.GetComponent<ProjectileController>().damageProjectile = Damage;
                    projectile.GetComponent<ProjectileController>().criticalProjectile = criticalRateBool;
                    Destroy(projectile, 5);

                    //AttackSpeed
                    float speedCalculate = 2 - (float)atkSpeed / 100;
                    yield return new WaitForSeconds(speedCalculate);
                    cooldownAtt = false;
                }



            }
        }

    }

    public void AddExp(int exp)
    {
        expPlayer += exp;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyTarget = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyTarget = null;
        }
    }
}
