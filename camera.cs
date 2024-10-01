using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject target;
    public float following_speed;
    Vector3 displacement;
    // Start is called before the first frame update
    void Start()
    {
        displacement = transform.position - target.transform.position;
        transform.parent = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 position_camera = target.transform.position + displacement;
        transform.position = Vector3.Lerp(transform.position, position_camera, following_speed * Time.deltaTime);
    }
}
