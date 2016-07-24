using UnityEngine;
using System.Collections;

public class PortDeactivation : MonoBehaviour {
    GameController gameController;
    GameObject castle;
    GameObject rat;

    void Start() {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        castle = GameObject.Find("Castle");
        rat = GameObject.Find("Rat");

    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            var distFromTop = Vector3.Distance(transform.position, castle.transform.position);
            var distFromBottom = Vector3.Distance(transform.position, rat.transform.position);
            var flag = distFromTop < distFromBottom ? "top" : "bottom";
            gameController.DeactivatePort(flag);
        }
    }
}
