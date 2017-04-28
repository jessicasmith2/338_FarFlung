using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityStandardAssets.Characters.FirstPerson;

public class hook2 : MonoBehaviour {

	public Transform cam;
	private RaycastHit hit;
	private Rigidbody rb;
	public bool attached = false;
	private float distance;
	private float momentum;
	public float speed;
	private float step;
	public RigidbodyFirstPersonController cc;
	public bool enabled;
	public LineRenderer LR;
	

	void Start () {
		rb = GetComponent <Rigidbody> ();
	}

	void Update () {
		LR.useWorldSpace = true;

		if (Input.GetButtonDown ("Fire1")) {
			if (Physics.Raycast (cam.position, cam.forward, out hit) && hit.distance <= 60) {
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
			LR.enabled = false;
		}

		if (attached) {
			LR.enabled = true;
         	LR.SetPosition(0, new Vector3(cam.position.x,cam.position.y,cam.position.z));
			LR.SetPosition(1, new Vector3(hit.point.x,hit.point.y,hit.point.z));
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
