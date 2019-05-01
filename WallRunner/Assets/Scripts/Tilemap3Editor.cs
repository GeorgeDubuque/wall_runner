using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Tilemap3))]
public class Tilemap3Editor : Editor
{
    Tilemap3 tilemap;
    GameObject scriptObject;
    Tilemap3.modeEnum mode;
    int hashCode;
    Collider coll;
    GameObject hitScriptObj;
    GameObject hitNonScriptObj;

    private void OnEnable ( ) {
        hashCode = GetHashCode();

        tilemap = (Tilemap3)target;
        scriptObject = tilemap.tile;
        mode = tilemap.mode;
        coll = tilemap.coll;
    }

    public override void OnInspectorGUI ( ) {
        base.OnInspectorGUI();

        hashCode = GetHashCode();

        tilemap = (Tilemap3)target;
        scriptObject = tilemap.tile;
        mode = tilemap.mode;
        coll = tilemap.coll;
    }

    private void OnSceneGUI ( ) {
        coll = tilemap.coll;
        mode = tilemap.mode;
        scriptObject = tilemap.tile;
        Event currentEvent = Event.current;
        if (Event.current.type == EventType.Layout) {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(GetHashCode(), FocusType.Passive));
        }
        if ((Event.current.type == EventType.MouseDrag && Event.current.button == 0) || (Event.current.type == EventType.MouseDown && Event.current.button == 0)) {
            Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hitInfo;

            if (coll.Raycast(worldRay, out hitInfo, 1000f)) {
                GameObject hitObj = hitInfo.collider.gameObject;
                if (hitObj.CompareTag("3DTilemap")) {
                    Vector3 hitPoint = hitInfo.point;
                    hitPoint = new Vector3(Mathf.RoundToInt(hitPoint.x), Mathf.RoundToInt(hitPoint.y), Mathf.RoundToInt(hitPoint.z));
                    int layerMask = ~LayerMask.GetMask("3DGrid");
                    Collider[] colls = 
                    Physics.OverlapBox(center: hitPoint, halfExtents: new Vector3(.48f, .48f, .48f), Quaternion.identity, layerMask, QueryTriggerInteraction.Collide);
                    
                    bool hitATile = HitTile(colls);
                    if (!hitATile && mode == Tilemap3.modeEnum.Draw) {
                        GameObject obj = PrefabUtility.InstantiatePrefab(scriptObject as GameObject) as GameObject;
                        obj.transform.position = hitPoint;
                        obj.transform.parent = hitObj.transform;
                    } else if(mode == Tilemap3.modeEnum.Erase) {
                        if(hitScriptObj != null)
                        {
                            hitScriptObj.transform.parent = null;
                            DestroyImmediate(hitScriptObj);
                        }
                    }
                }
            }
            Event.current.Use();
        }
    }

    bool HitTile(Collider[] colls)
    {
        bool hitTile = false;
        foreach(Collider currColl in colls)
        {
            if (currColl.CompareTag(scriptObject.tag)) {
                hitTile = true;
                hitScriptObj = currColl.gameObject;
            } else {
                hitTile = true;
                hitNonScriptObj = currColl.gameObject;
            }
                
        }

        return hitTile;
    }
}
#endif
