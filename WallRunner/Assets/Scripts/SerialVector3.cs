using UnityEngine;

[System.Serializable]
public class SerialVector3
{
    float x;
    float y;
    float z;
    public SerialVector3(Vector3 vector) {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public Vector3 GetVect ( ) {
        return new Vector3(x, y, z);
    }
}
