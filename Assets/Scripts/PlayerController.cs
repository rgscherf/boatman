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

    public GameObject rangeFinderObject;
    GameObject rangeFinder;

    // for paddle-based movement
    bool moveMouseDownPreviousFrame;
    GameObject paddleLine;
    Vector3 paddleStart;
    const float maxDistance = 5f;
    const float paddleForceMod = 60f;
    const float maxSpeed = 8f;

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

    void FixedUpdate() {
        if (shouldRelease) {
            rigid.AddForce(releaseVec);
            shouldRelease = false;
        }
        rigid.velocity = Vector2.ClampMagnitude(rigid.velocity, maxSpeed);
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
        GameObject.Find("restart-wipe").GetComponent<TitleBackgroundController>().TweenInRestart();
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
        var col = entities.palette.UI * new Color(1f, 1f, 1f, 0.75f);

        rangeFinder = (GameObject) Instantiate(rangeFinderObject,
                                               new Vector3(startpos.x, startpos.y, -9f),
                                               Quaternion.identity);
        rangeFinder.transform.localScale = new Vector3(maxDistance * 2, maxDistance * 2, 1f);

        paddleLine = (GameObject) Instantiate(entities.paddleLine, transform.position, Quaternion.identity);
        var lr = paddleLine.GetComponent<LineRenderer>();
        lr.SetColors(col, col);
        lr.SetWidth(0.25f, 0.25f);

        startpos.z = -9;
        paddleStart = startpos;
        paddleLine.GetComponent<LineRenderer>().SetPositions(new[] {paddleStart, paddleStart});
    }

    // addfoce-ing happens in FixedUpdate
    // based on these flags
    bool shouldRelease;
    Vector3 releaseVec;
    void ReleasePaddle(Vector3 mousepos) {
        var dir = Vector3.ClampMagnitude(paddleStart - mousepos, maxDistance);
        shouldRelease = true;
        releaseVec = dir * paddleForceMod;

        Object.Destroy(paddleLine);
        Object.Destroy(rangeFinder);
        moveMouseDownPreviousFrame = false;
    }

    void UpdatePaddle(Vector3 mousepos) {
        var adjmouse = new Vector3(mousepos.x, mousepos.y, -9f);
        var dist = Mathf.Min(maxDistance, Vector3.Distance(paddleStart, adjmouse));
        var pointDir = adjmouse - paddleStart;

        pointDir = Vector3.ClampMagnitude(pointDir, maxDistance * 0.98f);
        pointDir += paddleStart;
        pointDir.z = -9f;

        var lr = paddleLine.GetComponent<LineRenderer>();
        lr.SetPosition(1, pointDir);
        lr.SetWidth(0f, dist * 0.15f);
    }
}
