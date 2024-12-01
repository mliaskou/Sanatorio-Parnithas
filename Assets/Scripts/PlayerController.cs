using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject raycastedObj;

    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private Image uiCrosshair;
    [SerializeField] private Text interactableText;

    SoundPlay soundplay;

    public static int count = 0;
    Color interactabletextcolor = new Color32(255, 95, 8, 255);

    [SerializeField] private Text txt;
    void Start()
    {
        interactableText = GameObject.Find("InteractableText").GetComponent<Text>();
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
                float distance = Vector3.Distance(raycastedObj.gameObject.transform.position, transform.position);
                soundplay = raycastedObj.GetComponent<SoundPlay>();
                if (distance < 5f)
                {
                    interactableText.gameObject.SetActive(true);
                    CrosshairActive();
                    soundplay.ShowNarrativeCanvas();
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        soundplay.PlayAudio();
                    }
                }
            }
        }

        else
        {
            CrosshairNormal();
            interactableText.gameObject.SetActive(false);
        }

        if (raycastedObj != null)
        {
            if (Vector3.Distance(raycastedObj.transform.position, transform.position) > 5f)
            {
                if (soundplay != null)
                {
                    soundplay.StopAudio();
                }
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
