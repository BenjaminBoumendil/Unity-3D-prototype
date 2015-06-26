using UnityEngine;
using System.Collections;

// Abstract class for Player and Enemy
public abstract class AEntity : MonoBehaviour {

	protected float life = 100;
	protected Animator animator;

	// Moving  Entity
	protected void move(Vector3 direction) {
		transform.Translate (direction * 10f * Time.deltaTime);
		animator.SetBool ("Walk", true);
	}

	// When Entity take damage
	public void damage(float damage) {
		life -= damage;
		if (life <= 0f) {
			animator.SetBool("Died", true);
		}
	}
}
