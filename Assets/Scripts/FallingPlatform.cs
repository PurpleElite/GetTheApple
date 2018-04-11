using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

	private Rigidbody2D rb2d;
	public float fallDelay;
	float interval;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}
		

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.CompareTag ("Player")) {
			var normal = col.contacts[0].normal;
			if (normal.y < 0) {
				Destroy (gameObject, 1);
			}
		}
	}

	IEnumerator Fall()
	{
		yield return new WaitForSeconds (fallDelay);
		rb2d.isKinematic = false;
		yield return 0;
	}



}
