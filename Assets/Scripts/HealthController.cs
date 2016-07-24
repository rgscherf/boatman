using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {
    Entities entities;

    public int currentHealth;
    public int maxHealth;

    Timer invulnTimer;
    GameObject go;

    Color baseColor;

    void Awake() {
        invulnTimer = new Timer(0f);
        flickerTimer = new Timer(0f);
    }
    void Start() {
        go = gameObject;
        entities = GameObject.Find("Entities").GetComponent<Entities>();
    }

    public void Init(int hp, Color truecolor, float invuln = 0.8f) {
        baseColor = truecolor;
        maxHealth = currentHealth = hp;
        invulnTimer = new Timer(invuln, true);
    }

    void Update() {
        invulnTimer.Tick(Time.deltaTime);
        FlickerUpdate();
    }

    public void ReceiveDamage(int debitamount) {
        if (invulnTimer.Check()) {
            currentHealth -= debitamount;
            invulnTimer.Reset();
            Flicker(invulnTimer.Cooldown());
            if (gameObject.tag == "Player") {
                gameObject.GetComponent<PlayerController>().UpdateHealth(currentHealth);
            }
        }
        if (currentHealth <= 0) {
            SendMessage("Die");
        }
    }

    /////////////
    // FLICKERING
    // sprites flicker after taking damage (and maybe for other reasons??)
    /////////////

    // flicker-related fields
    int framesSinceLastChange;
    bool rotationOnBase;
    bool flickering;
    Timer flickerTimer;

    public void Flicker(float t) {
        if (!flickering) {
            flickering = true;
            framesSinceLastChange = 0;
            rotationOnBase = true;
            flickerTimer = new Timer(t);
        }
    }

    void FlickerUpdate() {
        flickerTimer.Tick(Time.deltaTime);
        if (flickering) {
            // we want to rotate colors every 2 frames
            // rather than 1, for a mellower flicker effect
            framesSinceLastChange++;
            if (framesSinceLastChange == 2) {
                framesSinceLastChange = 0;
                rotationOnBase = !rotationOnBase;
                GetComponent<SpriteRenderer>().color = rotationOnBase ? baseColor : entities.palette.background;
            }
            if (flickerTimer.Check()) {
                FlickerUnload();
            }
        }
    }

    void FlickerUnload() {
        flickering = false;
        GetComponent<SpriteRenderer>().color = baseColor;
    }

}
