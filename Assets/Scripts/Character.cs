using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Character : AEntity {
	
	private float energy = 100;
	private Rigidbody playerRb;

	private IEnumerator energyRegeneration() {
		while (true) {
			if (energy + 10 < 100)
				energy += 10;
			else
				energy = 100;
			yield return new WaitForSeconds(1);
		}
	}

	private IEnumerator specialAttack() {
		weapon.enabled = true;
		playerRb.AddTorque (Vector3.up * 10000f);
		yield return new WaitForSeconds(4);
		weapon.enabled = false;
	}

	private void moveChar(Vector3 direction) {
		if (Input.GetKeyDown (KeyCode.LeftShift) && energy >= 25) {
			energy -= 25;
			playerRb.AddRelativeForce (new Vector3 (direction.x * 4f, direction.y + 2f, direction.z * 4f) * 100f);
		} else if (Input.GetKeyDown("space")) {
			playerRb.AddForce(Vector3.up * 500f);
		} else
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
		else if (Input.GetKeyDown("space"))
			playerRb.AddForce(Vector3.up * 500f);
		else
			animator.SetBool ("Walk", false);

		// Attack
		if (Input.GetMouseButtonDown (0))
			StartCoroutine (attack (2));
		else if (Input.GetMouseButtonDown (1))
			StartCoroutine (specialAttack ());
	}

	// Handle Rotation with mouse position on X
	private void rotationHandler() {
		transform.Rotate (new Vector3 (0, Input.GetAxis("Mouse X") * 4, 0));
	}

	void OnGUI() {
		GUI.TextField (new Rect(10, 10, 60, 20), "Life : " + life.ToString(), 25);
		GUI.TextField (new Rect(10, 35, 80, 20), "Energy : " + energy.ToString(), 25);
	}

	void Awake() {
		// mouse cursor can't go out of game window, move it to gameManager later
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Start() {
		// Hide mouse cursor, move it to gameManager later
		Cursor.visible = false;

		animator = GetComponent<Animator> ();
		playerRb = GetComponent<Rigidbody> ();

		// Launch coroutine for energy regeneration over time
		StartCoroutine (energyRegeneration());
	}

	void Update () {
		if (!isDead) {
			inputHandler ();
			rotationHandler ();
		}
	}
}
