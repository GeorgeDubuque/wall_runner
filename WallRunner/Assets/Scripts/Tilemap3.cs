using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEngine;

[ExecuteInEditMode]
public class Tilemap3 : MonoBehaviour
{
    public GameObject tile;
    public enum modeEnum {
        Draw,
        Erase
    }
    public Vector3 snap;

    public modeEnum mode;

    public Collider coll;

    public Dictionary<Vector3, GameObject> tiles = new Dictionary<Vector3, GameObject>();
    
}
#endif