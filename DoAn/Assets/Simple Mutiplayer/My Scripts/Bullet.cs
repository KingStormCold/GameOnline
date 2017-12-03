using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		GameObject hit = collision.gameObject;
		Heatlh heatlh = hit.GetComponent<Heatlh> ();
		if (heatlh != null) {
			heatlh.TakeDame (10);
		}
		Destroy (gameObject);
	}
}
