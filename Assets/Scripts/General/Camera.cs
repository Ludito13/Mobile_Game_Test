using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Movimiento de cámara
public class Camera : MonoBehaviour
{
    public Transform player;
    public float speed;
    public float distance;
    public float angle;
    public float height;
    public float smoothness;
    private Vector3 referenceVelociy;

    void Update()
    {
        toCam();
    }

    private void toCam()
    {
        if (!player)
            return;

        Vector3 worldPos = (Vector3.forward * -distance) + (Vector3.up * height);
        Vector3 angleVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPos;
        Vector3 flatPos = player.position;
        flatPos.y = 0;
        Vector3 finalPos = flatPos + angleVector;

        transform.position = Vector3.SmoothDamp(transform.position, finalPos, ref referenceVelociy, smoothness);
    }
}
