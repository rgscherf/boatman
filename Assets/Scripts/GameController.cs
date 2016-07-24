using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    Entities entities;
    GameObject mapObjects;
    GameObject merchantUI;
    ShopController shop;
    PlayerController player;
    Maps maps;

    Dictionary<int, ICargo> cargo;
    Dictionary<int, Timer> cargoFireCooldowns;


    void Update() {
        foreach (var item in cargoFireCooldowns) {
            item.Value.Tick(Time.deltaTime);
        }
    }

    // Use this for initialization
    void Awake() {
        merchantUI = GameObject.Find("MerchantUI");
        shop = GameObject.Find("Shop").GetComponent<ShopController>();
        merchantUI.SetActive(false);
        maps = GetComponent<Maps>();
    }

    void Start () {
        entities = GameObject.Find("Entities").GetComponent<Entities>();
        mapObjects = GameObject.Find("MapObjects");
        GameObject.Find("background").GetComponent<SpriteRenderer>().color = entities.palette.background;
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        MapInit();

        sliderTop = GameObject.Find("sliderTop");
        sliderBottom = GameObject.Find("sliderBottom");
        sliderRight = GameObject.Find("sliderRight");
        sliderLeft = GameObject.Find("sliderLeft");

        cargo = new Dictionary<int, ICargo>();
        cargoFireCooldowns = new Dictionary<int, Timer>();

        AddCargo(new CargoDagger());
        AddCargo(new CargoFood());
        PlaceObjects();
    }

    void PlaceObjects() {
        var map = Maps.RandomMap();
        maps.Reify(map, entities);
    }

    bool CanAddCargo() {
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

    bool AddCargo(ICargo item) {
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

    ICargo[] MakeShopInventory(string port) {
        ICargo[] basicCargo = {
            new CargoDagger()
        };
        ICargo[] oldTownCargo = {
            new CargoSword(),
            new CargoFood(),
        };
        ICargo[] ratTownCargo = {
            new CargoDagger(),
            new CargoRock(),
        };
        ICargo[] res = port == "top"
                       ? basicCargo.Concat(oldTownCargo).ToArray()
                       : basicCargo.Concat(ratTownCargo).ToArray();
        const int numItems = 3;
        var fin = new List<ICargo>();
        for (var i = 0; i < numItems; i++) {
            var index = Random.Range(0, res.Length);
            fin.Add(res[index]);
        }
        return fin.ToArray();
    }

    //////////////////
    // PORT ACTIVATION
    //////////////////
    GameObject sliderTop;
    GameObject sliderBottom;
    GameObject sliderRight;
    GameObject sliderLeft;

    float slideTime = 0.5f;
    string activePort;
    public bool inPort;

    public void ActivatePort(string port) {
        activePort = port;
        inPort = true;
        if (port == "top") {
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
        var welcome = activePort == "top"
                      ? oldTownWelcome[Random.Range(0, oldTownWelcome.Length)]
                      : ratTownWelcome[Random.Range(0, ratTownWelcome.Length)];



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

    public void DeactivatePort(string port) {
        if (!inPort) { return; }
        inPort = false;
        if (port == "top") {
            TweenHelper(sliderBottom, new Vector3(19.9f, -35.7f, -5f));
            TweenHelper(sliderRight, new Vector3(65.4f, -1.7f, -5f));
        } else {
            TweenHelper(sliderTop, new Vector3(19.9f, 32f, -5f));
            TweenHelper(sliderLeft, new Vector3(-26.2f, 4.6f, -5f));
            TweenHelper(sliderBottom, new Vector3(19.9f, -35.7f, -5f));
        }
        merchantUI.SetActive(false);
        PlaceObjects();
    }

    void TweenHelper(GameObject obj, Vector3 vec) {
        LeanTween.move(obj, vec, slideTime).setEase(LeanTweenType.easeInQuad);
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
        var price = cargo[holdslot].price;
        switch (cargo[holdslot].cargoType) {
            case CargoType.Food:
                if (activePort == "bottom") {
                    player.ReceiveBooty(price);
                    RemoveCargo(holdslot);
                }
                break;
            case CargoType.Rock:
                if (activePort == "top") {
                    player.ReceiveBooty(price);
                    RemoveCargo(holdslot);
                }
                break;
            case CargoType.Dagger:
                player.ReceiveBooty(price);
                RemoveCargo(holdslot);
                break;
            case CargoType.Sword:
                player.ReceiveBooty(price);
                RemoveCargo(holdslot);
                break;
        }
    }

    public void ReceiveMerchantClick(int shopSlot) {
        Debug.Log(string.Format("Received click at {0}", shopSlot));
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

    void MapInit() {
        SetupStaticMapObjects();
        foreach (var y in Enumerable.Range(4, 16)) {
            MakeRock(40, y);
        }
        foreach (var y in Enumerable.Range(0, 16)) {
            MakeRock(-1, y);
        }
        foreach (var x in Enumerable.Range(-1, 37)) {
            MakeRock(x, -1);
        }
        foreach (var x in Enumerable.Range(5, 36)) {
            MakeRock(x, 20);
        }
    }

    void SetupStaticMapObjects() {
        var targetColor = entities.palette.player;
        var staticObjects = new[] {"dock-topleft", "dock-bottomright"};

        foreach (var s in staticObjects) {
            var d = GameObject.Find(s);
            for (int i = 0; i < d.transform.childCount; i++) {
                var childSpr = d.transform.GetChild(i).GetComponent<SpriteRenderer>();
                if (childSpr) {
                    childSpr.color = targetColor;
                }
            }
        }
    }

    void MakeRock(int x, int y) {
        var go = (GameObject) Instantiate(entities.rockObject, new Vector2(x, y), Quaternion.identity);
        go.GetComponent<SpriteRenderer>().sprite = entities.rocks[Random.Range(0, entities.rocks.Length)];
        go.GetComponent<SpriteRenderer>().color = entities.palette.geometry;
        go.transform.parent = mapObjects.transform;
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
    public void FireCleanup(int index) {
        if (cargo[index].cargoType == CargoType.Food) {
            RemoveCargo(index);
        } else {
            cargoFireCooldowns[index].Reset();
        }
    }





}
