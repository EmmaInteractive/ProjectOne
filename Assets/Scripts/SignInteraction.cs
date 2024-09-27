using UnityEngine;
using TMPro; 

[RequireComponent(typeof(Collider2D))]
public class SignInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private GameObject interactionMessagePrefab; 
    [SerializeField] private string signText = "Welcome to the Adventures Guild"; 
    [SerializeField] private string interactionKey = "e"; 

    private GameObject interactionMessageInstance;
    private TextMeshProUGUI messageText;
    private bool isInRange;

    private void Start()
    {
        if (interactionMessagePrefab != null)
        {
           
            interactionMessageInstance = Instantiate(interactionMessagePrefab, Vector3.zero, Quaternion.identity);
            interactionMessageInstance.transform.SetParent(GameObject.Find("SignTexts")?.transform, false); 

            messageText = interactionMessageInstance.GetComponentInChildren<TextMeshProUGUI>();
            if (messageText != null)
            {
                messageText.text = signText; 
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found on the interactionMessagePrefab.");
            }
            interactionMessageInstance.SetActive(false); 
        }
        else
        {
            Debug.LogError("interactionMessagePrefab is not assigned.");
        }
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(interactionKey))
        {
            throw new System.Exception();
            if (interactionMessageInstance != null)
            {
                bool isActive = interactionMessageInstance.activeSelf;
                interactionMessageInstance.SetActive(!isActive);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (interactionMessageInstance != null)
            {
                interactionMessageInstance.SetActive(false); 
            }
        }
    }
}