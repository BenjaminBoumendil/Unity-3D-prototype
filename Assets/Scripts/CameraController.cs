using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject target;
	private Vector3 offset;

	void Start() {
		offset = target.transform.position - transform.position;
	}

	void LateUpdate () {
		float desiredAngle = target.transform.eulerAngles.y;
		Quaternion rotation = Quaternion.Euler (0, desiredAngle, 0);
		transform.position = target.transform.position - (rotation * offset);
		transform.LookAt (target.transform);
	}
}
