/*
 * Create by Douglas Krein
 * Description: Script to controll the player controllers, animations and audios
 * 
 * 
*/

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;
	public GameObject bomb;

	private bool jump = false;
	private bool grounded = false;
	private Animator animator;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (Input.GetKey("space") && grounded) {
			jump = true;
			StartCoroutine(CreateBomb(transform));
		}

	}

	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");

		animator.SetFloat ("Speed", Mathf.Abs (h));

		if (h * rb2d.velocity.x < maxSpeed) {
			rb2d.AddForce(Vector2.right * h * moveForce);
		}

		if (Mathf.Abs(rb2d.velocity.x)>maxSpeed){
			rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x)* maxSpeed, rb2d.velocity.y); 
		}

		if (h > 0 && !facingRight) {
			Flip ();
		}else if (h<0 && facingRight ) {
			Flip ();
		}

		if (jump) {
			animator.SetTrigger("Jump");
			rb2d.AddForce(new Vector2(0f,jumpForce));
			jump = false;
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	IEnumerator CreateBomb(Transform trans) {
		yield return new WaitForSeconds(2);
		Instantiate(bomb,trans.position,trans.rotation);
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Bomb"){
			Debug.Log("SCREAM");
			gameObject.GetComponent<AudioSource>().Play ();
		}

	}

}
