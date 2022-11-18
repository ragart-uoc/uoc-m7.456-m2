/*
 * Create by Douglas Krein
 * Description: Script to controll the bomb behavior
 * 
 * 
*/

using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour {

	public float explosionDelay = 2f;
	public float explosionMaxSize = 6f;
	public GameObject explosionParticles;

	private bool exploded = false;
	private CircleCollider2D explosionRadius;

	// Use this for initialization
	void Start () {
		explosionRadius = gameObject.GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {

		if (!exploded) {
			if (explosionDelay < 0) {
				exploded = true;
				StartCoroutine(ExplodeBomb());
			} else {
				explosionDelay -= Time.deltaTime;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(exploded) {
			if (other.tag == "Player"){
				Debug.Log("Player hit");
			}
		}
	}
		
	IEnumerator ExplodeBomb() {
		yield return new WaitForSeconds(explosionDelay);
		Debug.Log("Explode increase size");
		explosionRadius.radius = explosionMaxSize;
		gameObject.tag = "Bomb";
		Instantiate(explosionParticles, transform.position, Quaternion.identity);
		PlaySound();
		yield return new WaitForSeconds(1f);
		DestroyeBomb();
	}

	void PlaySound() {
		Debug.Log("Play sound");
		gameObject.GetComponent<AudioSource>().Play();
	}
	
	void DestroyeBomb() {
		Debug.Log("Destroy bomb");
		Destroy(gameObject);
	}
	
}
