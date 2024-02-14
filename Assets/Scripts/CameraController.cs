using UnityEngine;

//public class CameraController : MonoBehaviour
//{
//    Camera camera;

//    float lookSpeed = 3;
//    Vector2 rotation = Vector2.zero;

//    private void Awake()
//    {
//        camera = GetComponent<Camera>();
//        print("Camera name: " + camera.name);

//        Cursor.visible = false;
//    }

//    //public void Update()
//    //{
//    //    rotation.y += Input.GetAxis("Horizontal");
//    //    rotation.x += -Input.GetAxis("Vertical");
//    //    print("Y: " + rotation.y + ". X: " + rotation.x);
//    //    rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
//    //    transform.eulerAngles = new Vector2(0, rotation.y) * lookSpeed;
//    //    camera.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
//    //}

//    float horizontalSpeed = 2.0f;
//    float verticalSpeed = 2.0f;

//    void Update()
//    {
//        // Get the mouse delta. This is not in the range -1...1
//        float h = horizontalSpeed * Input.GetAxis("Mouse X");
//        float v = verticalSpeed * Input.GetAxis("Mouse Y");

//        transform.Rotate(v, h, 0);
//    }
//}



public class CameraController : MonoBehaviour
{
    [Space, Header("Look Settings")]
    [SerializeField] private Vector2 sensitivity = Vector2.zero;
    [SerializeField] private Vector2 smoothAmount = Vector2.zero;
    [SerializeField] private Vector2 lookAngleMinMax = Vector2.zero; // MIN/MAX -90/90

    private float m_yaw;
    private float m_pitch;

    private float m_desiredYaw;
    private float m_desiredPitch;

    private Transform m_pitchTranform;
    private Camera m_cam;

    void Awake()
    {
        GetComponents();
        InitValues();
        ChangeCursorState();
    }

    void LateUpdate()
    {
        CalculateRotation();
        SmoothRotation();
        ApplyRotation();
    }

    void GetComponents()
    {
        m_pitchTranform = transform.GetChild(0).transform;
        m_cam = GetComponentInChildren<Camera>();
    }

    void InitValues()
    {
        m_yaw = transform.eulerAngles.y;
        m_desiredYaw = m_yaw;
    }

    void CalculateRotation()
    {
        m_desiredYaw += Input.GetAxis("Mouse X") * sensitivity.x * Time.deltaTime;
        m_desiredPitch -= Input.GetAxis("Mouse Y") * sensitivity.y * Time.deltaTime;

        m_desiredPitch = Mathf.Clamp(m_desiredPitch, lookAngleMinMax.x, lookAngleMinMax.y);
    }

    void SmoothRotation()
    {
        m_yaw = Mathf.Lerp(m_yaw, m_desiredYaw, smoothAmount.x * Time.deltaTime);
        m_pitch = Mathf.Lerp(m_pitch, m_desiredPitch, smoothAmount.y * Time.deltaTime);
    }

    void ApplyRotation()
    {
        transform.eulerAngles = new Vector3(0f, m_yaw, 0f);
        m_pitchTranform.localEulerAngles = new Vector3(m_pitch, 0f, 0f);
    }

    void ChangeCursorState()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

