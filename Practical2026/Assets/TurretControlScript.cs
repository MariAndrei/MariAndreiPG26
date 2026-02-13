using UnityEngine;
using UnityEngine.Rendering;

public class TurretControlScript : MonoBehaviour
{
    //private float damage = 50f; 
    private float range = 100f;
    private float rayDistance = 3.5f;
    private float damage = 25f;


    void Update()
    {

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
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

        if (Input.GetMouseButtonDown(1))
        {

            if (Physics.Raycast(Camera.main.transform.position+transform.forward * rayDistance, Camera.main.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                Target target = hit.transform.GetComponent<Target>();
                if (target != null) {

                    target.takeDamage(damage);
                    
                }

            }

        }
    }
}
