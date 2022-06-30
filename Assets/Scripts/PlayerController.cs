using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject raycastedObj;
    
    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private Image uiCrosshair;
    [SerializeField] private Text interactableText;
    private AudioSource sound;

    public Text CountText;
    public static int count =0;
    

    [SerializeField] private Text txt;

    private SoundPlay s;
    void Start()
    {
        interactableText = GameObject.Find("InteractableText").GetComponent<Text>();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if(Physics.Raycast(transform.position, fwd,out hit, rayLength, layerMaskInteract.value))
        {
            Debug.Log("RAYCAST");
            
            if (hit.collider.CompareTag("Audio"))
            {
                raycastedObj = hit.collider.gameObject;
                float distance = Vector3.Distance(raycastedObj.gameObject.transform.position, transform.position);
                

                if(distance < 5f)
                {
                    interactableText.gameObject.SetActive(true);
                    CrosshairActive();
                    if (Input.GetKeyDown(KeyCode.E))
                    {

                        Debug.Log("I have interacted with an object");
                        sound = raycastedObj.GetComponent<AudioSource>();
                        sound.Play();
                        s = raycastedObj.GetComponent<SoundPlay>();
                        s.SoundList();
                        count++;
                        CountText.text = count.ToString();

                    }
                }
            }
        }
    
        else 
        {
            CrosshairNormal();
            interactableText.gameObject.SetActive(false);
            
        }
    }

    void CrosshairActive()
    {
        uiCrosshair.color = Color.red;
    }
    void CrosshairNormal()
    {
        uiCrosshair.color = Color.white;
    }

 

}
