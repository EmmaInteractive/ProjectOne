using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float vertifalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, vertifalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
    }
}
