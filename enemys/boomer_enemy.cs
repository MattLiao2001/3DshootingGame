using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomer : MonoBehaviour
{
    float healthly;
    int a = 0;
    GameObject target;
    public GameObject you;
    public GameObject boomEffect;
    
    bool shieldONOFF;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        healthly = GetComponent<enemy_control>().blood_value;
    }

    // Update is called once per frame
    void Update()
    {
        shieldONOFF = target.GetComponent<main_player>().shieldONOFF;
        if (healthly > 0)
            healthly = GetComponentInParent<enemy_control>().blood_value;
        else {
            healthly = 0;
            boom();
            a++;
        }
    }
    void boom()
    {
        if (a == 1) 
        {
            Instantiate(boomEffect, you.transform);

            if (Vector3.Distance(you.transform.position, target.transform.position) < 3 && shieldONOFF == false)
            {
                target.GetComponent<main_player>().hurt(200);
            }
        }
    }
}
