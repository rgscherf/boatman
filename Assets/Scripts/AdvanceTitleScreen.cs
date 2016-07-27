using UnityEngine;

public class AdvanceTitleScreen : MonoBehaviour {

    bool activated;

    void OnTriggerEnter2D(Collider2D other) {
        if (!activated && other.gameObject.name == "Player") {
            GameObject.Find("GameController").GetComponent<GameController>().AdvanceTutorial();
            activated = true;
        }
    }
}
