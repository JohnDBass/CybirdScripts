using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int respawnPenalty = 50; // Points lost after player respawns
    public float speed = 10; // Speed player travels
    public float rotateSpeed = 100; // Speed player rotates
    public float controllerRotateSpeed = 2000;
    public float jumpHeight = 5; // Height of player's jump
    public float doubleJumpHeight = 4f; // Height of player's double jump
    public float attackDelay = 5; // Time before player can attack again
    public float shootDelay;
    public float hoverStartupTime = .1f; // Time it required to hold before hover starts
    public float fuelRate = 10; // Rate at which fuel depletes and replenishes
    public bool hasAttackEnabled; // Determines if player can use Melee Attack
    public bool hasRangedEnabled; // Determines if player can use Ranged Attack
    public bool hasDoubleJumpEnabled; // Determines if player can double jump
    public bool hasJetpackEnabled; // Determines if player can jetpack
    public bool isOnGround; // Determines if player is on ground *Left public for debug purposes
    public GameObject Melee; // GameObject that serves as melee attack
    public GameObject dumbLaser;
    public GameObject aimLaser;// GameObject that serves as projectile attack
    public GameObject playerModel;
    public GameObject energyBar;
    public Image fuelBar;
    public Material[] materials;
    public Renderer rend;


    AudioSource audioSource;
    private Animator anim;
    private float nextAttackTime;
    private float shootCooldown;
    private float fuel = 100; // Shows remaing fuel left. Bind this to a UI meter
    public float maxFuel;
    private float hoverPressedTime;
    private float jumpTime = 0f;
    private bool isDoubleJumpAvailable;
    private bool isFloating = false;
    private bool nearAButton = false;
    private GameObject ButtonImNear;
    private Rigidbody rigid; // Rigidbody component, attach to same object as script
    private Vector3 respawnPoint;
    private float attackLength = 1f;
    [SerializeField]
    private AudioClip HurtNoise, FlapNoise, MeleeNoise, RespawnNoise, LazerNoise, JetpackNoise;
    [SerializeField]
    private float HurtVolume, FlapVolume, MeleeVolume, RespawnVolume, LazerVolume, JetpackVolume;
    private Transform dasBoost; //where on the player the jetpack particles will play
    [SerializeField]
    private GameObject jetpackParticles;
    private bool boosting = false;
    private GameObject onFire;
    private GameObject player;



    private bool TimeStop = false;
    /*
     * Gets reference to rigidbody component, locks cursor position, 
     * and sets initial values for fuel and nextAttackTime
     */
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rigid = GetComponent<Rigidbody>();
        nextAttackTime = 0;
        fuel = 100;
        maxFuel = fuel;
        anim = playerModel.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        dasBoost = this.gameObject.transform.GetChild(1);//
        player = GameObject.Find("Player"); //
        rend.enabled = true;
        rend.sharedMaterial = materials[0];
    }

    /*
     * Handles all real time game events 
     */
    private void Update()
    {
        if (hasJetpackEnabled)
        {

            fuelBar.fillAmount = fuel / maxFuel;
        }




        jumpTime += Time.deltaTime;

        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0,
                            Input.GetAxis("Vertical") * Time.deltaTime * speed);
        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed, 0);
        transform.Rotate(0, Input.GetAxis("Controller X") * Time.deltaTime * controllerRotateSpeed, 0);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }


        /*
         * Restores fuel while player is on ground
         */
        if (isOnGround)
        {
            anim.SetBool("Gliding", false);
            fuel = Mathf.MoveTowards(fuel, 100f, fuelRate * Time.deltaTime * 2);
        }

        attackLength += Time.deltaTime;

        if (attackLength >= 1)
        {
            anim.SetBool("Attacking", false);
        }

        if (hasAttackEnabled)
        {
            rend.sharedMaterial = materials[2];

        }
        if (hasDoubleJumpEnabled)
        {
            rend.sharedMaterial = materials[1];

        }

        /*
         * Instantiates melee attack
         */
        if (Input.GetButton("Melee") && hasAttackEnabled)
        {
            if (nextAttackTime < Time.time)
            {
                attackLength = 0;
                anim.SetTrigger("AttackTrig");
                Debug.Log("Active");
                Melee.SetActive(true);
                MeleeAttack call = Melee.GetComponent<MeleeAttack>();
                call.SetTime();
                nextAttackTime = Time.time + attackDelay;
                audioSource.PlayOneShot(MeleeNoise, MeleeVolume);
            }
        }

        /*
         * Instantiates ranged attack
        */
        shootCooldown += Time.deltaTime;

        if ((Input.GetButton("Fire1") && !Input.GetButton("Fire2")) && hasRangedEnabled)
        {
            if (shootCooldown >= shootDelay)
            {
                audioSource.PlayOneShot(LazerNoise, LazerVolume);
                Instantiate(dumbLaser, transform.position, transform.rotation);
                shootCooldown = 0;
            }

        }

        if ((Input.GetButton("Fire1") && Input.GetButton("Fire2")) && hasRangedEnabled)
        {
            if (shootCooldown >= shootDelay)
            {
                audioSource.PlayOneShot(LazerNoise, LazerVolume);
                Instantiate(aimLaser, transform.position, transform.rotation);
                shootCooldown = 0;
            }
        }

        /*
         * Causes player to jump
         */
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            rigid.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            audioSource.PlayOneShot(FlapNoise, FlapVolume);
            anim.SetTrigger("JumpTrig");
            hoverPressedTime = Time.time + hoverStartupTime;
            jumpTime = 0;

        }

        /*
         * Causes player to double jump
         */
        else if (Input.GetButtonDown("Jump") && hasDoubleJumpEnabled && jumpTime >= .2)
        {
            if (isDoubleJumpAvailable)
            {
                isDoubleJumpAvailable = false;
                rigid.AddForce(new Vector3(0, doubleJumpHeight, 0), ForceMode.Impulse);
                audioSource.PlayOneShot(FlapNoise, FlapVolume);
                hoverPressedTime = Time.time + hoverStartupTime;
                anim.SetTrigger("JumpTrig");
                jumpTime = 0;
            }
        }

        /*
         * Causes player to begin floating
         */
        else if (Input.GetButton("Jump") && jumpTime >= .2 && hasJetpackEnabled && !isDoubleJumpAvailable && (fuel > 0))
        {
            if (Time.time > hoverPressedTime)
            {
                anim.SetBool("Gliding", true);
                audioSource.Play();
                isFloating = true;
                if (!boosting) //
                {
                    onFire = Instantiate(jetpackParticles, dasBoost.transform.position, this.transform.rotation);
                    onFire.transform.parent = player.transform;
                    boosting = true;
                }

                //jumpTime = 0;
            }
        }


        /*
         * When The Player Uses an Item
         */

        if (Input.GetButton("Use"))
        {

            Debug.Log("Trying to Use Something");
            if (nearAButton)
            {
                ButtonImNear.SendMessage("Pressed");
            }
        }


        if (isFloating && (fuel > 0) && jumpTime >= .2)
        {
            rigid.useGravity = false;
            rigid.velocity = new Vector3(0, 0, 0);

            fuel = Mathf.MoveTowards(fuel, 0, fuelRate * Time.deltaTime);
            fuelBar.fillAmount = fuel / maxFuel;
        }

        /*
         * Ends float
         */
        if ((Input.GetButtonUp("Jump") && isFloating) || fuel == 0)
        {
            rigid.useGravity = true;
            isFloating = false;
            audioSource.Stop();
            Destroy(onFire.gameObject);
            boosting = false;
        }

        /*
         * Respawns player if they fall too low
         */
        if (this.transform.position.y < -50)
        {
            this.transform.position = respawnPoint;
        }

    }

    /*
     * Public function that enables double jump
     */
    public void EnableDoubleJump()
    {
        hasDoubleJumpEnabled = true;
    }

    /*
     * Public function that enables jetpack
     */
    public void EnableJetpack()
    {
        hasJetpackEnabled = true;
        energyBar.SetActive(true);
    }

    /*
     * Public function that enables melee attack
     */
    public void EnableAttack()
    {
        hasAttackEnabled = true;
    }

    /*
     * Public function that enables ranged attack
     */
    public void EnableRanged()
    {
        hasRangedEnabled = true;
    }

    /*
     * Checks if player is touching ground
     */
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isOnGround = true;
            isDoubleJumpAvailable = true;
            anim.SetBool("Jumping", false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHit"))
        {
            audioSource.PlayOneShot(HurtNoise, HurtVolume);
            this.SendMessage("TakeDamage", 10);
        }
        if (other.CompareTag("Button"))
        {
            Debug.Log("Found a Button");
            nearAButton = true;
            ButtonImNear = other.gameObject;
        }
    }
    /*
    * Checks if player is no longer touching ground or next to a button
    */
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isOnGround = false;
        }
        if (other.CompareTag("Button"))
        {
            nearAButton = false;
        }
    }

    /*
     * Public function that respawns player
     */
    public void SetRespawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }

    public void Respawn()
    {
        this.SendMessage("SubtractScore", respawnPenalty);
        audioSource.PlayOneShot(RespawnNoise, RespawnVolume);
        this.transform.position = respawnPoint;
    }

    public void SetLevel(int LevelNum)
    {
        if (LevelNum == 1)
        {

        }
        if (LevelNum == 2)
        {

        }
        if (LevelNum == 3)
        {
            hasDoubleJumpEnabled = true;
            hasRangedEnabled = true;
            hasAttackEnabled = true;
            hasJetpackEnabled = true;
        }

    }

}

