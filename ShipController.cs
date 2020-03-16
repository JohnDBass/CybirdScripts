using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float shipSpeed = 10; // Speed at which ship moves
    public float rollDistance = 20; // How far the player rolls
    public float rollSpeed = 5; // How fast the player rolls
    public float shotCooldown; // Time before player can shoot again
    public GameObject lazer; // Lazer player shoots
    public GameObject lazerSpawn; // Location lazer spawns from
    private float playerWidth;
    private float playerHeight;
    private float nextLazer = 0;
    private bool isRolling = false;
    private Rigidbody rb;
    private Vector2 screenBounds;
    private Vector3 rollDestination;
    public GameObject playerModel;
    private Animator anim;
    private AudioSource source;
    [SerializeField]
    private AudioClip lazerNoise, rollNoise;
    [SerializeField]
    private float lazerVolume, rollVolume;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Debug.Log("" + screenBounds.x);
        Debug.Log("" + screenBounds.y);
        playerWidth = 0; //this.GetComponent<MeshRenderer>().bounds.size.x / 2;
        playerHeight = 0; //this.GetComponent<MeshRenderer>().bounds.size.y / 2;

        nextLazer = 0;
        rb = GetComponent<Rigidbody>();
        anim = playerModel.GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!isRolling)
        {
            transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * shipSpeed,
                                Input.GetAxis("Vertical") * Time.deltaTime * shipSpeed, 0);

            if (Input.GetButtonDown("Jump"))
            {
                float horRoll = Mathf.Clamp((Input.GetAxis("Horizontal") * rollDistance) + transform.position.x,
                                            screenBounds.x + playerWidth, screenBounds.x * -1 - playerWidth);
                float vertRoll = Mathf.Clamp((Input.GetAxis("Vertical") * rollDistance) + transform.position.y,
                                            screenBounds.y + playerHeight, screenBounds.y * -1 - playerHeight);

                source.PlayOneShot(rollNoise, rollVolume);
                rollDestination = new Vector3(horRoll, vertRoll);
                isRolling = true;
                anim.SetTrigger("DoABarrelRoll");
            }

            nextLazer += Time.deltaTime;

            if (Input.GetButton("Fire1"))
            {
                if (nextLazer >= shotCooldown)
                {
                    source.PlayOneShot(lazerNoise, lazerVolume);
                    Instantiate(lazer, lazerSpawn.transform.position, lazerSpawn.transform.rotation);
                    nextLazer = 0;
                }                
            }
        }

        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, rollDestination, rollSpeed);
            if (this.transform.position == rollDestination)
            {
                isRolling = false;
            }
        }
    }

   private void LateUpdate()
   {
       Vector3 boundPos = this.transform.position;
       boundPos.x = Mathf.Clamp(boundPos.x, screenBounds.x + playerWidth, screenBounds.x * -1 - playerWidth);
       boundPos.y = Mathf.Clamp(boundPos.y, screenBounds.y + playerHeight, screenBounds.y * -1 - playerHeight);
       this.transform.position = boundPos;
    }

}