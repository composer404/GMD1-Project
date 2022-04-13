using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float walkZoneRadius = 20f;

    [SerializeField]
    private float visibleZoneRadius = 10f;

    [SerializeField]
    private float attackZoneRadius = 2f;

    [SerializeField]
    private HealthBarController healthBar;

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private GameObject attackedObject;
    private EnemyStat enemyStat;
    private Vector3 walkDestination;
    private bool isDestination;

    void Start() {
        player = PlayerManager.GetInstance().GetPlayer().transform;
        animator = gameObject.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        enemyStat = gameObject.GetComponent<EnemyStat>();
        healthBar.SetHealth(enemyStat.GetHealth());
    }

    void Update() {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance > visibleZoneRadius) {
            Patrol();
        }

        if (distance == visibleZoneRadius) {
            Notice();
        }

        if (distance <= visibleZoneRadius) {
            Follow();
        }

        if (distance <= attackZoneRadius) {
            Stop();
            Rotate();
        }
    }

    /* ---------------------------- Public functions ---------------------------- */

    public void GetDamage(int damage) {
        enemyStat.GetHit(damage);
        healthBar.SetHealth(enemyStat.GetHealth());
        StartCoroutine(IsDead());
    }

    /* ------------------------------ Enemy states ------------------------------ */

    private void Patrol() {
        if (transform.position == walkDestination) {
            isDestination = false;
        }

        if(!isDestination) {
            float randomZ = Random.Range(-walkZoneRadius, walkZoneRadius);
            float randomX = Random.Range(-walkZoneRadius, walkZoneRadius);

            walkDestination = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
            NavMeshHit hit;
            print(walkDestination);
            NavMeshPath navMeshPath = new NavMeshPath();
            if (NavMesh.SamplePosition(walkDestination, out hit, 1f, NavMesh.AllAreas) && agent.CalculatePath(walkDestination, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete) {
                walkDestination = hit.position;
                isDestination = true;
            }
            return;
        }
        agent.speed = 5;
        animator.SetBool("Run Forward", false);
        animator.SetBool("Walk Forward", true);
        agent.SetDestination(walkDestination);
    }

    private void Follow() {
        agent.speed = 10;
        agent.SetDestination(player.position);
        animator.SetBool("Walk Forward", false);
        animator.SetBool("Run Forward", true);
    }

    private void Notice() {
        animator.SetTrigger("Jump");
    }

    private void Stop() {
        agent.SetDestination(transform.position);
        animator.SetBool("Walk Forward", false);
        animator.SetBool("Run Forward", false);
    }

    private void Rotate() {
        animator.SetBool("Walk Forward", false);
        animator.SetBool("Run Forward", false);
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }

    private void Attack() {
        this.animator.SetTrigger("Attack 01");
    }

    private void OnTriggerEnter(Collider other) {
        print(other.gameObject.tag);
        if (other.gameObject.tag == "Player") {
            attackedObject = other.gameObject;
            InvokeRepeating ("AttackInLoop", 0, 1); 
        }
    }

    private void OnTriggerExit() {
        CancelInvoke("AttackInLoop");
    }

    
    /* ------- Gizmos that provide graphical representation of enemy zones ------- */

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkZoneRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visibleZoneRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackZoneRadius);
    }


    /* -------------------------------------------------------------------------- */
    /*                               HELP FUNCTIONS                               */
    /* -------------------------------------------------------------------------- */

    private void AttackInLoop() {
        animator.SetTrigger("Attack 01");
        PlayerController controller = attackedObject.GetComponent<PlayerController>();
        if (controller != null) {
            StartCoroutine(controller.GetHit(enemyStat.GetDamage()));
        }
    }

    private IEnumerator IsDead() {
        if (enemyStat.GetHealth() <= 0) {
            // Destroy(this);
            animator.SetTrigger("Die");
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
    }
}
