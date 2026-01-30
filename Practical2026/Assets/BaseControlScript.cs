using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseControlScript : MonoBehaviour
{
    Rigidbody rb;

    private float groundDrag = 1;

    private float jumpForce = 5;
    private float jumpCooldown = 1;
    bool readyToJump;


    float speed = 5f;
    float turnSpeed = 45f;
    Vector3 turretTarget;

    private float playerHeight = 4;
    public LayerMask whatIsGround;
    bool grounded;

    Transform turret, leftGun, rightGun, gunMounting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        readyToJump = true;

        turretTarget = Camera.main.transform.position + Camera.main.transform.forward * 1000f;
        turret = transform.GetChild(1);
        gunMounting = turret.GetChild(0);
        leftGun = gunMounting.GetChild(0);
        rightGun = gunMounting.GetChild(1);
        print(turret.name);
        print(leftGun.name);
        print(rightGun.name);


    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += speed * transform.forward * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= speed * transform.forward * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
            {

            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }

        //JUMP 

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.8f + 0.3f, whatIsGround);

        //-- Checks for RayCast and also prints if the tank is grounded.
        //Debug.DrawRay(transform.position, (playerHeight * 0.5f + 0.3f) * Vector3.down);
        //print(grounded);

        //Jump keybind, checks if its grounded and the cooldown
        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {

            readyToJump = true;

            Jump();

            Invoke(nameof(resetJump), jumpCooldown);

        }

        

        // Handling drag 
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;


        turretTarget = RotatePointAroundPivot(turretTarget, turret.position, Quaternion.AngleAxis(Input.GetAxis("THorizontal") * turnSpeed * Time.deltaTime, transform.up));

        
        turretTarget = RotatePointAroundPivot(turretTarget, turret.position, Quaternion.AngleAxis(Input.GetAxis("TVertical") * turnSpeed * Time.deltaTime,turret.right ));
        turret.LookAt(new Vector3(turretTarget.x, turret.position.y, turretTarget.z), transform.up);
        
        gunMounting.LookAt(turretTarget);


    }

    //Method called after the jump is activated, with a delay by the JumpCooldown.
    private void resetJump()
    {
        readyToJump = true;
    }

    //Jump boost formula
    private void Jump()
    {
        // reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        // 1. Get the direction from the pivot to the point
        Vector3 direction = point - pivot;

        // 2. Rotate that direction
        direction = rotation * direction;

        // 3. Add it back to the pivot to get the new point
        return pivot + direction;
    }

}
