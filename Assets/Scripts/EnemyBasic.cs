using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyBasic : MonoBehaviour {

    PolyNavAgent agent;
    Transform playerTransform;
    bool begunPathfinding; // are we looking for target right now?
    const float detectionDistance = 20f; // detection radius
    bool recoveringFromAttack;

    // enemy chills out after attacking
    // so that it has time to bounce around and such
    Timer attackRecoveryTimer;
    const float attackRecovery = 2f;
    const float attackVelocity = 700f; // attack 'launch' speed

    // timer started when movement resumes after attack
    // exists so that player has time to react after enemy starts moving again
    Timer attackCooldownTimer;
    const float attackCooldown = 3f;

    // Use this for initialization
    void Start () {
        var c = GameObject.Find("Entities")
                .GetComponent<Entities>()
                .palette
                .danger;
        GetComponent<SpriteRenderer>().color = c;
        GetComponent<HealthController>().Init(1, c);

        agent = GetComponent<PolyNavAgent>();
        agent.OnDestinationReached += BeginAttack;
        playerTransform = GameObject.Find("Player").transform;


        // have to init an instant attack cooldown
        attackCooldownTimer = new Timer(0f);

    }

    void BeginAttack() {
        // setup attack cooldowns
        if (attackCooldownTimer.Check()) {
            recoveringFromAttack = true;
            attackRecoveryTimer = new Timer(attackRecovery);

            // the actual attacking part
            var tar = playerTransform.position;
            var dir = (tar - transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(dir * attackVelocity);
        }
    }

    Vector2 GetTarget() {
        // this is where we mess with enemy begunPathfinding.
        // if we have effects that change enemy begunPathfinding targets,
        // use code like below.
        // var ds = GameObject.FindGameObjectsWithTag("PlayerDrumstick")
        //          .Where( d => Vector2.Distance(d.transform.position, transform.position) < drumstickLureDistance)
        //          .OrderBy( d => Vector2.Distance(d.transform.position, transform.position)).ToArray();
        // return ds.Length == 0 ? targetTransform.position : ds[0].transform.position;

        // right now, we just chase the player.
        return playerTransform.position;
    }

    void CheckPathfindingActivation() {
        // enemies only activate at a certain distance from player and when not blocked
        var dist = Vector2.Distance(playerTransform.position, transform.position);
        if (dist < detectionDistance) {
            // but note that...
            // raycast blocks on other enemies, so watch line of sight during placement.
            // could not figure out layermasks. try later! (2016-05-23)
            // is this still true? (2016-07-23)
            var mask = LayerMask.GetMask("Geometry", "Player");
            var hit = Physics2D.Raycast(transform.position,
                                        playerTransform.position - transform.position,
                                        Mathf.Infinity, mask);
            if (hit.collider != null && hit.collider.tag == "Player") {
                begunPathfinding = true;
            }
        }
    }

    void Die() {
        var entities = GameObject.Find("Entities").GetComponent<Entities>();
        if (Random.value < 0.75f) {
            Instantiate(entities.coin, transform.position, Quaternion.identity);
        } else {
            Instantiate(entities.remains, transform.position, Quaternion.identity);
        }
        Object.Destroy(gameObject);
    }

    void Update() {
        if (playerTransform != null) {
            if (!begunPathfinding) {
                CheckPathfindingActivation();
                return;
            }
            if (!recoveringFromAttack) {
                // if activated && not recoveringFromAttack, update target and move toward it
                Pathfind();
                return;
            } else {
                StepAttackRecovery();
            }
        } else {
            playerTransform = GameObject.Find("Player").transform;
        }
    }


    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<HealthController>().ReceiveDamage(1);
        }
    }

    void Pathfind() {
        attackCooldownTimer.Tick(Time.deltaTime);
        var targetpos = GetTarget();
        agent.SetDestination(targetpos);
    }

    void StepAttackRecovery() {
        agent.Stop();
        if (attackRecoveryTimer.TickCheck(Time.deltaTime)) {
            recoveringFromAttack = false;
            attackCooldownTimer = new Timer(attackCooldown);
        }
    }

    public void TookDamage() {
        Camera.main.GetComponent<CameraShaker>().Shake(0.1f, 0.05f);
    }
}
