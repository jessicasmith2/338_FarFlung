using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class hook : MonoBehaviour {

	public Transform cam;
	private RaycastHit hit;
	private Rigidbody rb;
	public bool attached = false;
	private float momentum;
	public float maxDistance = 50f;
	public float speed;
	private float step;
	public RigidbodyFirstPersonController cc;

	void Start () {
		rb = GetComponent <Rigidbody> ();
	}

	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			if (Physics.Raycast (cam.position, cam.forward, out hit)) {
				cc.mouseLook.XSensitivity = 0;
				cc.mouseLook.YSensitivity = 0;
				attached = true;
				rb.isKinematic = true;
			} 
		}

		if (Input.GetButtonUp ("Fire1")) {
			cc.mouseLook.XSensitivity = 5;
			cc.mouseLook.YSensitivity = 5;
			attached = false;
			rb.isKinematic = false;
			rb.velocity = cam.forward * momentum / 2;
		}

		if (attached) {
			momentum += Time.deltaTime * speed;
			step = momentum * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, hit.point, step);
		}

		if (!attached && momentum >= 10) {
			momentum -= Time.deltaTime * 5;
			step = 0;
		}

		if (cc.Grounded && momentum <= 0) {
			momentum = 0;
			step = 0;
		}
	}
}
