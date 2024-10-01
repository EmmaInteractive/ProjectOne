using UnityEngine;

public class InteractionMessageController : MonoBehaviour
{
    [SerializeField] private RectTransform interactionMessageRectTransform;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject player;

    private RectTransform canvasRectTransform;

    void Start()
    {
        canvasRectTransform = canvas.GetComponent<RectTransform>();
        if (interactionMessageRectTransform != null)
        {
            interactionMessageRectTransform.gameObject.SetActive(false); 
        }
    }

    void Update()
    {
        if (interactionMessageRectTransform != null && player != null)
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(player.transform.position);
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPosition, mainCamera, out localPoint);
            interactionMessageRectTransform.localPosition = localPoint;

            
            if (Input.GetKeyDown(KeyCode.E) && interactionMessageRectTransform.gameObject.activeSelf)
            {
                interactionMessageRectTransform.gameObject.SetActive(false);
            }
        }
    }
}