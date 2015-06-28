using UnityEngine;
using System.Collections;

// Abstract class for Player and Enemy
public abstract class AEntity : MonoBehaviour {

	protected float life = 100;
	protected Animator animator;
	protected bool isDead = false;
	public BoxCollider weapon;

	// handle weapon enabling and animation for attack
	protected IEnumerator attack(float waitTime) {
		weapon.enabled = true;
		animator.SetBool ("Attack", true);
		yield return new WaitForSeconds(waitTime);
		animator.SetBool ("Attack", false);
		weapon.enabled = false;
	}

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
			isDead = true;
		}
	}
}
