using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour {

    public GameObject player;
    public float zPosition = 0f;


    void FixedUpdate () {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, zPosition);
	}
}
