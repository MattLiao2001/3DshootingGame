using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class level_contriol : MonoBehaviour
{
    int a = 1;
    public Text level;
    // Start is called before the first frame update
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time == 0) 
        { 
            StartCoroutine("gone");
            level.text = "LEVEL " + a.ToString();
        }
        }
    IEnumerator gone()
    {
        yield return new WaitForSeconds(5f);
        level.enabled = false;
       
        StartCoroutine("found");
    }

    IEnumerator found()
    {
        yield return new WaitForSeconds(5f);
           a++;
           
        
        level.text = "LEVEL " + a.ToString();
        
        level.enabled = true;
        StartCoroutine("gone");
    }
}
