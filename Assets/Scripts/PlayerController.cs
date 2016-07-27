using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Entities entities;

    int booty = 0;

    Rigidbody2D rigid;
    SpriteRenderer spr;
    CargoController cargoController;
    GameController gameController;
    HullController hullController;
    BootyController bootyController;

    // for paddle-based movement
    bool moveMouseDownPreviousFrame;
    GameObject paddleLine;
    Vector3 paddleStart;
    const float maxDistance = 7f;
    const float paddleForceMod = 10f;
    const float maxSpeed = 10f;

    // Use this for initialization
    void Awake () {
        entities = GameObject.Find("Entities").GetComponent<Entities>();
        cargoController = GameObject.Find("Cargo").GetComponent<CargoController>();
        hullController = GameObject.Find("Hull").GetComponent<HullController>();
        bootyController = GameObject.Find("Booty").GetComponent<BootyController>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start() {
        spr = GetComponent<SpriteRenderer>();
        spr.color = entities.palette.player;
        GetComponent<HealthController>().Init(6, spr.color);
    }

    void Update () {
        spr.flipX = rigid.velocity.x < 0;
        CheckMovement();
        CheckCargoSelection();
        CheckFire();
    }


    ///////////////
    // PRIMARY FIRE
    ///////////////

    void CheckFire() {
        if (Input.GetAxisRaw("Fire1") == 1 && !gameController.inPort) {
            var current = cargoController.currentSelection;
            if (current == 0) { return; }

            if (gameController.SelectedCanFire(current)) {
                // special handling for cannoner
                if (gameController.SelectedFireType(current) == CargoType.Cannoner) {
                    if (!CanDebitBooty(2)) { return; }
                    DebitBooty(2);
                }
                var fireObject = gameController.SelectedFireObject(current);
                var go = (GameObject) Instantiate(
                             fireObject,
                             transform.position,
                             Quaternion.identity);
                go.GetComponent<FireObjectController>().Init(
                    gameController.SelectedFireType(current),
                    transform,
                    (Vector2) MousePos());
                gameController.FireCleanup(current);
            }
        }
    }

    //////////////////////////
    // SPECIAL DAMAGE HANDLING
    //////////////////////////

    public void UpdateHealth(int currentHealth) {
        hullController.Repaint(currentHealth);
    }

    public void TookDamage() {
        Camera.main.GetComponent<CameraShaker>().Shake(0.5f, 0.25f);
        gameController.BreakCeramics();
    }

    public void Die() {
        gameObject.SetActive(false);
    }


    ///////////////////
    // BOOTY OPERATIONS
    ///////////////////

    public bool CanDebitBooty(int amt) {
        return booty >= amt;
    }
    public bool DebitBooty(int amt) {
        if (booty >= amt) {
            booty -= amt;
            bootyController.Repaint(booty);
            return true;
        }
        return false;
    }

    public void ReceiveBooty(int amt) {
        booty += amt;
        bootyController.Repaint(booty);
    }

    //////////////////////
    // INVENTORY SELECTION
    //////////////////////

    void CheckCargoSelection() {
        if (Input.GetAxisRaw("Select1") == 1) {
            cargoController.ChangeSelection(1);
        } else if (Input.GetAxisRaw("Select2") == 1) {
            cargoController.ChangeSelection(2);
        } else if (Input.GetAxisRaw("Select3") == 1) {
            cargoController.ChangeSelection(3);
        } else if (Input.GetAxisRaw("Select4") == 1) {
            cargoController.ChangeSelection(4);
        }
    }

    /////////////////
    // MOVEMENT STUFF
    /////////////////

    Vector3 MousePos() {
        var mousePos = Input.mousePosition;
        var worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        worldMousePos.z = -1f;
        return worldMousePos;
    }

    void CheckMovement() {
        var moveMouseDownThisFrame = Input.GetMouseButton(1);

        var worldMousePos = MousePos();

        if (moveMouseDownThisFrame) {
            if (!moveMouseDownPreviousFrame) {
                // start paddle action
                InitPaddle(worldMousePos);
            } else {
                // continue paddle action
                UpdatePaddle(worldMousePos);
            }
        } else {
            // we are not paddling this frame
            if (moveMouseDownPreviousFrame) {
                // end paddle action
                ReleasePaddle(worldMousePos);
            }
        }
    }

    void InitPaddle(Vector3 startpos) {
        moveMouseDownPreviousFrame = true;

        paddleLine = (GameObject) Instantiate(entities.paddleLine, transform.position, Quaternion.identity);
        var lr = paddleLine.GetComponent<LineRenderer>();
        lr.SetColors(entities.palette.UI, entities.palette.UI);
        lr.SetWidth(0.25f, 0.25f);

        startpos.z = -9;
        paddleStart = startpos;
        paddleLine.GetComponent<LineRenderer>().SetPositions(new[] {paddleStart, paddleStart});
    }

    void ReleasePaddle(Vector3 mousepos) {
        var dist = Mathf.Max(maxDistance, Vector3.Distance(paddleStart, mousepos));
        var dir = Vector3.ClampMagnitude(paddleStart - mousepos, dist);
        rigid.AddForce(dir * paddleForceMod);
        rigid.velocity = Vector2.ClampMagnitude(rigid.velocity, maxSpeed);

        Object.Destroy(paddleLine);
        moveMouseDownPreviousFrame = false;
    }

    void UpdatePaddle(Vector3 mousepos) {
        var lr = paddleLine.GetComponent<LineRenderer>();
        var dist = Vector3.Distance(paddleStart, mousepos);
        mousepos.z = -9;
        lr.SetPosition(1, mousepos);
        lr.SetWidth(0f, dist * 0.15f);
    }
}
