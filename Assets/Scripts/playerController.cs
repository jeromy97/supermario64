using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

    public Animator anim;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.Play("Armature|ArmatureAction.001");
            RaycastHit hit;

            if (Physics.Raycast(transform.FindChild("Eyes").position, transform.FindChild("Eyes").forward, out hit, 0.7f))
            {
                GameObject target = hit.collider.gameObject;
                print("WE HIT " + target.name);
                Health health = target.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(0);
                }

                if (target.tag.ToString() == "Goomba")
                {
                    //Gooi naar achter
                    print("HET IS EEN GOOMBA VERDOMME");
                    target.transform.rotation = transform.rotation;
                    //target.GetComponent<Rigidbody>().AddForce(target.transform.forward.x, target.transform.forward.y, target.transform.forward.z + 400);//new Vector3(400, 100, 0));
                    //Debug.Log(transform.forward.x + ", " + transform.forward.y + ", " + transform.forward.z);
                    Vector3  vec = target.transform.position;
                    vec -= this.transform.position;
                    vec.Normalize();
                    vec *= 15;
                    target.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    target.GetComponent<Rigidbody>().AddForce(vec, ForceMode.Impulse); // target_.rigidbody.AddExplosionForce(force,target_.transform.position,10,0,ForceMode.Impulse); } 

                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            other.gameObject.SetActive(false);
        }
    }

}
