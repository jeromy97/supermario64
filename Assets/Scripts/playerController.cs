using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

    private int coins;
    private int stars;
    private int lifes;

    public Animator anim;

    public Text coinText;
    public Text starText;
    public Text lifeText;

    // Use this for initialization
    void Start () {
        coins = 0;
        stars = 0;
        lifes = 3;

        setCoinText();
        setStarText();
        setLifesText();
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
                    health.TakeDamage(100);
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
            coins++;
            setCoinText();
        }
    }

    void setCoinText()
    {
        coinText.text = "Coins: " + coins.ToString();
    }

    void setStarText()
    {
        starText.text = "Stars: " + stars.ToString();
    }
    void setLifesText()
    {
        lifeText.text = "Lifes: " + lifes.ToString();
    }


}
