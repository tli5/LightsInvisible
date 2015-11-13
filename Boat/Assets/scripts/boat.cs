using UnityEngine;
using System.Collections;

public class boat : MonoBehaviour {
	private Rigidbody rbody;
	public float turnSpeed = 1000f;
	public float accellerateSpeed = 1000f;
	private float curSpeed = 0f;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<NetworkView>().isMine)
		{
			InputMovement();
		}
	}

	void InputMovement() {
		float h = Input.GetAxis ("Horizontal" );
		float v = Input.GetAxis ("Vertical" ) * -1 ;
		rbody.AddTorque (0f, h*turnSpeed*Time.deltaTime, 0f );
		//rbody.AddForce (transform.up *v*accellerateSpeed*Time.deltaTime );
		curSpeed += Time.deltaTime * v * accellerateSpeed;
		transform.position += transform.up * curSpeed;

	}
}
