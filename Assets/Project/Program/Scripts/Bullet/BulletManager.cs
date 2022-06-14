using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletManager : MonoBehaviour
{
    [SerializeField] Bullet _bulletPrefab;

    ObjectPool<Bullet> _bulletPool;
    public IObjectPool<Bullet> BulletPool => _bulletPool;
    public static BulletManager _instance;
    public static BulletManager Instance => _instance;


    void Awake()
    {
        _instance = this;

        _bulletPool = new ObjectPool<Bullet>(
            OnCreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            true,
            100,
            100
            );
    }

    Bullet OnCreateBullet()
    {
        Bullet bullet = Instantiate(_bulletPrefab);
        return bullet;
    }

    void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false   );
    }

    void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void OnDestroy()
    {
        _instance = null;
    }
}
