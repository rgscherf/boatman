using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    Entities entities;
    GameObject mapObjects;

    Dictionary<int, ICargo> cargo;
    Dictionary<int, Timer> cargoFireCooldowns;


    void Update() {
        foreach (var item in cargoFireCooldowns) {
            item.Value.Tick(Time.deltaTime);
        }
    }

    // Use this for initialization
    void Start () {
        entities = GameObject.Find("Entities").GetComponent<Entities>();
        mapObjects = GameObject.Find("MapObjects");
        GameObject.Find("background").GetComponent<SpriteRenderer>().color = entities.palette.background;

        MapInit();

        sliderTop = GameObject.Find("sliderTop");
        sliderBottom = GameObject.Find("sliderBottom");
        sliderRight = GameObject.Find("sliderRight");
        sliderLeft = GameObject.Find("sliderLeft");

        cargo = new Dictionary<int, ICargo>();
        cargoFireCooldowns = new Dictionary<int, Timer>();

        AddCargo(new CargoDagger());
        AddCargo(new CargoFood());
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
            // throws exception when First() cannot return a value
            return false;
        }
    }

    //////////////////
    // PORT ACTIVATION
    //////////////////
    GameObject sliderTop;
    GameObject sliderBottom;
    GameObject sliderRight;
    GameObject sliderLeft;
    // slider positions:

    //// top activation
    // bot start    19.9, -35.7
    // bot end      19.9, -9.7
    // right start  65.4, -1.7
    // right end    31.9, -1.7

    //// bottom activation
    // top start    19.9, 31.7
    // top end      19.9, 13.4
    // left start   -26.2, 4.6
    // left end     11.5, 4.6
    // bot start    19.9, -25.9

    float slideTime = 1f;
    string activePort;

    public void ActivatePort(string port) {
        activePort = port;
        if (port == "top") {
            TweenHelper(sliderBottom, new Vector3(19.9f, -9.7f, -5f));
            TweenHelper(sliderRight, new Vector3(31.9f, -1.7f, -5f));
        } else {
            TweenHelper(sliderTop, new Vector3(19.9f, 13.4f, -5f));
            TweenHelper(sliderLeft, new Vector3(11.5f, 4.6f, -5f));
            TweenHelper(sliderBottom, new Vector3(19.9f, -25.9f, -5f));
        }
        Invoke("DestroyObjects", slideTime);
        Invoke("ActivatePortUI", slideTime);
    }

    void ActivatePortUI() {

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

    public void DeactivatePort() {
    }

    void TweenHelper(GameObject obj, Vector3 vec) {
        LeanTween.move(obj, vec, slideTime).setEase(LeanTweenType.easeInQuad);

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
            cargo.Remove(index);
            cargoFireCooldowns.Remove(index);
            GameObject.Find("Cargo").GetComponent<CargoController>().Repaint(cargo);
        } else {
            cargoFireCooldowns[index].Reset();
        }
    }
}
