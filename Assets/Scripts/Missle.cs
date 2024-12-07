using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public Vector3 mousePos;
    float speed = 100f;
    private float damage = 20f;
    Rigidbody2D rb;
    Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out var enemy))
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    public void AdjustTheMissle(Vector3 mousePos, float _damage)
    {
        direction = (mousePos - transform.position).normalized;
        damage = _damage;
    }
}
