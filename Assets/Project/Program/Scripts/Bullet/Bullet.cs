using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(Collider))]
public class Bullet : MonoBehaviour
{
    Rigidbody _rigidbody;
    [SerializeField]float _speed;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Setup(Vector3 pos, Vector3 dir, float speed)
    {
        transform.position = pos;
        transform.rotation = Quaternion.LookRotation(dir);
        _speed = speed;
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = _speed * Time.fixedDeltaTime * transform.forward;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            // var player = GetComponent<Player>(); 
            // player.TakeDamage();
            BulletManager.Instance.BulletPool.Release(this);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Stage"))
        {
            BulletManager.Instance.BulletPool.Release(this);
        }
    }
}