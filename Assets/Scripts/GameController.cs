using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    Entities entities;
    GameObject mapObjects;

    Dictionary<int, ICargo> cargo;

    // Use this for initialization
    void Start () {
        entities = GameObject.Find("Entities").GetComponent<Entities>();
        mapObjects = GameObject.Find("MapObjects");
        GameObject.Find("background").GetComponent<SpriteRenderer>().color = entities.palette.background;

        MapInit();

        cargo = new Dictionary<int, ICargo>();
        AddCargo(new CargoFood());
        AddCargo(new CargoDagger());
    }

    bool AddCargo(ICargo item) {
        // try to add cargo to hold.
        // returns true if cargo added, false otherwise
        try {
            int slot = new[] {1, 2, 3, 4}
            .Where(i => !cargo.ContainsKey(i))
            .First();
            cargo[slot] = item;
            GameObject.Find("Cargo").GetComponent<CargoController>().Repaint(cargo);
            return true;
        } catch {
            // throws exception when First() cannot return a value
            return false;
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
}
