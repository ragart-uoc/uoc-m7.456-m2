/*
 * Create by Douglas Krein
 * Description: Script to destroy the particle system after 1 second
 * 
 * 
*/

using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		StartCoroutine(DestroyeObject());
	}

	IEnumerator DestroyeObject() {
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
