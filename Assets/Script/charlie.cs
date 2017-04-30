using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charlie : MonoBehaviour {

	public AudioSource[] source;
	public AudioSource source1;
	
	// Use this for initialization
	void Start () {
		source1 = source[0];
		source1.Play();
	}
}
