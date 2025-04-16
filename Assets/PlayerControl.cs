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
    private float yatiklik = 0f;  //0 ile 1 arasnda olsn.         //Kanatlarin belli bir derece yanlara yatmasi icin lerp ile kullanilacak

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
        w = inputAction.PlayerKeyboard.W.ReadValue<float>();
        a = inputAction.PlayerKeyboard.A.ReadValue<float>();
        s = inputAction.PlayerKeyboard.S.ReadValue<float>();
        d = inputAction.PlayerKeyboard.D.ReadValue<float>();
        d = inputAction.PlayerKeyboard.D.ReadValue<float>();
       
        if(a>0 || d>0) yatiklik = Mathf.Lerp(1, 0f,100* Time.deltaTime);
        
    }

    private void FixedUpdate()
    {
        
        var deltaRotation = Rotate(w, -a, a*yatiklik, xRotateSpeed);
        rb.MoveRotation(rb.rotation * deltaRotation);
        var deltaRotation2 = Rotate(-s, d, -d*yatiklik, zRotateSpeed);
        rb.MoveRotation(rb.rotation * deltaRotation2);
        rb.linearVelocity = transform.forward * linearSpeed;//Carpisma olacaksa. 
        //rb.MovePosition(rb.position + transform.forward * linearSpeed * Time.fixedDeltaTime);//Carpisma olmayacaksa.
    }

    Quaternion Rotate(float x, float y, float z, float rotateSpeed)
    {
        

        var rotateVector = new Vector3(x, y, z);
        rotateVector = rotateVector * rotateSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(rotateVector);
        return deltaRotation;
    }
}
