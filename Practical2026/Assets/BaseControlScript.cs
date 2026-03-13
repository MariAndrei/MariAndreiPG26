using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
using Unity.VisualScripting;

public class BaseControlScript : MonoBehaviour
{

    private float range = 100f;
    private float rayDistance = 3.5f;
    private float damage = 25f;

    float cooldown = 1.5f;
    float leftCooldown = 0f;
    float rightCooldown = 0f;

    Rigidbody rb;

    private float groundDrag = 1;

    private float jumpForce = 10;
    private float jumpCooldown = 2;
    bool readyToJump;

    GameObject crosshairs;

    float speed = 5f;
    float turnSpeed = 45f;
    Vector3 turretTarget;

    private float playerHeight = 4;
    public LayerMask whatIsGround;
    bool grounded;
    AnimationClip[] clips;
    Animator leftGunAnim, rightGunAnim;

    float resetAnimation = 0.1f;

    public Image IMG1;
    public Image IMG2;

    Transform turret, leftGun, rightGun, gunMounting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();

        readyToJump = true;

        Animator[] animators = GetComponentsInChildren<Animator>();
        leftGunAnim = animators[0];
        rightGunAnim = animators[1];

        //crosshairs = GameObject.Find("Crosshair");
        //if (crosshairs != null) { print("found"); }
        //UnityEngine.UI.Image ch = crosshairs.GetComponent<UnityEngine.UI.Image>();


        //ch.color = new Color(0, 0, 1, 0.7f);
        turretTarget = Camera.main.transform.position + Camera.main.transform.forward * 1000f;
        turret = transform.GetChild(1);
        gunMounting = turret.GetChild(2);
        leftGun = gunMounting.GetChild(0);
        rightGun = gunMounting.GetChild(1);
        //print(turret.name);
        //print(leftGun.name);
        //print(rightGun.name);




    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        leftCooldown -= Time.deltaTime;
        rightCooldown -= Time.deltaTime;
        
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

        if (Input.GetKeyDown(KeyCode.Space) && readyToJump && grounded)
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


        //SHOOTING  



        if (Input.GetMouseButtonDown(0) && leftCooldown <= 0)
        {
            leftGunAnim.SetBool("Fire2", true);
            leftCooldown = cooldown;
            if (Physics.Raycast(Camera.main.transform.position + transform.forward * rayDistance, Camera.main.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);


                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {

                    target.takeDamage(damage);

                }
            }
        }
        if ((cooldown - leftCooldown) > resetAnimation) { leftGunAnim.SetBool("Fire2", false); }

        IMG1.fillAmount = Mathf.Clamp((cooldown - leftCooldown)/cooldown, 0, 1);

        
        if (Input.GetMouseButtonDown(1) && rightCooldown <= 0)
        {
            rightGunAnim.SetBool("Fire", true);
            rightCooldown = cooldown;
            if (Physics.Raycast(Camera.main.transform.position + transform.forward * rayDistance, Camera.main.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);


                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {

                    target.takeDamage(damage);

                }
            }
        }
        if ((cooldown - rightCooldown) > resetAnimation) { rightGunAnim.SetBool("Fire", false); }

        IMG2.fillAmount = Mathf.Clamp((cooldown - rightCooldown) / cooldown, 0, 1);
        //JUMP 

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 1f + 0.1f, whatIsGround);

        //-- Checks for RayCast and also prints if the tank is grounded.
        //Debug.DrawRay(transform.position, (playerHeight * 0.5f + 0.3f) * Vector3.down);
        //print(grounded);

        //Jump keybind, checks if its grounded and the cooldown
        

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
