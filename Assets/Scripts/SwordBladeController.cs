using UnityEngine;
using System.Collections;

public class SwordBladeController : MonoBehaviour {
    Transform player;
    const int damage = 3;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update () {
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            // do damage!
            var dir = player.position - other.transform.position;
            other.GetComponent<Rigidbody2D>().AddForce(dir.normalized * -800);
            other.GetComponent<HealthController>().ReceiveDamage(damage);
        }
    }
}
