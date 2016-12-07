using UnityEngine;
using System.Collections;

public class star : MonoBehaviour {

	public float rotate_speed;

	// Use this for initialization
	void Start () {
        this.gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, Time.deltaTime * rotate_speed, 0));
	}


    public void activate()
    {
        this.gameObject.SetActive(true);
    }
}
