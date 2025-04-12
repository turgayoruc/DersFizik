using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    private MyInputAction inputAction;

    float w;
    float a;
    float s;
    float d;

    public float xRotateSpeed = 10;
    public float zRotateSpeed = 10;
    public float linearSpeed = 1000f;


    Rigidbody rb;


    void Awake()
    {
        inputAction = new MyInputAction();
    }

    void OnEnable()
    {
        inputAction.Enable();

    }
    void OnDisable()
    {
        inputAction.Disable();
    }
    void Start()
    {

        rb = GetComponent<Rigidbody>();
      
    }

    // Update is called once per frame
    void Update()
    {
        w = inputAction.Player.W.ReadValue<float>();
        a = inputAction.Player.A.ReadValue<float>();
        s = inputAction.Player.S.ReadValue<float>();
        d = inputAction.Player.D.ReadValue<float>();

        var deltaRotation= Rotate(w, -a,0,xRotateSpeed);
        rb.MoveRotation(rb.rotation * deltaRotation); 
         var deltaRotation2= Rotate(-s, d,0,zRotateSpeed);
        rb.MoveRotation(rb.rotation * deltaRotation2); 

        
       
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + transform.forward * linearSpeed * Time.fixedDeltaTime);
    }

    Quaternion Rotate(float x, float y, float z, float rotateSpeed)
    {
        var rotateVector = new Vector3(x, y, z);
        rotateVector = rotateVector * rotateSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(rotateVector);
        return deltaRotation;
    }
}
