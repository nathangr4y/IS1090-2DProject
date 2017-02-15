using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fieldController : MonoBehaviour {

	public Rigidbody2D border;
	public static int enemyPassed;
	public AudioSource enemyPassSound;



	void Start () {

		enemyPassed = 0;
	}
	
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			Destroy (other.gameObject);
			enemyPassSound.Play ();
			enemyPassed++; 
		}
	}

}
