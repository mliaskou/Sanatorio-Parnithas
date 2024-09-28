using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject raycastedObj;
    private GameObject soundObject=null;

    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private Image uiCrosshair;
    [SerializeField] private Text interactableText;

    public NarrativesInventory _NarrativesInventory;
    SoundPlay soundplay;

    public static int count = 0;
    Color interactabletextcolor = new Color32(255, 95, 8, 255);

    [SerializeField] private Text txt;

    private SoundPlay s;
    void Start()
    {
        interactableText = GameObject.Find("InteractableText").GetComponent<Text>();
        _NarrativesInventory = GameObject.Find("NarrativeInventory").GetComponent<NarrativesInventory>();
    }


    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {
            if (hit.collider.CompareTag("Audio"))
            {
                raycastedObj = hit.collider.gameObject;
                soundObject = hit.collider.gameObject;
                float distance = Vector3.Distance(raycastedObj.gameObject.transform.position, transform.position);
                soundplay = raycastedObj.GetComponent<SoundPlay>();
                if (distance < 5f)
                {
                    interactableText.gameObject.SetActive(true);
                    CrosshairActive();

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        
                        if (!soundplay._audioHasPlayed)
                        {                          
                            count++;
                            _NarrativesInventory._CountText.text = count.ToString();
                        }
                        soundplay.SoundList();
                    }
                }
            }
        }

        else
        {
            CrosshairNormal();
            interactableText.gameObject.SetActive(false);
        }

        if (soundObject != null)
        {
            if (Vector3.Distance(soundObject.transform.position, transform.position) > 5f)
            {
                soundplay.StopAudio();
                soundObject = null;
            }
        }
    }

    void CrosshairActive()
    {
        uiCrosshair.gameObject.SetActive(true);
        uiCrosshair.color = interactabletextcolor;
    }
    void CrosshairNormal()
    {
        uiCrosshair.gameObject.SetActive(false);
    }
}
