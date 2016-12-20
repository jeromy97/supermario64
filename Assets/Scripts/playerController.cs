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
    
    private Animator anim;

    private Rigidbody rb;

    public Text coinText;
    public Text starText;
    public Text lifeText;
    public Text redCoinsText;
    public Text healthText;
    
    public Image healthBar;

    public Sprite[] healthBarSprite;
    public star usestar;

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

                    usestar.activate();

                }
            }
        }
        
        if (other.gameObject.CompareTag("star"))
        {
            other.gameObject.SetActive(false);
            stars++;
            setStarText();
            SceneManager.LoadScene("Castle", LoadSceneMode.Single);
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
