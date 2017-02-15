using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGenerator : MonoBehaviour {

	public Rigidbody2D enemy; //DO need this one 
	public int frameDelta;
	private int counter; 
	public static int randMax;
	public static int waveDelta;

	void Start () {
		counter = 1;
	}
		
	void Update () {
		if (playerController.waveCount == 0){
			randMax = 6;
			waveDelta = 240;
		} else if (playerController.waveCount == 1) {
			randMax = 4;
			waveDelta = 200;
		} else if (playerController.waveCount == 2) {
			randMax = 4;
			waveDelta = 180;
		}

		if(playerController.pause == false){
			counter++;
			if (counter % frameDelta == 0) {
				for (int i = 0; i < Random.Range (1, randMax); i++) {
					Rigidbody2D	enemyClone = (Rigidbody2D)Instantiate (enemy, transform.position, transform.rotation);
					playerController.enemyCount++;
				}
			}
		}
	}
}