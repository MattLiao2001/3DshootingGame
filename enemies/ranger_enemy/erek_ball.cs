using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class erek_ball : MonoBehaviour
{
    public GameObject you;
    GameObject target;
    float nextTimeAttack;
    float healthly;
    public GameObject erekBall;
    public GameObject firePosition;
    
    // Start is called before the first frame update
    void Start()
    {
        healthly = GetComponentInParent<enemy_control>().blood_value;
        target = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {
    if (healthly > 0)
    {
        healthly = GetComponentInParent<enemy_control>().blood_value;
        if (Vector3.Distance(you.transform.position, target.transform.position) <= 10)
            { 
                if (Time.time >= nextTimeAttack)
                { 
                    nextTimeAttack = Time.time + 1.5f;
                  
                    GameObject attackedErekball;
                    attackedErekball = Instantiate(erekBall,
                    firePosition.transform.position, Quaternion.Euler(0, 0, 0));
                    attackedErekball.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
                }
            }
    }
    else
        healthly = 0;
    }
}
