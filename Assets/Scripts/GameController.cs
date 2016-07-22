using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour {

    public GameObject defaultMapTile;

    Entities entities;
    GameObject mapObjects;

    void MapInit() {

        // generate water tiles
        const int mapWidth = 40;
        const int mapHeight = 20;
        var xrange = Enumerable.Range(0, mapWidth);
        var yrange = Enumerable.Range(0, mapHeight);

        // foreach (var x in xrange) {
        //     foreach (var y in yrange) {
        //         var go = (GameObject) Instantiate(defaultMapTile, new Vector2(x, y), Quaternion.identity);
        //         go.transform.parent = mapObjects.transform;
        //     }
        // }


        // generate framing rocks
        foreach (var y in Enumerable.Range(0, 21)) {
            if (y == 15 || y == 5) { continue; }
            var go = (GameObject) Instantiate(entities.rockObject, new Vector2(40, y), Quaternion.identity);
            go.GetComponent<SpriteRenderer>().sprite = entities.rocks[Random.Range(0, entities.rocks.Length)];
            go.transform.parent = mapObjects.transform;
        }
        foreach (var y in Enumerable.Range(0, 21)) {
            if (y == 15 || y == 5) { continue; }
            var go = (GameObject) Instantiate(entities.rockObject, new Vector2(-1, y), Quaternion.identity);
            go.GetComponent<SpriteRenderer>().sprite = entities.rocks[Random.Range(0, entities.rocks.Length)];
            go.transform.parent = mapObjects.transform;
        }
        foreach (var x in Enumerable.Range(-1, 42)) {
            var go = (GameObject) Instantiate(entities.rockObject, new Vector2(x, -1), Quaternion.identity);
            go.GetComponent<SpriteRenderer>().sprite = entities.rocks[Random.Range(0, entities.rocks.Length)];
            go.transform.parent = mapObjects.transform;
        }
        foreach (var x in Enumerable.Range(-1, 42)) {
            var go = (GameObject) Instantiate(entities.rockObject, new Vector2(x, 20), Quaternion.identity);
            go.GetComponent<SpriteRenderer>().sprite = entities.rocks[Random.Range(0, entities.rocks.Length)];
            go.transform.parent = mapObjects.transform;
        }

    }

    // Use this for initialization
    void Start () {
        entities = GameObject.Find("Entities").GetComponent<Entities>();
        mapObjects = GameObject.Find("MapObjects");

        MapInit();

    }

    // Update is called once per frame
    void Update () {

    }
}
