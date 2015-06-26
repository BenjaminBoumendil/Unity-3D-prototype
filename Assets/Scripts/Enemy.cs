using UnityEngine;
using System.Collections;

public class Enemy : AEntity {
	
	void Start () {
		animator = gameObject.GetComponent<Animator> ();
	}
}
