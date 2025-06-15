using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private GameObject raycastedObj;
    [SerializeField] private int rayLength = 10;

    Image _interactableImage;
    Text interactableText;
    SoundPlay soundplay;

    Color interactableTextColor;
    public IEnumerator Initialise(GameObject interactable)
    {
        interactable.SetActive(true);
        interactableText = interactable.GetComponent<InteractableReferencies>()._InteractableText;
        _interactableImage = interactable.GetComponent<InteractableReferencies>()._InteractableImage;
        interactableTextColor = GameStateManager._Instance._UISettings._InteractableTextColor;
        yield return null;
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength))
        {
            if (hit.collider.CompareTag("Audio"))
            {
                raycastedObj = hit.collider.gameObject;
                string name = raycastedObj.name;
                float distance = Vector3.Distance(raycastedObj.gameObject.transform.position, transform.position);
                soundplay = raycastedObj.GetComponent<SoundPlay>();

                CrosshairActive();
                if (distance < 10f)
                {            
                    if (Input.GetKeyDown(KeyCode.E))
                    {                     
                        soundplay.PlayAudio();
                        soundplay.ShowNarrativeCanvas(name);
                    }
                }
            }
            else
            {
                if (_interactableImage != null)
                {
                    CrosshairNormal();
                }
            }
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
        _interactableImage.gameObject.SetActive(true);
        _interactableImage.color = interactableTextColor;
        interactableText.color = interactableTextColor;
    }
    void CrosshairNormal()
    {
        _interactableImage.gameObject.SetActive(false);
    }
}
