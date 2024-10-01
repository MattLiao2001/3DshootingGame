using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_ball : MonoBehaviour
{
    float healthly;
    public GameObject erekBall;
    public GameObject firePosition;

    // Start is called before the first frame update
    void Start()
    {
        healthly = GetComponentInParent<enemy_control>().blood_value;

        GameObject attackedErekball;
        attackedErekball = Instantiate(erekBall,
        firePosition.transform.position, Quaternion.Euler(0, 0, 0));
        attackedErekball.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        StartCoroutine("fire");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator fire()
    {
        if (healthly >= 0)
        {
            yield return new WaitForSeconds(1.5f);
            
            GameObject attackedErekball;
            attackedErekball = Instantiate(erekBall,
            firePosition.transform.position, Quaternion.Euler(0, 0, 0));
            attackedErekball.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
            StartCoroutine("fire");
        }
    }
}
