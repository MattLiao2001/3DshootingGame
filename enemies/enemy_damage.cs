using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_damage : MonoBehaviour
{
    public float damage = 50;
    GameObject attacked_target;
    float damage_interval;
    // Start is called before the first frame update
    void Start()
    {
        attacked_target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision hit_object)
    {
        if (hit_object.gameObject == attacked_target && Time.time >= damage_interval)
        {
            var playerComponent = hit_object.collider.gameObject.GetComponent<main_player>();
            if (playerComponent == null)
            {
                Debug.LogError("main_player component missing on the collided object.");
                return;
            }
            if (playerComponent.shieldONOFF == false)
            {
                damage_interval = Time.time + 0.5f;
                playerComponent.hurt(damage);
            }

            if (playerComponent.blood_value <= 0)
            {
                GetComponent<Animator>().SetTrigger("player_dead");
                this.enabled = false;
            }
        }
    }

}
