using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    Entities entities;
    GameObject mapObjects;
    GameObject merchantUI;
    ShopController shop;
    public GameObject playerObject;
    PlayerController player;
    Maps maps;
    GameObject mainUI;

    Dictionary<int, ICargo> cargo;
    Dictionary<int, Timer> cargoFireCooldowns;


    bool onTutorialMap = true;

    void Update() {
        if (cargoFireCooldowns != null) {
            foreach (var item in cargoFireCooldowns) {
                item.Value.Tick(Time.deltaTime);
            }
        }
        if (Input.GetButtonDown("Restart")) {
            Restart();
        }
    }

    // Use this for initialization
    void Awake() {
        // init UI
        // these will be deactivated immediately so we need the references now
        merchantUI = GameObject.Find("MerchantUI");
        shop = GameObject.Find("Shop").GetComponent<ShopController>();
        merchantUI.SetActive(false);
        mainUI = GameObject.Find("MainUIPanel");
        mainUI.SetActive(false);
        maps = GetComponent<Maps>();

        entities = GameObject.Find("Entities").GetComponent<Entities>();
        GameObject.Find("background").GetComponent<SpriteRenderer>().color = entities.palette.background;
        mapObjects = GameObject.Find("MapObjects");

        // begin tutorial sequence.
    }

    void Start () {
        StaticMapObjectsInit();
        PlayerDataInit();
        StageTutorial();
    }

    void PlayerDataInit() {
        mainUI.SetActive(true);
        var pgo = (GameObject) Instantiate(playerObject, new Vector3(3, 15, -1), Quaternion.identity);
        pgo.name = "Player";
        player = pgo.GetComponent<PlayerController>();
        cargo = new Dictionary<int, ICargo>();
        cargoFireCooldowns = new Dictionary<int, Timer>();

        // INITIAL CARGO LOADOUT

        AddCargo(new CargoDagger());
        AddCargo(new CargoFood());

        everVisitedPort = false;
        inPort = false;
    }

    public void Restart() {
        if (inPort) {
            // PlayerDataInit() clears inPort, so we need this check at the top
            ClearPortObjects(activePort);
        }
        StaticMapObjectsInit();
        MapCleanup();
        PlayerDataInit();
        onTutorialMap = false;
        player.transform.position = new Vector3(2.5f, 15f, -1f);
        Camera.main.GetComponent<CameraPositionScripter>().CompleteTutorial();

        // purely to recolor dock prefabs.
        // has unintentional side-effect of reassigning slider objects.
        SpawnDynamicMapObjects();
    }

    void MapCleanup() {
        DestroyObjects();
        if (player.gameObject != null) {
            Object.Destroy(player.gameObject);
        }
    }

    /////////////////////
    // TUTORIAL SCRIPTING
    /////////////////////

    void StageTutorial() {
        // var cam = Camera.main;
        // cam.transform.position = cam.GetComponent<CameraPositionScripter>().titlePosition;
        player.gameObject.transform.position = new Vector3(-112f, -106f, -1f);
        player.gameObject.transform.localScale = new Vector3(4f, 4f, 1f);
        mainUI.SetActive(false);
    }

    void TutorialPlayerPosition() {
        player.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        player.gameObject.transform.position = new Vector3(-114f, -144f, -1f);
        player.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
    }

    void ActivateMainUI() {
        mainUI.SetActive(true);
    }

    Vector3 transportedPlayerPos;

    public void AdvanceTutorial() {
        Invoke("TutorialPlayerPosition", 1f);
        Invoke("ActivateMainUI", 1.5f);
        Camera.main.GetComponent<CameraPositionScripter>().AdvanceTutorial();
    }

    public void CompleteTutorial() {
        TweenHelper(GameObject.Find("sliderBottomDummy"), new Vector3(-119.5f + 18.6f, -149.89f + -9.2f, -5f));
        TweenHelper(GameObject.Find("sliderRightDummy"), new Vector3(-119.5f + 29.2f, -149.89f + 0.2f, -5f));
        Vector3 relativePos = player.gameObject.transform.position - Camera.main.gameObject.transform.position;
        Camera.main.gameObject.GetComponent<CameraPositionScripter>().CompleteTutorial();
        var newPos = Camera.main.gameObject.transform.position + relativePos;
        newPos.z = -1f;
        player.gameObject.transform.position = newPos;
    }

    void SpawnDynamicMapObjects() {
        var map = Maps.RandomMap();
        maps.Reify(map, entities);
    }

    public bool CanAddCargo() {
        // nondestructive check of cargo
        // for testing buy transactions
        try {
            int slot = new[] {1, 2, 3, 4}
            .Where(i => !cargo.ContainsKey(i))
            .First();
            return true;
        } catch {
            // throws exception when first() cannot return a value
            return false;
        }
    }

    public bool AddCargo(ICargo item) {
        // try to add cargo to hold.
        // returns true if cargo added, false otherwise
        try {
            int slot = new[] {1, 2, 3, 4}
            .Where(i => !cargo.ContainsKey(i))
            .First();
            cargo[slot] = item;
            cargoFireCooldowns[slot] = new Timer(item.cargoFireTimer, true);
            GameObject.Find("Cargo").GetComponent<CargoController>().Repaint(cargo);
            return true;
        } catch {
            // throws exception when first() cannot return a value
            return false;
        }
    }

    ///////////////////
    // MERCHANT CONFIGS
    ///////////////////

    ICargo[] MakeShopInventory(Port port) {
        // 'useful cargo': cargo with effects
        ICargo[] basicCargoUseful = {
            new CargoDagger(),
            new CargoEmpty(),
        };
        ICargo[] oldTownCargoUseful = {
            new CargoSword(),
            new CargoHealing(),
        };
        ICargo[] ratTownCargoUseful = {
            new CargoHealing(),
            new CargoCannoner(),
        };

        // 'econ cargo': cargo to sell
        ICargo[] basicCargoEcon = {
        };
        ICargo[] oldTownCargoEcon = {
            new CargoFood(),
        };
        ICargo[] ratTownCargoEcon = {
            new CargoRock(),
        };

        // merchant inventory generation
        // first two slots are always 'useful cargo'
        // final slot is always economic.
        ICargo[] usefulCargo = port == Port.Top
                               ? basicCargoUseful.Concat(oldTownCargoUseful).ToArray()
                               : basicCargoUseful.Concat(ratTownCargoUseful).ToArray();
        ICargo[] econCargo = port == Port.Top
                             ? basicCargoEcon.Concat(oldTownCargoEcon).ToArray()
                             : basicCargoEcon.Concat(ratTownCargoEcon).ToArray();
        var fin = new List<ICargo>();
        fin.Add(usefulCargo[Random.Range(0, usefulCargo.Length)]);
        fin.Add(usefulCargo[Random.Range(0, usefulCargo.Length)]);
        fin.Add(econCargo[Random.Range(0, econCargo.Length)]);

        return fin.ToArray();
    }

    //////////////////
    // PORT ACTIVATION
    //////////////////
    GameObject sliderTop;
    GameObject sliderBottom;
    GameObject sliderRight;
    GameObject sliderLeft;

    const float slideTime = 0.5f;
    Port activePort;
    bool everVisitedPort;
    public bool inPort;

    public void ActivatePort(Port port) {
        if (onTutorialMap) {
            CompleteTutorial();
            onTutorialMap = false;
        }
        if (everVisitedPort && port == activePort) { return; }
        if (!everVisitedPort) { everVisitedPort = true; }
        activePort = port;
        inPort = true;
        if (port == Port.Top) {
            TweenHelper(sliderBottom, new Vector3(19.9f, -9.7f, -5f));
            TweenHelper(sliderRight, new Vector3(31.9f, -1.7f, -5f));
        } else {
            TweenHelper(sliderTop, new Vector3(19.9f, 14.54f, -5f));
            TweenHelper(sliderLeft, new Vector3(8.83f, 4.6f, -5f));
            TweenHelper(sliderBottom, new Vector3(19.9f, -25.6f, -5f));
        }
        Invoke("DestroyObjects", slideTime);
        Invoke("ActivatePortUI", slideTime);
        GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
    }


    void ActivatePortUI() {
        ColorDockObject(entities.palette.player, "dock-topleft");
        ColorDockObject(entities.palette.player, "dock-bottomright");

        // greeting message
        var oldTownWelcome = new[] {"OLD TOWN WELCOMES YOU.",
                                    "A FINE DAY IN OLD TOWN.",
                                    "STOP BY THE TULIP FESTIVAL!",
                                    "THE KING WISHES TO BUY EXOTIC FURS."
                                   };
        var ratTownWelcome = new[] {"WATCH YERSELF IN RAT TOWN.",
                                    "THIS IS RAT TOWN, STRANGER.",
                                    "WATCH YOUR BACK.",
                                    "THERE ARE BAD THINGS IN THE FOREST."
                                   };

        var welcome = activePort == Port.Top
                      ? oldTownWelcome[Random.Range(0, oldTownWelcome.Length)]
                      : ratTownWelcome[Random.Range(0, ratTownWelcome.Length)];
        welcome = !onTutorialMap ? welcome : "WELCOME TO OLD TOWN. YOU'RE ON YOUR OWN FROM HERE.";


        merchantUI.SetActive(true);
        var inventory = MakeShopInventory(activePort);
        shop.Restock(inventory);
        GameObject.Find("BuyWelcome").GetComponent<Text>().text = welcome;
    }

    void DestroyObjects() {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var e in enemies) {
            Object.Destroy(e);
        }
        var geog = GameObject.FindGameObjectsWithTag("Geometry");
        foreach (var g in geog) {
            Object.Destroy(g);
        }
    }

    public void ClearPortObjects(Port port) {
        if (!inPort) { return; }
        inPort = false;
        if (port == Port.Top) {
            TweenHelper(sliderBottom, new Vector3(19.9f, -35.7f, -5f));
            TweenHelper(sliderRight, new Vector3(65.4f, -1.7f, -5f));
            ColorDockObject(entities.palette.geometry, "dock-topleft");
        } else {
            TweenHelper(sliderTop, new Vector3(19.9f, 32f, -5f));
            TweenHelper(sliderLeft, new Vector3(-26.2f, 4.6f, -5f));
            TweenHelper(sliderBottom, new Vector3(19.9f, -35.7f, -5f));
            ColorDockObject(entities.palette.geometry, "dock-bottomright");
        }
        merchantUI.SetActive(false);
    }

    public void DeactivatePort(Port port) {
        ClearPortObjects(port);
        SpawnDynamicMapObjects();
    }

    public void TweenHelper(GameObject obj, Vector3 vec, float slide = slideTime) {
        LeanTween.move(obj, vec, slide).setEase(LeanTweenType.easeInQuad);
    }

    ////////////////////
    // FIRING OPERATIONS
    ////////////////////

    public void FireCleanup(int index) {
        switch (cargo[index].cargoType) {
            case CargoType.Food:
                RemoveCargo(index);
                break;
            case CargoType.Healing:
                RemoveCargo(index);
                break;
            default:
                cargoFireCooldowns[index].Reset();
                break;
        }
    }

    public GameObject SelectedFireObject(int index) {
        return cargo[index].cargoFireObject;
    }
    public CargoType SelectedFireType(int index) {
        return cargo[index].cargoType;
    }
    public bool SelectedCanFire(int index) {
        return cargoFireCooldowns.ContainsKey(index) ? cargoFireCooldowns[index].Check() : false;
    }


    //////////////////////
    // MERCHANT OPERATIONS
    //////////////////////
    void RemoveCargo(int slot) {
        if (cargo.ContainsKey(slot)) {
            cargo.Remove(slot);
            cargoFireCooldowns.Remove(slot);
            GameObject.Find("Cargo").GetComponent<CargoController>().Repaint(cargo);
        }
    }

    public void ReceiveCargoClick(int holdslot) {
        if (!inPort || !cargo.ContainsKey(holdslot)) { return; }
        var sellprice = cargo[holdslot].sellprice;
        switch (cargo[holdslot].cargoType) {
            case CargoType.Rock:
                if (activePort == Port.Top) {
                    player.ReceiveBooty(sellprice);
                    RemoveCargo(holdslot);
                }
                break;
            case CargoType.Food:
                if (activePort == Port.Bottom) {
                    player.ReceiveBooty(sellprice);
                    RemoveCargo(holdslot);
                }
                break;
            case CargoType.Sword:
                if (activePort == Port.Bottom) {
                    player.ReceiveBooty(sellprice);
                    RemoveCargo(holdslot);
                }
                break;
            default:
                player.ReceiveBooty(sellprice);
                RemoveCargo(holdslot);
                break;
        }
    }

    public void ReceiveMerchantClick(int shopSlot) {
        if (!inPort || !shop.ContainsKey(shopSlot)) { return; }
        int price = shop.GetPrice(shopSlot);
        if (player.CanDebitBooty(price) && CanAddCargo()) {
            player.DebitBooty(price);
            var newItem = shop.GetItem(shopSlot);
            AddCargo(newItem);
        }
    }


    /////////////
    // INIT STUFF
    /////////////

    void StaticMapObjectsInit() {
        sliderTop = GameObject.Find("sliderTop");
        sliderBottom = GameObject.Find("sliderBottom");
        sliderRight = GameObject.Find("sliderRight");
        sliderLeft = GameObject.Find("sliderLeft");

        ColorDockObject(entities.palette.player, "dock-topleft");
        ColorDockObject(entities.palette.player, "dock-bottomright");
    }

    void ColorDockObject(Color targetColor, string targetObject) {
        var d = GameObject.Find(targetObject);
        for (int i = 0; i < d.transform.childCount; i++) {
            var childSpr = d.transform.GetChild(i).GetComponent<SpriteRenderer>();
            if (childSpr) {
                childSpr.color = targetColor;
            }
        }
    }

    void MakeRock(int x, int y) {
        var go = (GameObject) Instantiate(entities.rockObject, new Vector2(x, y), Quaternion.identity);
        go.GetComponent<SpriteRenderer>().sprite = entities.rocks[Random.Range(0, entities.rocks.Length)];
        go.GetComponent<SpriteRenderer>().color = entities.palette.geometry;
        go.transform.parent = mapObjects.transform;
    }
}
