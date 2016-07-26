using UnityEngine;
using System.Collections;

public class IndividualSwordController : MonoBehaviour {

    const float thrustSpeed = 10f;

    const float alive = .25f;
    Timer aliveTimer;

    // Use this for initialization
    void Start () {
        aliveTimer = new Timer(alive);
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(Vector3.up * thrustSpeed * Time.deltaTime);
        aliveTimer.Tick(Time.deltaTime);
        if (aliveTimer.Check()) {
            Object.Destroy(gameObject);
        }
    }
}
