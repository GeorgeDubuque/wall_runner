using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.SceneManagement;


[ExecuteInEditMode]
public class Tilemap3 : MonoBehaviour {
    public GameObject tile;
    public Vector3 snap;
    public Collider coll;
    public GameObject grid;
    public Dictionary<string, GameObject> tiles = new Dictionary<string, GameObject>();

    private void Awake ( ) {
        Debug.Log(gameObject.name + " Awake");
        InitializeTiles();
    }

    public void InitializeTiles ( ) {
        Debug.Log("Init Tiles");
        tiles = new Dictionary<string, GameObject>();
        for(int i = 0; i < grid.transform.childCount; i++) {
            GameObject currChild = grid.transform.GetChild(i).gameObject;
            string tileKey = currChild.transform.position.ToString();
            if (tiles.ContainsKey(tileKey)) {
                tiles.Remove(tileKey);
            }
            tiles.Add(tileKey, currChild);
        }
    }
}
#endif