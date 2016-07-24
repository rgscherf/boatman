using UnityEngine;
using System.Collections;

public class DaggerController : MonoBehaviour {
    Transform player;

    float swingSpeed = -900f;

    const float alive = 0.13f;
    Timer aliveTimer;

    int damage = 1;

    public void Init(Transform playerTransform, Vector2 mouseClickPos) {
        player = playerTransform;
        transform.position = player.position;
        Vector3 mousePos = new Vector3(mouseClickPos.x, mouseClickPos.y, -1);
        Quaternion rot = Quaternion.LookRotation (mousePos - transform.position, transform.TransformDirection(Vector3.back));
        transform.rotation = new Quaternion(0, 0, rot.z, rot.w);
    }

    // Use this for initialization
    void Start () {
        aliveTimer = new Timer(alive);
    }

    // Update is called once per frame
    void Update () {
        transform.position = player.position;
        transform.Rotate(Vector3.forward, swingSpeed * Time.deltaTime);
        aliveTimer.Tick(Time.deltaTime);
        if (aliveTimer.Check()) {
            Object.Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            // do damage!
            var dir = player.position - other.transform.position;
            other.GetComponent<Rigidbody2D>().AddForce(dir.normalized * -400);
            other.GetComponent<HealthController>().ReceiveDamage(damage);
        }
    }
}
