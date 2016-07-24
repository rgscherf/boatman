using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {

    Timer aliveTimer;
    const float alive = 4f;

    void Start() {
        aliveTimer = new Timer(alive);
    }

    void Update() {
        if (aliveTimer.TickCheck(Time.deltaTime)) {
            Object.Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<PlayerController>().ReceiveBooty(1);
            Object.Destroy(gameObject);
        }
    }

}
