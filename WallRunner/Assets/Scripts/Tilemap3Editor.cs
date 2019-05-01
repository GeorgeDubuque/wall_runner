using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Tilemap3))]
public class Tilemap3Editor : Editor
{
    Tilemap3 tilemap;
    GameObject tile;
    Tilemap3.modeEnum mode;
    int hashCode;
    Collider coll;

    private void OnEnable ( ) {
        hashCode = GetHashCode();

        tilemap = (Tilemap3)target;
        tile = tilemap.tile;
        mode = tilemap.mode;
        coll = tilemap.coll;
    }

    public override void OnInspectorGUI ( ) {
        base.OnInspectorGUI();

        hashCode = GetHashCode();
        tilemap = (Tilemap3)target;
        tile = tilemap.tile;
        mode = tilemap.mode;
        coll = tilemap.coll;
    }

    private void OnSceneGUI ( ) {
        tilemap = (Tilemap3)target;
        coll = tilemap.coll;
        mode = tilemap.mode;
        tile = tilemap.tile;
        
        Event currentEvent = Event.current;
        if (currentEvent.type == EventType.Layout) {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(GetHashCode(), FocusType.Passive));
        }
        if ((currentEvent.type == EventType.MouseDrag && currentEvent.button == 0) || (currentEvent.type == EventType.MouseDown && currentEvent.button == 0)) {
            Ray worldRay = HandleUtility.GUIPointToWorldRay(currentEvent.mousePosition);
            RaycastHit hitInfo;

            if (coll.Raycast(worldRay, out hitInfo, 1000f)) {
                GameObject hitObj = hitInfo.collider.gameObject;
                if (hitObj.CompareTag("3DTilemap")) {
                    Vector3 hitPoint = hitInfo.point;
                    hitPoint = new Vector3(Mathf.RoundToInt(hitPoint.x), Mathf.RoundToInt(hitPoint.y), Mathf.RoundToInt(hitPoint.z));
                    GameObject obj;
                    bool tileHere = tilemap.tiles.TryGetValue(hitPoint, out obj);
                    if (!tileHere && mode == Tilemap3.modeEnum.Draw) {
                        obj = PrefabUtility.InstantiatePrefab(tile as GameObject) as GameObject;
                        obj.transform.position = hitPoint;
                        obj.transform.parent = hitObj.transform;
                        tilemap.tiles.Add(hitPoint, obj);
                    } else if(tileHere && mode == Tilemap3.modeEnum.Erase) {
                        obj.transform.parent = null;
                        tilemap.tiles.Remove(hitPoint);
                        DestroyImmediate(obj);
                    }
                }
            }
            currentEvent.Use();
        }
    }
}
#endif
