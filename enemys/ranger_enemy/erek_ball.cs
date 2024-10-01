using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ereck_ball_control : MonoBehaviour
{
    public float exist_time = 1.5f;
    float end_time;
    // Start is called before the first frame update
    void Start()
    {
        end_time = Time.time + exist_time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= end_time)
            Destroy(gameObject);

        
    }
    
    private void OnCollisionEnter(Collision hitted)
    {

        if (hitted.gameObject.CompareTag("Player"))
        {
            if(hitted.gameObject.GetComponent<main_player>().shieldONOFF == false)
                hitted.collider.gameObject.GetComponent<main_player>().hurt(100);
            Destroy(gameObject);
        }
        
    }
    
}
