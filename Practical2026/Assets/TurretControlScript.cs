using UnityEngine;
using UnityEngine.Rendering;

public class TurretControlScript : MonoBehaviour
{
    //private float damage = 50f; 
    private float range = 100f;


    void Update()
    {

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range)) 
            {
                Debug.Log(hit.transform.name);
            }

        }

        if (Input.GetMouseButtonDown(1))
        {

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
            }

        }
    }
}
