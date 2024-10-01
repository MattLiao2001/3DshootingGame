using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_control : MonoBehaviour
{
    GameObject track_target;
    NavMeshAgent navigator;
    Animator animator;
    public float blood_value = 100;
    public float movement_speed = 6f;
    float base_speed;
    bool isDead = false;



    AudioSource[] hitted_sound;


    public void loose_health(float damage)
    {
        if (isDead) return; // Prevent further damage if already dead

        hitted_sound[0].Play();
        blood_value -= damage;

        if (blood_value <= 0)
        {
            isDead = true; // Set the flag to dead state
            GetComponent<Collider>().enabled = false; // Disable collider to stop further raycast interactions

            GetComponent<Light>().enabled = false;
            GetComponentInChildren<SkinnedMeshRenderer>().materials[0].DisableKeyword("_EMISSION");
            hitted_sound[1].Play(); // Play death sound
            animator.SetTrigger("enemy_dead"); // Play death animation

            this.enabled = false;
            navigator.enabled = false;

            StartCoroutine("gone");
        }
    }
    IEnumerator gone()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        track_target = GameObject.FindGameObjectWithTag("Player");
        navigator = GetComponent<NavMeshAgent>();
        navigator.stoppingDistance = GetComponent<CapsuleCollider>().radius + track_target.GetComponent<CapsuleCollider>().radius;
        animator = GetComponent<Animator>();
        hitted_sound = GetComponents<AudioSource>();
        base_speed = movement_speed;
        navigator.speed = base_speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(track_target.GetComponent<main_player>().blood_value > 0)
            navigator.SetDestination(track_target.transform.position);
        else
        {
            animator.SetBool("player_dead", true);
            this.enabled = false;
        }
    }

    public void slow()
    {
        navigator.speed = base_speed - 2f;
        StartCoroutine("fast");
    }
  
    IEnumerator fast()
    {
        yield return new WaitForSeconds(0.5f);
        navigator.speed = base_speed;
    }
    
    
}
