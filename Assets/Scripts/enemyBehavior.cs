using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour {

	public Rigidbody2D enemy; // dont actually need this; this script addresses the entire object
	public Rigidbody2D projBoom;
	public float speed;
	private float posY;
	Animator anim;


	void Start () {
		anim = GetComponent<Animator>();

		posY = Random.Range (-33.0f, 11.0f);

		if (playerController.waveCount == 1 || playerController.waveCount == 2) {
			speed = Random.Range(0.10f,0.30f);
		} else {
			speed = Random.Range(0.05f,0.25f);
		}
	}

	void Update () {
		transform.position = new Vector3 (transform.position.x - speed, posY);
	}
}
