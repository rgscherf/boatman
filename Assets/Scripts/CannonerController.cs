using UnityEngine;
using System.Collections;

public class CannonerController : MonoBehaviour {
    Vector3 spawnpos;
    int damage = 1;
    float velocity = 800f;

    const float alive = 1f;
    Timer aliveTimer;

    public void Init(Transform playerTransform, Vector2 mouseClickPos) {
        spawnpos = playerTransform.position;
        var dir = (mouseClickPos - (Vector2) spawnpos).normalized;
        GetComponent<Rigidbody2D>().AddForce(dir * velocity);

        aliveTimer = new Timer(alive);
    }


    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        if (aliveTimer.TickCheck(Time.deltaTime)) {
            Object.Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            var dir = spawnpos - other.transform.position;
            other.GetComponent<Rigidbody2D>().AddForce(dir.normalized * -100);
            other.GetComponent<HealthController>().ReceiveDamage(damage);
        }
    }
}
