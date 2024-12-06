using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public Vector3 mousePos;
    float speed = 50f;
    Rigidbody2D rb;

    Vector2 target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)target * speed * Time.deltaTime;
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("carpistim");
        if (other.gameObject.CompareTag("ground"))
        {
            Destroy(this.gameObject);
            Debug.Log("patladım ben kardesim");
        }
    }

    public void AdjustTheMousePos(Vector3 mousePos)
    {
        target = (mousePos - transform.position).normalized;
    }
}
