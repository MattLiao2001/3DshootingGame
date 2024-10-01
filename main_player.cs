using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main_player : MonoBehaviour
{
    Animator main_player_animator;
    public float movement_speed = 6f;
    CharacterController main_player_controller;
    Rigidbody body;
    int floor_layer;
    public float blood_value = 1000;
    public float total_blood_value = 1000;
    public float damage = 25;
    public Slider blood_bar;
    public Color hurt_color;
    public Image hurt_image;

    float next_time_rush;
    float current_speed = 6f;

    public GameObject[] tool;

    public bool shieldONOFF;

    public GameObject runEffect;
    public GameObject positionOfEffect;

    public GameObject bonousEffect;


    public void hurt(float damage)
    {
        blood_value -= damage;
        blood_bar.value = blood_value;
        StartCoroutine("hurt_hint");
        if(blood_value <= 0)
        {
            GetComponent<Animator>().SetTrigger("death");
            this.enabled = false;
            blood_bar.fillRect.gameObject.SetActive(false);
        }
    }

    IEnumerator hurt_hint()
    {
        hurt_image.color = hurt_color;
        yield return new WaitForSeconds(0.1f);
        hurt_image.color = Color.clear;
    }

   
    // Start is called before the first frame update
    void Start()
    {
        main_player_animator = GetComponent<Animator>();
        main_player_controller = GetComponent<CharacterController>();
        body = GetComponent<Rigidbody>();
        floor_layer = LayerMask.GetMask("Default");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= next_time_rush)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                next_time_rush = Time.time + 3f;
                movement_speed = 50;
                StartCoroutine("rushOFF");
            }
        }
        float up_and_down = Input.GetAxis("Vertical");
        float left_and_right = Input.GetAxis("Horizontal");
        play_animation(up_and_down, left_and_right);
        //simple_move(up_and_down, left_and_right);
        //controller_move(up_and_down, left_and_right);
        rigid_move(up_and_down, left_and_right);
        turn();
        blood_bar.value = blood_value;
        if (blood_value > total_blood_value)
        {
            blood_value = total_blood_value;
            blood_bar.value = total_blood_value;
        }
        if(damage == 50)
            Instantiate(bonousEffect, positionOfEffect.transform);
    }

        void play_animation(float vertical, float horizontal)
    {
        if (vertical != 0 || horizontal != 0)
        {
            main_player_animator.SetBool("walking", true);
            if (movement_speed == 8f)
                Instantiate(runEffect, positionOfEffect.transform);
        }
        else
            main_player_animator.SetBool("walking", false);
    }

    void turn ()
    {
        float camera_ray_length = 100f;
        Ray camera_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floor_cross;
        if (Physics.Raycast(camera_ray, out floor_cross, camera_ray_length, floor_layer))
        {
            Vector3 player_to_mouse = floor_cross.point - transform.position;
            player_to_mouse.y = 0;
            Quaternion rotation_angle = Quaternion.LookRotation(player_to_mouse);
            body.MoveRotation(rotation_angle);
        }
    }

    void rigid_move(float vertical, float horizontal)
    {
        Vector3 move_value = new Vector3(horizontal, 0, vertical);
        move_value = move_value.normalized * movement_speed * Time.fixedDeltaTime;
        if (move_value != Vector3.zero)
        {
            body.MovePosition(transform.position + move_value);
            body.MoveRotation(Quaternion.LookRotation(move_value));
        }
        else
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("tool"))
        {
            if (collision.gameObject == tool[0])
            {
                blood_value += 500;
                collision.gameObject.SetActive(false);
                StartCoroutine(reborn(collision.gameObject));
            }
            else if (collision.gameObject == tool[1])
            {
                shieldONOFF = true;
                collision.gameObject.SetActive(false);
                StartCoroutine(reborn(collision.gameObject));
                StartCoroutine("shieldOFF");
            }
            else if (collision.gameObject == tool[2])
            {
                movement_speed = 8f;
                current_speed = 8f;

                Instantiate(runEffect, positionOfEffect.transform);
                collision.gameObject.SetActive(false);
                StartCoroutine(reborn(collision.gameObject));
                StartCoroutine("slow");
            }
            else if (collision.gameObject == tool[3]) {
                damage = 50;
                Instantiate(bonousEffect, positionOfEffect.transform);
                collision.gameObject.SetActive(false);
                StartCoroutine(reborn(collision.gameObject));
                StartCoroutine("turnDown");
            }

        }
    }

    IEnumerator slow()
    {
        yield return new WaitForSeconds(10);
        movement_speed = 6f;
        current_speed = 6f;
    }
    IEnumerator rushOFF()
    {
        yield return new WaitForSeconds(0.12f);
        movement_speed = current_speed;
    }

    IEnumerator reborn(GameObject rebornObject)
    {
        yield return new WaitForSeconds(10);
        rebornObject.gameObject.SetActive(true);
    }

    IEnumerator shieldOFF()
    {
        yield return new WaitForSeconds(10);
        shieldONOFF = false;
    }

    IEnumerator turnDown()
    {
        yield return new WaitForSeconds(10);
        damage = 25;
    }
}
