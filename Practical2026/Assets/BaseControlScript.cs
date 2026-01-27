using UnityEngine;
using UnityEngine.UIElements;

public class BaseControlScript : MonoBehaviour
{

    float speed = 5f;
    float turnSpeed = 45f;
    Vector3 turretTarget;

    Transform turret, leftGun, rightGun, gunMounting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turretTarget = Camera.main.transform.position + Camera.main.transform.forward * 1000f;
        turret = transform.GetChild(0);
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

        
        turretTarget = RotatePointAroundPivot(turretTarget, turret.position, Quaternion.AngleAxis(Input.GetAxis("THorizontal") * turnSpeed * Time.deltaTime, transform.up));

        
        turretTarget = RotatePointAroundPivot(turretTarget, turret.position, Quaternion.AngleAxis(Input.GetAxis("TVertical") * turnSpeed * Time.deltaTime,turret.right ));
        turret.LookAt(new Vector3(turretTarget.x, turret.position.y, turretTarget.z), transform.up);
        
        gunMounting.LookAt(turretTarget);










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
