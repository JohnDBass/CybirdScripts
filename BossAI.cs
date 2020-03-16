using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{

    [SerializeField]
    private GameObject bullet, missile, laser, leftGun,
        rightGun, leftLauncher, rightLauncher, laserOrigin;

    [SerializeField]
    private float bulletDelay = 1.0f, missileDelay = 0.5f, laserChargeUp = 2.0f, laserDuration = 1.0f, floatStrength = 1;

    [SerializeField]
    private int maxBulletsPerSalvo = 10, maxMissilesPerSalvo = 5, laserDamage = 5;

    [SerializeField]
    private AudioClip chargeNoise, bigLaserNoise, smallLaserNoise;

    [SerializeField]
    private float chargeVolume, bigLaserVolume, smallLaserVolume;

    private GameObject player, laserEnd;
    private LineRenderer laserRenderer;
    private SphereCollider endCollider;
    private MeshRenderer endMesh;
    private PlayerHealth playerHealth;

    private float bulletTimer = 1.0f, chargeTimer = 0f, durationTimer = 0f, missileTimer = 1.0f;
    private float originalY;

    private int attackType, whichGun, bullets = 0, missiles = 0;
    private Animator anim;

    public GameObject bossModel;
    public GameObject chargePart;
    AudioSource audioSource;


    void Start()
    {
        anim = bossModel.GetComponent<Animator>();
        player = GameObject.Find("Player");
        attackType = Random.Range(1, 4);
        this.originalY = this.transform.position.y;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        //transform.LookAt(player.transform);
        transform.position = new Vector3(transform.position.x, originalY + ((float)Mathf.Sin(Time.time) * floatStrength),
            transform.position.z);

        bulletTimer += Time.deltaTime;
        missileTimer += Time.deltaTime;

        if (attackType == 1 && chargeTimer == 0)
        {
            whichGun = Random.Range(1, 3);

            if (bulletTimer >= bulletDelay)
            {
                if (whichGun == 1)
                {
                    Instantiate(bullet, new Vector3(rightGun.transform.position.x, rightGun.transform.position.y,
                        rightGun.transform.position.z), rightGun.transform.rotation);
                    anim.SetTrigger("RightShooting");


                    bullets += 1;
                }
                else if (whichGun == 2)
                {
                    Instantiate(bullet, new Vector3(leftGun.transform.position.x, leftGun.transform.position.y,
                        leftGun.transform.position.z), leftGun.transform.rotation);
                    anim.SetTrigger("LeftShooting");

                    bullets += 1;
                }

                audioSource.PlayOneShot(smallLaserNoise, smallLaserVolume);
                bulletTimer = 0f;
            }
        }

        if (bullets >= maxBulletsPerSalvo)
        {
            attackType = Random.Range(1, 4);
            bullets = 0;
        }

        if (attackType == 2 && chargeTimer == 0)
        {
            whichGun = Random.Range(1, 3);

            if (missileTimer >= missileDelay)
            {
                if (whichGun == 1)
                {
                    Instantiate(missile, new Vector3(rightLauncher.transform.position.x, rightLauncher.transform.position.y,
                        rightLauncher.transform.position.z), rightLauncher.transform.rotation);

                    missiles += 1;
                    anim.SetTrigger("HeadShooting");

                }
                else if (whichGun == 2)
                {
                    Instantiate(missile, new Vector3(leftLauncher.transform.position.x, leftLauncher.transform.position.y,
                        leftLauncher.transform.position.z), leftLauncher.transform.rotation);

                    missiles += 1;
                    anim.SetTrigger("HeadShooting");
                }
                missileTimer = 0f;
            }
        }

        if (missiles >= maxMissilesPerSalvo)
        {
            attackType = Random.Range(1, 4);
            missiles = 0;
        }

        if (attackType == 3)
        {
            if (chargeTimer == 0)
            {

                anim.SetTrigger("LazerCharging");
                chargePart.SetActive(true);
                audioSource.PlayOneShot(chargeNoise, chargeVolume);

            }
            chargeTimer += Time.deltaTime;




            if (chargeTimer >= laserChargeUp)
            {
                chargePart.SetActive(false);
                audioSource.PlayOneShot(bigLaserNoise, bigLaserVolume);
                Instantiate(laser, new Vector3(laserOrigin.transform.position.x, laserOrigin.transform.position.y,
                        laserOrigin.transform.position.z), laserOrigin.transform.rotation);

                attackType = Random.Range(1, 4);
                chargeTimer = 0;
            }
        }
    }
}
