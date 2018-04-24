using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

	private Rigidbody2D rb2d;
    public float fallDelay;
	float interval;
    public int number = 0;
    void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}

    void OnCollisionEnter2D(Collision2D col)
	{
        if (col.collider.CompareTag ("Player") && number == 0) {
            
            StartCoroutine(Fall());
        }
        if (gameObject.transform.position.y < -.6)
        {
            BoxCollider2D[] gamecolliders = gameObject.GetComponents<BoxCollider2D>();
            foreach (BoxCollider2D bc in gamecolliders) bc.enabled = false;
        }
        


    }

	IEnumerator Fall()
	{
        yield return new WaitForSeconds (0.5f);
        
		rb2d.isKinematic = false;

        yield return new WaitForSeconds(0.5f);

            rb2d.isKinematic = true;
            GameObject newplatform = Instantiate(gameObject, new Vector3(-.21f, -.55f, -1), new Quaternion(0, 0, 0, 0));
            BoxCollider2D[] myColliders = newplatform.GetComponents<BoxCollider2D>();
            foreach (BoxCollider2D bc in myColliders) bc.enabled = true;
            newplatform.GetComponent<FallingPlatform>().enabled = true;
            Destroy(gameObject, 1);
        
        yield return 0;
	}



}
