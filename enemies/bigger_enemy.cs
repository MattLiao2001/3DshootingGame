using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class biigger : MonoBehaviour
{
    public GameObject children;
    float healthly;
    int a = 0; 


    // Start is called before the first frame update
    void Start()
    {

        healthly = GetComponentInParent <enemy_control> ().blood_value;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (healthly > 0)
            healthly = GetComponentInParent<enemy_control>().blood_value;
        else
        {
            a++;
            if (a == 1)
            {

                divide();
                gameObject.transform.parent = null;
            }
        }
    }
    void divide()
    {

        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        Instantiate(children, currentPosition, currentRotation);
        Instantiate(children, currentPosition, currentRotation);
    }
}
