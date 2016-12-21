using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour {

    private int coins;
    private int stars;
    private int lifes;
    private int redCoins;
    private int health;
    public GameObject star;
    
    private Animator anim;

    private Rigidbody rb;

    public Text coinText;
    public Text starText;
    public Text lifeText;
    public Text redCoinsText;
    public Text healthText;
    
    public Image healthBar;
    public Sprite[] healthBarSprite;

    public float Turning = 0.0f;

    public float LastJump = 0.0f;
    public int Jump = 0;

    // Use this for initialization
    void Start () {
        coins = 0;
        stars = 0;
        lifes = 3;
        redCoins = 0;
        health = 8;

        setCoinText();
        setHealthText();
        setStarText();
        setLifesText();
        setRedCoinsText();

        anim = GetComponent<Animator>();

        var stardeactivate = GameObject.FindWithTag("star");
        stardeactivate.gameObject.SetActive(false);

        System.Array.Reverse(healthBarSprite);

        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E");
            Pickup();
        }

        if (Input.GetKey(KeyCode.W) && !GetComponent<ThirdPersonCharacter>().m_IsGrounded)
        {
            Vector3 vec = transform.forward;
            //vec -= this.transform.position;
            vec.Normalize();
            vec /= 15;
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().AddForce(vec, ForceMode.Impulse);
        }

        LastJump += 1 * Time.deltaTime;

        if (LastJump < 1)
        {
            if (Jump == 1 && GetComponent<ThirdPersonCharacter>().m_IsGrounded)
            {
                GetComponent<ThirdPersonCharacter>().m_JumpPower = 13;
            }
            else if (Jump == 2 && GetComponent<ThirdPersonCharacter>().m_IsGrounded)
            {
                GetComponent<ThirdPersonCharacter>().m_JumpPower = 16;
            }
        }
        else
        {
            GetComponent<ThirdPersonCharacter>().m_JumpPower = 7;
            Jump = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && GetComponent<ThirdPersonCharacter>().m_IsGrounded)
        {
            LastJump = 0.0f;
            Jump++;
        }

        if (health >= 8)
        {
            health = 8;
        }

        if (health <= 0)
        {
            // Play dead animation.

            lifes--;
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }

        setHealthText();
        setCoinText();
        setLifesText();
        setRedCoinsText();
        setStarText();
    }

    public void bomdamage()
    {
        health = health -= 20;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            other.gameObject.SetActive(false);
            coins++;
            health = health + 1;
            if (coins >= 100)
            {
                lifes++;
                coins = 0;
            }
        }

        if (other.gameObject.CompareTag("red_coin"))
        {
            other.gameObject.SetActive(false);
            redCoins++;
            health = health + 1;
            if (redCoins >= 8)
            {
                {
                    star.gameObject.SetActive(true);          
                }
            }
        }
        
        if (other.gameObject.CompareTag("star"))
        {
            other.gameObject.SetActive(false);
            stars++;
            setStarText();
            SceneManager.LoadScene("HubLevel", LoadSceneMode.Single);
        }




    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("green_mushroom"))
        {
            other.gameObject.SetActive(false);
            lifes++;
        }

        if (other.gameObject.CompareTag("Goomba") || other.gameObject.CompareTag("Boo"))
        {
            health = health - 1;
        }

        /*
        if (other.gameObject.CompareTag("thwomp"))
        {
            //print("Points colliding: " + other.contacts.Length);
            //print("First point that collided: " + other.contacts[0].point);
            // If contact point is larger than mario's height, Mario will become flat.
            if (other.contacts[0].point.y > rb.position.y)
            {
                Debug.Log("Mario wordt geplet door de Thwomp.");
            }
            else
            {
                Debug.Log("Mario wordt niet geplet door de Thwomp.");
            }
        }
        */
    }

    void Attack()
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
                health.TakeDamage(50);
            }

            if (target.tag.ToString() == "Goomba")
            {
                //Gooi naar achter
                print("HET IS EEN GOOMBA VERDOMME");
                target.transform.rotation = transform.rotation;
                //target.GetComponent<Rigidbody>().AddForce(target.transform.forward.x, target.transform.forward.y, target.transform.forward.z + 400);//new Vector3(400, 100, 0));
                //Debug.Log(transform.forward.x + ", " + transform.forward.y + ", " + transform.forward.z);
                Vector3 vec = target.transform.position;
                vec -= this.transform.position;
                vec.Normalize();
                vec *= 15;
                target.GetComponent<Rigidbody>().velocity = Vector3.zero;
                target.GetComponent<Rigidbody>().AddForce(vec, ForceMode.Impulse); // target_.rigidbody.AddExplosionForce(force,target_.transform.position,10,0,ForceMode.Impulse); } 

            }
        }
    }

    void Pickup()
    {
        RaycastHit hit;
        Debug.Log(transform.FindChild("Grabpos").childCount.ToString());
        if (transform.FindChild("Grabpos").childCount > 0)
        {
            //Gooi
            Debug.Log("GOOII");
            Transform grabbed = transform.FindChild("Grabpos").GetChild(0);

            grabbed.transform.SetParent(null, true);
            //transform.FindChild("Grabpos").SetParent(null, true);
            grabbed.GetComponent<GoombaController>().enabled = true;
            grabbed.GetComponent<Rigidbody>().detectCollisions = true;//
            grabbed.GetComponent<Rigidbody>().isKinematic = false;//

            grabbed.transform.rotation = transform.rotation;
            Vector3 vec = grabbed.transform.position;
            vec -= this.transform.position;
            vec.Normalize();
            vec *= 15;
            grabbed.GetComponent<Rigidbody>().velocity = Vector3.zero;
            grabbed.GetComponent<Rigidbody>().AddForce(vec, ForceMode.Impulse);
        }
        else if (Physics.Raycast(transform.FindChild("Eyes").position, transform.FindChild("Eyes").forward, out hit, 1f))
        {
            GameObject target = hit.collider.gameObject;
            print("WE TRIED TO PICK " + target.name + " UP");
            //print("WE TRIED TO PICK " + target.transform.parent + " UP");

            if (target.tag.ToString() == "Goomba")
            {
                print("HET IS EEN GOOMBA VERDOMME");
                target.transform.parent = transform.FindChild("Grabpos").transform;
                target.GetComponent<GoombaController>().enabled = false;
                target.GetComponent<Rigidbody>().detectCollisions = false;//
                target.GetComponent<Rigidbody>().isKinematic = true;//

                //target.transform.rotation = transform.rotation;
                //Vector3 vec = target.transform.position;
                //vec -= this.transform.position;
                //vec.Normalize();
                //vec *= 15;
                //target.GetComponent<Rigidbody>().velocity = Vector3.zero;
                //target.GetComponent<Rigidbody>().AddForce(vec, ForceMode.Impulse); // target_.rigidbody.AddExplosionForce(force,target_.transform.position,10,0,ForceMode.Impulse); } 

            }
        }
        else
        {
            Debug.Log("No goomba :(");
        }
    }

    void setCoinText()
    {
        coinText.text = "x " + coins.ToString();
    }
    void setStarText()
    {
        starText.text = "x " + stars.ToString();
    }
    void setLifesText()
    {
        lifeText.text = "x " + lifes.ToString();
    }
    void setRedCoinsText()
    {
        redCoinsText.text = "x " + redCoins.ToString() + "/8";
    }
    void setHealthText()
    {
        //healthText.text = "Health: " + health.ToString();
       healthBar.sprite = healthBarSprite[health];
    }

}
