using UnityEngine;
using System.Collections;

public class RemainsController : MonoBehaviour {
    Timer aliveTimer;
    const float alive = 4f;

    GameController gameController;


    void Start() {
        aliveTimer = new Timer(alive);
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update() {
        if (aliveTimer.TickCheck(Time.deltaTime)) {
            Object.Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (gameController.CanAddCargo()) {
                gameController.AddCargo(new CargoRemains());
                Object.Destroy(gameObject);
            }
        }
    }

}
