using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    ParticleSystem fire_effect;
    public float shoot_range = 1000f;
    LineRenderer ray_effect;
    Ray shooting_ray;
    public float shoot_interval = 0.2f;
    public float effect_duration = 0.05f;
    float effect_endtime;
    float next_shoot_time;

    int shootable_object;
    RaycastHit shoot_point;


    public Light gunEnd_fire, face_light;
    public ParticleSystem hit_effect;

    AudioSource gund_sound;
    // Start is called before the first frame update
    void Start()
    {
        fire_effect = GetComponentInChildren<ParticleSystem>();
        ray_effect = GetComponent<LineRenderer>();
        shootable_object = LayerMask.GetMask("Shootable");
        gund_sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= next_shoot_time && GetComponentInParent<main_player>().blood_value > 0 && Time.timeScale > 0)
            start_shooting();
        if (Time.time >= effect_endtime)
        {
            ray_effect.enabled = false;
            gunEnd_fire.enabled = false;
            face_light.enabled = false;
        }
        
    }

    void start_shooting()
    {
        effect_endtime = Time.time + effect_duration;
        next_shoot_time = Time.time + shoot_interval;
        gund_sound.Play();
        gunEnd_fire.enabled = true;
        face_light.enabled = true;
        fire_effect.Stop();
        fire_effect.Play();
        ray_effect.enabled = true;
        shooting_ray.origin = transform.position;
        shooting_ray.direction = transform.forward;
        ray_effect.SetPosition(0, transform.position);
        ray_effect.SetPosition(1, shooting_ray.origin + shooting_ray.direction * shoot_range);
        if (Physics.Raycast(shooting_ray, out shoot_point, shoot_range, shootable_object))
        {
            ParticleSystem play_effect = Instantiate(hit_effect, shoot_point.point, Quaternion.Euler(shoot_point.normal), shoot_point.collider.transform);
            StartCoroutine("play_hit_effect", play_effect);
            ray_effect.SetPosition(1, shoot_point.point);

            var enemy_control = shoot_point.collider.GetComponent<enemy_control>();
            if (enemy_control != null)
            {
                enemy_control.loose_health(20);
                enemy_control.slow();
            }
            else
                ray_effect.SetPosition(1, shooting_ray.origin + shooting_ray.direction * shoot_range);

        }
    }

    IEnumerator play_hit_effect(ParticleSystem effect)
    {
        effect.Play();
        yield return new WaitForSeconds(1);
        if(effect != null)
            Destroy(effect.gameObject);
    }

}
