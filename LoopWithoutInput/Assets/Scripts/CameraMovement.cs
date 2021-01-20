using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public GameObject player1Ground;
    public GameObject player2Ground;

    private float dist;
    private float dist1;
    private float dist2;

    public float smoothTime = 0.6f;
    public float minDist = 1.5f;
    public float cameraDist = 1.5f;

    float yVelocity = 0.0f;
    public bool changeCamera = true;

    void Start()
    {
    }

    void LateUpdate()
    {
        dist1 = Vector3.Distance(player1Ground.transform.position, Vector3.zero); //min 1.48
        dist2 = Vector3.Distance(player2Ground.transform.position, Vector3.zero); //min 1.49
        dist = dist1 > dist2 ? dist1 : dist2;

        if (dist > minDist)
        {
            GetComponent<Camera>().orthographicSize = Mathf.SmoothDamp(GetComponent<Camera>().orthographicSize, dist + cameraDist, ref yVelocity, smoothTime);
        }
    }
}
