using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundPlay : MonoBehaviour
{
    public AudioSource source;
    public int count;
   


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
   public void Play()
    {
        source.Play();
        
    }

    public void SoundList()
    {
        SoundsArray s = GameObject.FindObjectOfType<SoundsArray>();
        s.Array[count - 1] = true;
        s.ArraySound();
    }

}
