using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMove : MonoBehaviour
{
    [SerializeField]float speed = 3;
    Vector3 direction;
    bool inLight;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            inLight = true;
            Vector3 dir = transform.position - other.transform.position;
            dir.y = 0;
            dir.Normalize();
            if (dir == Vector3.zero)
            {
                dir.z = 1;
            }
            direction = dir;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            inLight = false;
            direction = Vector3.zero;
        }
    }
}
