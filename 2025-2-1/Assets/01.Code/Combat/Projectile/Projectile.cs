using UnityEngine;
using UnityEngine.Serialization;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject target;
    [SerializeField] private float damage;
    [SerializeField] private float maxLifeTime = 5f;
    [SerializeField] private LayerMask whatIsEnemy;

    protected virtual void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    protected virtual void Update()
    {
        if (target == null)
        {
            OnTargetLost();
            return;
        }

        MoveTowardsTarget();
    }

    protected virtual void MoveTowardsTarget()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
        transform.forward = dir; // 쿼터뷰에서는 시각적 방향 중요
    }

    protected virtual void OnTriggerEnter(Collider collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsEnemy) != 0)
        {
            OnHit();
        }
    }

    protected virtual void OnHit()
    {
        Destroy(gameObject);
    }

    protected virtual void OnTargetLost()
    {
        Destroy(gameObject);
    }
}