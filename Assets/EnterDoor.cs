using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Application.LoadLevel("HubLevel");
            Debug.Log("To Hublevel");
            SceneManager.LoadScene("HubLevel");
        }
    }
}
