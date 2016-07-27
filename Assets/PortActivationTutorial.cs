using UnityEngine;
using System.Collections;

public class PortActivationTutorial : MonoBehaviour {
    GameController gameController;

    void Start() {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            gameController.ActivatePort(Port.Top);
        }
    }
}
