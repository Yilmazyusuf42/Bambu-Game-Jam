using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public Vector3 mousePos;
    float speed = 50f;
    Rigidbody2D rb;
    Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * speed;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    public void AdjustTheMissle(Vector3 mousePos)
    {
        direction = (mousePos - transform.position).normalized;
    }
}
