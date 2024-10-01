using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class sound_controller : MonoBehaviour
{
    public AudioMixer mix;
    public Canvas control_panel;
    bool pause = false;

    public void set_background_sound (float value)
    {
        mix.SetFloat("background_sound_value", value);

    }

    public void set_sound_effect(float value)
    {
        mix.SetFloat("sound_effect_value", value);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pause)
            {
                Time.timeScale = 0;
                control_panel.gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                control_panel.gameObject.SetActive(false);
            }
            pause = !pause;
        }
        float adjust = 0;
        if (Input.GetKey(KeyCode.PageUp))
            adjust = Time.deltaTime * 5f;
        if (Input.GetKey(KeyCode.PageDown))
            adjust = -Time.deltaTime * 5f;
        if(adjust != 0)
        {
            float now_value;
            mix.GetFloat("main_sound_value", out now_value);
            now_value = Mathf.Clamp(now_value + adjust, -80, 20);
            mix.SetFloat("main_sound_value", now_value);
        }
    }
}
