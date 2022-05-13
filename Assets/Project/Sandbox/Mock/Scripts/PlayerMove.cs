using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed = 5;
    float angle = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ˆÚ“®
        float inputV = Input.GetAxisRaw("Vertical");
        float inputH = Input.GetAxisRaw("Horizontal");
        Move(new Vector3(inputH, 0, inputV));
        // ‰ñ“]

        ;
        Vector3 inputMousePos = Input.mousePosition;
        Vector3 forward = inputMousePos - new Vector3(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2, 0);
        forward.Normalize();

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, (Mathf.Atan2(-forward.y, forward.x)+Mathf.PI * 0.5f) * Mathf.Rad2Deg , transform.localEulerAngles.z);
    }

    void Move(Vector3 dir)
    {
        transform.position += dir.normalized * speed * Time.deltaTime;
    }
}
