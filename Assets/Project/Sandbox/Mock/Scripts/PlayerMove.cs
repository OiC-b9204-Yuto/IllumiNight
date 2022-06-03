using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed = 5;
    float angle = 0;
    Vector3 _direction;
    Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // 移動
        float inputV = Input.GetAxisRaw("Vertical");
        float inputH = Input.GetAxisRaw("Horizontal");
        _direction = new Vector3(inputH, 0, inputV);
        // 回転

        float inputMouse = Input.GetAxisRaw("Mouse X");

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + inputMouse, transform.localEulerAngles.z);

        //エディタでの実行時、マウス移動の中心点が右上になってる
        //Vector3 inputMousePos = Input.mousePosition;
        //Vector3 forward = inputMousePos - new Vector3(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2, 0);
        //forward.Normalize();

        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, (Mathf.Atan2(-forward.y, forward.x)+Mathf.PI * 0.5f) * Mathf.Rad2Deg , transform.localEulerAngles.z);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }

    }

    void FixedUpdate()
    {
        _rigidbody.velocity = transform.rotation * _direction.normalized * speed;
    }
}
