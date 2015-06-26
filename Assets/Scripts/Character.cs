using UnityEngine;
using System.Collections;

public class Character : AEntity {

	public BoxCollider weapon;
	private Vector3 mousePos;
	private float energy = 100;

	private IEnumerator energyRegeneration() {
		while (true) {
			if (energy + 10 < 100)
				energy += 10;
			else
				energy = 100;
			yield return new WaitForSeconds(1);
		}
	}

	private IEnumerator attack() {
		weapon.enabled = true;
		animator.SetBool ("Attack", true);
		yield return new WaitForSeconds(2);
		animator.SetBool ("Attack", false);
		weapon.enabled = false;
	}
	
	private void moveChar(Vector3 direction) {
		if (Input.GetKeyDown ("space") && energy >= 25) {
			energy -= 25;
			GetComponent<Rigidbody> ().AddRelativeForce (new Vector3 (direction.x * 4f, direction.y + 2f, direction.z * 4f) * 100f);
		}
		else
			move (direction);
	}

	// Handle Keyboard and mouse input
	private void inputHandler() {
		// Movement
		if (Input.GetKey ("d") && Input.GetKey ("w"))
			moveChar (Vector3.right + Vector3.forward);
		else if (Input.GetKey ("d") && Input.GetKey ("s"))
			moveChar (Vector3.right + Vector3.back);
		else if (Input.GetKey ("a") && Input.GetKey ("w"))
			moveChar (Vector3.left + Vector3.forward);
		else if (Input.GetKey ("a") && Input.GetKey ("s"))
			moveChar (Vector3.left + Vector3.back);
		else if (Input.GetKey ("d"))
			moveChar (Vector3.right);
		else if (Input.GetKey ("a"))
			moveChar (Vector3.left);
		else if (Input.GetKey ("w"))
			moveChar (Vector3.forward);
		else if (Input.GetKey ("s"))
			moveChar (Vector3.back);
		else
			animator.SetBool ("Walk", false);

		// Attack
		if (Input.GetMouseButtonDown (0)) {
			StartCoroutine (attack ());
		}
	}

	// Handle Rotation with mouse position on X
	private void rotationHandler() {
		transform.Rotate (new Vector3 (0, (Input.mousePosition.x - mousePos.x) / 6, 0));
		mousePos = Input.mousePosition;
	}

	void Awake() {
		// mouse cursor can't go out of game window, move it to gameManager later
		Cursor.lockState = CursorLockMode.Confined;
	}

	void Start() {
		// Hide mouse cursor, move it to gameManager later
		Cursor.visible = false;

		animator = gameObject.GetComponent<Animator> ();

		mousePos = Input.mousePosition;

		// Launch coroutine for energy regeneration over time
		StartCoroutine (energyRegeneration());
	}

	void Update () {
		inputHandler ();
		rotationHandler ();
	}
}
