using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]

public class EnemyBehave : MonoBehaviour
{

    AudioSource audioSource;
    public float seeDistance = 30f;
    public float attackDistance = 10f;
    public GameObject shootyBall;
    public GameObject meleeHit;
    public GameObject meleeEnd;
    public GameObject meleeStart;
    public GameObject player;
    public GameObject enemyModel;
    public GameObject rightArm;
    public GameObject leftArm;

    private NavMeshAgent enemyAgent;
    private Animator anim;
    private int whichArm = 1;

    bool patting = true;
    bool Ouchy = false;
    bool alive = true;
    bool iAmMelee = false;
    bool iAmRanged = false;
    private float attackTimer = 3f;
    public float enemySpeed = 1f;
    float enemyStopDist = 2f;

    [SerializeField]
    private AudioClip AttackNoise, ChaseNoise, RangeNoise;
    [SerializeField]
    private float AttackVolume = 0.7f, ChaseVolume = 0.7f, RangeVolue = 0.5f;
    

    //int currentPoint = 1;

    Rigidbody rigid;
    public GameObject GameStuff;
    //public GameObject patPoint1;
    // public GameObject patPoint2;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        GameStuff = GameObject.Find("spawner");
        enemyAgent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        anim = enemyModel.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }




    // Update is called once per frame
    void Update()
    {

        attackTimer += Time.deltaTime;
        //Debug.Log(attackTimer);


        float dist = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log(dist);
        // float patDist1 = Vector3.Distance(transform.position, patPoint1.transform.position);
        //float patDist2 = Vector3.Distance(transform.position, patPoint2.transform.position);

        if (attackDistance <= dist && dist <= seeDistance && alive)
        {
            patting = false;
            Vector3 chase = transform.position - player.transform.position;
            Vector3 moveTo = transform.position - chase;
            enemyAgent.SetDestination(moveTo);
            enemyAgent.speed = enemySpeed;
            anim.SetBool("Moving", true);
        }
        else if (dist < attackDistance && alive)
        {

            if (iAmMelee)
            {
                patting = false;
                Vector3 chase = transform.position - player.transform.position;
                Vector3 moveTo = transform.position - chase;
                enemyAgent.SetDestination(moveTo);
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(ChaseNoise, ChaseVolume);
                }          

                if (dist < enemyStopDist)
                {
                    enemyAgent.speed = 0.1f;

                }

                if (attackTimer >= 3f)
                {
                    //player.SendMessage("TakeDamage", 25);                    
                    anim.SetBool("Attacking", true);
                    meleeHit.SetActive(true);
                    meleeHit.transform.position = Vector3.MoveTowards(meleeHit.transform.position, meleeEnd.transform.position, Time.deltaTime);
                    if (meleeHit.transform.position == meleeEnd.transform.position)
                    {
                        audioSource.PlayOneShot(AttackNoise, AttackVolume);
                        attackTimer = 0f;                        
                        meleeHit.SetActive(false);
                        meleeHit.transform.position = meleeStart.transform.position;
                        anim.SetBool("Attacking", false);                        
                    }
                }
            }
            else if (iAmRanged)
            {
                //Vector3 chase = transform.position - player.transform.position;
                //Vector3 moveTo = transform.position - chase;
                enemyAgent.SetDestination(player.transform.position);
                enemyAgent.speed = 0.1f;
                if (attackTimer >= 3f)
                {
                    if(whichArm == 1)
                    {
                        Instantiate(shootyBall, new Vector3(rightArm.transform.position.x, rightArm.transform.position.y, rightArm.transform.position.z), rightArm.transform.rotation);
                        whichArm = whichArm * -1;
                        anim.SetTrigger("RightArm");
                    }
                    else if(whichArm == -1)
                    {
                        Instantiate(shootyBall, new Vector3(leftArm.transform.position.x, leftArm.transform.position.y, leftArm.transform.position.z), leftArm.transform.rotation);
                        whichArm = whichArm * -1;
                        anim.SetTrigger("LeftArm");
                    }
                    audioSource.PlayOneShot(RangeNoise, RangeVolue);
                    attackTimer = 0f;
                }

            }
        }
        else
        {
            patting = true;
        }

    }

    void SetType(int type)
    {
        if (type == 1)
        {
            iAmMelee = true;
            seeDistance = 30f;
            attackDistance = 5f;
        }
        else if (type == 2)
        {
            iAmRanged = true;
            seeDistance = 40f;
            attackDistance = 20f;
        }
    }


}
