using UnityEngine;
using System.Collections;

public class Swimmer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RenderSettings.fog = false;
        RenderSettings.fogColor = new Color(0.2f, 0.4f, 0.8f);
        RenderSettings.fogDensity = 0.03f;
	}
	
    bool IsUnderWater()
    {
        return gameObject.transform.position.y < 0;
    }

	// Update is called once per frame
	void Update () {
        RenderSettings.fog = IsUnderWater();

        if (IsUnderWater())
        {
            //transform.GetComponent<Animator>().Play("Armature | ArmatureAction");
        }
	}
}
