using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour {

    Entities entities;
    GameObject mapObjects;

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

    void MapInit() {
        SetupStaticMapObjects();

        // generate water tiles
        // const int mapWidth = 40;
        // const int mapHeight = 20;

        // generate framing rocks
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

    // Use this for initialization
    void Start () {
        entities = GameObject.Find("Entities").GetComponent<Entities>();
        mapObjects = GameObject.Find("MapObjects");
        GameObject.Find("background").GetComponent<SpriteRenderer>().color = entities.palette.background;

        MapInit();

    }

    // Update is called once per frame
    void Update () {

    }
}
