using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class projController : MonoBehaviour {

	public Rigidbody2D projBoom;
	Animator anim; 
	private float playerY;
	AudioSource enemyKill;
	public static int kill=0;
	public static int rollingScore;
	public bool killTrigger = false;

	void Start () {
		anim = GetComponent<Animator>();
		enemyKill = GetComponent<AudioSource> ();
		playerY = GameObject.FindGameObjectWithTag ("Player").transform.position.y;
	}
		
	void FixedUpdate () {
		anim.SetFloat ("animSpeed",1.0f);
		transform.position = new Vector3 (transform.position.x + 0.7f,playerY);
	}





	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("border"))
		{
			Destroy (projBoom.gameObject);
			playerController.enemyCount--;
		} else if (other.gameObject.CompareTag("Enemy"))
		{
			enemyKill.Play ();
			if (playerController.waveCount == 0){
				kill += 5;
			}else if (playerController.waveCount == 1){
				kill += 10;
			}else if (playerController.waveCount == 2){
				kill += 15;
			}
			playerController.enemyCount--;
			Destroy (other.gameObject);
			Destroy (projBoom.gameObject);
		}
	}	
}
