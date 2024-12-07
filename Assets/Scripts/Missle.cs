using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public Vector3 mousePos;
    float speed = 100f;
    float damage;
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
        print(collision.gameObject.name);
        Destroy(gameObject);
    }

    public void AdjustTheMissle(Vector3 mousePos, float _damage)
    {
        direction = (mousePos - transform.position).normalized;
        damage = _damage;
    }
}
