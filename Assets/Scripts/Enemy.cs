using UnityEngine;
using System.Collections;

public class Enemy : AEntity {

	public GameObject target;

	private void movementHandler() {
		if (Mathf.Abs (transform.position.x - target.transform.position.x) < 6f && Mathf.Abs (transform.position.y - target.transform.position.y) < 6f) {
			if (!animator.GetBool("Attack"))
				StartCoroutine(attack(4));
			animator.SetBool("Walk", false);
		} else {
			move (Vector3.forward);
		}
	}

	private void rotationHandler() {
		transform.LookAt (target.transform);
	}

	void Start () {
		animator = gameObject.GetComponent<Animator> ();
	}

	void Update () {
		if (!isDead) {
			rotationHandler ();
			movementHandler ();
		}
	}
}
