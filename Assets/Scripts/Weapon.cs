using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.GetComponent<AEntity>() && collision.gameObject.tag != tag) {
			collision.gameObject.GetComponent<AEntity>().damage(10f);
		}
	}
}
