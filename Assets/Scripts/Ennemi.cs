using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Ennemi : MonoBehaviour
{
    [SerializeField]
    private GameObject cible;



    private NavMeshAgent agent;

    public float distanceThereshold = 10f;

    public enum AIState { idle, chasing, attack, receive, dead }

    public AIState state = AIState.idle;

    public float attackThereshold = 2f;

    [SerializeField]
    public float dist;

    public HealthManager healthManager;

    public float health = 100f;
    public float maxHealth = 100f;

    public Image healthBarImage;

    //Personnage mort??
    public bool isDead = false;

    public void ReceiveDammage(float theDammage)
    {
        health = health - theDammage;

        if (health <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        isDead = true;
        agent.GetComponent<Animator>().SetBool("Dead", true);
        agent.isStopped = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        
            StartCoroutine(Think());
       
    }
    // Update is called once per frame
    void Update()
    {
        healthBarImage.fillAmount = health / maxHealth;
    }
    IEnumerator Think()
    {
        
            while (true)
            {
            if (!isDead)
            {
                cible = GameObject.FindGameObjectWithTag("Player");
                switch (state)
                {
                    case AIState.idle:
                        dist = Vector3.Distance(cible.transform.position, agent.transform.position);
                        if (dist < distanceThereshold)
                        {
                            state = AIState.chasing;
                            agent.GetComponent<Animator>().SetBool("Chasing", true);
                        }
                        agent.SetDestination(transform.position);

                        break;
                    case AIState.chasing:
                        dist = Vector3.Distance(cible.transform.position, agent.transform.position);
                        if (dist > distanceThereshold)
                        {
                            state = AIState.idle;
                            agent.GetComponent<Animator>().SetBool("Chasing", false);
                        }
                        if (dist < attackThereshold)
                        {
                            state = AIState.attack;
                            agent.GetComponent<Animator>().SetBool("Attacking", true);
                        }
                        agent.SetDestination(cible.transform.position);
                        //Destination(cible.transform.position);
                        break;
                    case AIState.attack:
                        dist = Vector3.Distance(cible.transform.position, agent.transform.position);
                        if (dist > attackThereshold)
                        {
                            state = AIState.chasing;
                            agent.GetComponent<Animator>().SetBool("Attacking", false);
                        }
                        //if (dist < attackThereshold)
                        //{
                        //    ReceiveDammage(5);
                        //}
                        agent.SetDestination(cible.transform.position);
                        healthManager.ApplyDammage(1.5f);
                        //Destination(cible.transform.position);
                        break;
                    default:
                        break;
                }
            }
                yield return new WaitForSeconds(0.2f);
            }
        


    }

    private void Destination(Vector3 pos)
    {
        if(Vector3.Distance(pos, agent.transform.position) > 2f)
        {
            agent.SetDestination(pos);

        }
        else
        {
            agent.isStopped = true;
        }
    }
}
