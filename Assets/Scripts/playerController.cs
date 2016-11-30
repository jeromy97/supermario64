using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour {

    private int coins;
    private int stars;
    private int lifes;
    private int health;
    private int redCoins;

    public Animator anim;

    public Text coinText;
    public Text starText;
    public Text lifeText;
    public Text healthText;
    public Text redCoinText;

    // Use this for initialization
    void Start () {
        coins = 0;
        stars = 0;
        lifes = 3;
        health = 100;
        redCoins = 0;

        setCoinText();
        setStarText();
        setLifesText();
        setHealthText();
        setRedCoinText();
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
                    health.TakeDamage(50);
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
            if (coins == 100)
            {
                lifes++;
            }
            if (health < 100)
            {
                health = health + 20;
                if (health > 100)
                {
                    health = 100;
                }

            }
            setCoinText();
            setLifesText();
            setHealthText();
        }

        if (other.gameObject.CompareTag("red_coin"))
        {
            other.gameObject.SetActive(false);
            redCoins++;
            setRedCoinText();
            if (redCoins == 8)
            {
                // Star appears...
            }
        }


        if (other.gameObject.CompareTag("star"))
        {
            other.gameObject.SetActive(false);
            stars++;
            setStarText();
        }

        if (other.gameObject.CompareTag("Goomba") || other.gameObject.CompareTag("Boo"))
        {
            loseHealth();
        }

        if (other.gameObject.CompareTag("green_mushroom"))
        {
            other.gameObject.SetActive(false);
            lifes++;
            setLifesText();
        }


    }

    // Puntensysteem en zooi //

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

    void setHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

    void setRedCoinText()
    {
        redCoinText.text = "Red coins: " + redCoins.ToString() + "/8";
    }

    void loseHealth()
    {
        health = health - 20;
        setHealthText();
        if (health > 1)
        {
            lifes--;
            setLifesText();
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
    }


}
