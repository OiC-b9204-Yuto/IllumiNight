using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(Collider))]
public class Bullet : MonoBehaviour
{
    Rigidbody _rigidbody;
    [SerializeField] float _speed;
    public bool _swing;

    float timer;
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
        if (_swing)
        {
            timer += Time.fixedDeltaTime;
            _rigidbody.velocity += transform.rotation * new Vector3(Mathf.Sin(timer * 2) * _speed * Time.fixedDeltaTime, 0, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            var player = other.GetComponent<PlayerState>(); 
            player.TakeDamage(1);
            BulletManager.Instance.BulletPool.Release(this);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Stage"))
        {
            BulletManager.Instance.BulletPool.Release(this);
        }
    }
}
