using UnityEngine;
using System.Collections;

public class GoombaController : MonoBehaviour {

    public int speed;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
        rb.velocity = new Vector3(speed, rb.velocity.y, 0.0f);
	}
}
