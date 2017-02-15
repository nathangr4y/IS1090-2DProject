using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This game is based off of Data East's 1991 SNES platformer, Joe & Mac
//All sprite sheets were found here: https://www.spriters-resource.com/snes/joemac/
//All sound effects were found here: https://www.sounds-resource.com/snes/joeandmaccavemanninja/sound/300/

//The player picks up a single boomerang from the repository (pile of boomerangs). Player must return to the 
//stockpile to get another shot. The player can withstand 3 hits and must survive 3 increasingly difficult 
//waves of enemies. 


public class playerController : MonoBehaviour {

	public float speed = 1.0f;
	private int projCount = 0;
	private int victCount = 0;
	private int defeatCount = 0;
	private int clickCount = 0;
	private int clickInstCount = 0;
	public static int enemyCount = 0;
	private bool alive = true;
	public static bool pause = false;
	public bool repTrig = false;

	public Rigidbody2D player;
	public Rigidbody2D projBoom;
	public Rigidbody2D victMess;
	public Rigidbody2D gameOver;
	public Rigidbody2D boomToken;
	public Rigidbody2D boomToken1;
	public Rigidbody2D life1;
	public Rigidbody2D life2;
	public Rigidbody2D life3;
	public Rigidbody2D waveMarker2;
	public Rigidbody2D waveMarker3;

	public Text scoreDisplay;
	public int life;

	public AudioSource bgMusic; 
	public AudioSource takeDamage;
	public AudioSource enemyKill;
	public AudioSource defeat;
	public AudioSource victory;

	Animator anim;

	private static int score;
	private int scoreTemp;
	private static int enemyPassCount;
	public static int waveCount = 0;

	void Start () {
		life = 3;
		anim = GetComponent<Animator> ();
	}


	void FixedUpdate () {
		//Initializations
		scoreTemp = score;
		score = projController.kill;
		enemyPassCount = fieldController.enemyPassed;

		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		scoreDisplay.text = ""+score;


		//Life Bar
		if (life == 2) 
		{
			life3.gameObject.SetActive (false);
		} else if (life == 1) 
		{
			life2.gameObject.SetActive (false);
		} else if (life == 0)
		{
			life1.gameObject.SetActive (false);
		}


		//Enemy Wave Management
		if (score == 75) {
			waveCount = 1;
			waveMarker2.gameObject.SetActive (true);
		} else if (score == 235) {
			waveCount = 2;
			waveMarker3.gameObject.SetActive (true);
		} else if (score == 535) {
			while (victCount < 1) {
				victory.Play ();
				victCount++;
			}
			bgMusic.Pause ();
			anim.SetBool ("victory", true);
			victMess.gameObject.SetActive (true);
			alive = false;
			pause = true;
			for (int i = 0; i <= enemyCount; i++) {
				Destroy (GameObject.FindGameObjectWithTag ("Enemy"));
				enemyCount = 0;
			}
		}


		//Controls and Animation
		if (alive)
		{
			transform.position = new Vector3 (transform.position.x + speed * moveHorizontal, transform.position.y + speed * moveVertical); 
			anim.SetFloat ("animSpeed", Mathf.Abs (Input.GetAxisRaw ("Horizontal") + Input.GetAxisRaw ("Vertical")));
		} else 
		{
			player.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
		}
			

		//Projectiles
		if (Input.GetKey (KeyCode.Mouse0)) 
		{
			while (clickInstCount < 1) 
			{
				if (clickCount == 0) 
				{
					if (repTrig == true) 
					{
						anim.SetBool ("pickUp", true);
						boomToken.gameObject.SetActive (false);
						boomToken1.gameObject.SetActive (true);
						clickCount++;
					}
				} else if (clickCount == 1) 
				{
					while (projCount < 1) {
						anim.SetBool ("pickUp", false);
						anim.SetTrigger ("release");
						Rigidbody2D	projBoomClone = (Rigidbody2D)Instantiate (projBoom, new Vector3 (GameObject.FindGameObjectWithTag ("Player").transform.position.x, GameObject.FindGameObjectWithTag ("Player").transform.position.y, 0.0f), transform.rotation);
						boomToken.gameObject.SetActive (true);
						boomToken1.gameObject.SetActive (false);
						projCount++;
					}
					clickCount = 0;
				}
				clickInstCount++;
			}
		} else 
		{
			projCount = 0;
			clickInstCount = 0;
		}
			

		//Game Over
		if (enemyPassCount == 5 || life == 0) 
		{
			gameOver.gameObject.SetActive (true);
			alive = false;
			anim.SetTrigger ("dieTrig");
			anim.SetBool ("dieBool", true);
			while (defeatCount < 1) 
			{
				defeat.Play ();
				defeatCount++;
			}
			bgMusic.Pause ();
		}


		//had to use this in order to play the sound effect, since destroying the object cancelled the sound
		if (score != scoreTemp) 
		{
			enemyKill.Play ();
		}

	}


	//Take Damage and Get Projectile
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Enemy") && life != 0)
		{
			Destroy (other.gameObject);
			takeDamage.Play ();
			anim.SetTrigger ("hitTrig");
			life--;
		} 
		else if (other.gameObject.CompareTag ("Repository")) 
		{
			repTrig = true;
		} else if (other.gameObject.CompareTag ("RepBorder"))
		{
			repTrig = false;
		}
	}
}
