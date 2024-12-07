using System.Collections;
using UnityEngine;

public class FreezerEnemy : MonoBehaviour
{
    public float detectionRange = 10f;      // Algýlama mesafesi
    public float freezeDistance = 1.5f;     // Oyuncu ile durulacak mesafe
    public float moveSpeed = 3f;            // Düþman hýz
    public float freezeTime = 2f;           // Oyuncu dondurulacak süre
    public float attackDistance = 1f;       // Saldýrý mesafesi

    private Transform player;               // Oyuncu referansý
    private Rigidbody2D playerRb;           // Oyuncunun Rigidbody2D'si
    private bool isFreezing = false;        // Oyuncuyu dondurma durumu
    private float freezeTimer = 0f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Oyuncuyu algýla
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRange, LayerMask.GetMask("Player"));

        if (playerCollider != null)
        {
            player = playerCollider.transform;
            playerRb = player.GetComponent<Rigidbody2D>();

            // Oyuncuya doðru gitme yerine belli bir mesafeye yaklaþma
            MoveNearPlayer();

            // Oyuncuya yaklaþtýðýnda durakla
            if (Vector2.Distance(transform.position, player.position) <= freezeDistance)
            {
                StartCoroutine(FreezePlayerIfClose());
            }
        }
    }

    void MoveNearPlayer()
    {

        if (player != null)
        {

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);


            if (distanceToPlayer > freezeDistance)
            {
                animator.SetBool(AnimationKey.Is_Running, true);
                Vector2 direction = (player.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

                if (direction.x != 0)
                {
                    Vector3 scale = transform.localScale;
                    scale.x = direction.x > 0 ? Mathf.Abs(scale.x) : - Mathf.Abs(scale.x);
                    transform.localScale = scale;
                }
            }
        }
    }


    IEnumerator FreezePlayerIfClose()
    {
        // Player'a yakýnsa 2 saniye bekle
        float initialDistance = Vector2.Distance(transform.position, player.position);

        // Duraklamayý baþlat
        Debug.Log("Düþman oyuncuya yakýnlaþtý, duraklama baþlýyor.");

        // Süreyi sýfýrla
        freezeTimer = 0f;

        while (Vector2.Distance(transform.position, player.position) <= freezeDistance)
        {
            freezeTimer += Time.deltaTime;

            // 2 saniye boyunca Player'ýn yakýnýnda dur
            if (freezeTimer >= freezeTime)
            {
                if (playerRb != null)
                {
                    // Player'ýn Rigidbody2D'sini dondur
                    playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
                    Debug.Log("Oyuncu dondu!");

                    // 2 saniye dolduðunda saldýrý yap
                    AttackPlayer();
                }
                yield break; // 2 saniye dolmuþsa Coroutine'i bitir
            }

            yield return null;
        }

        // Eðer Player uzaklaþýrsa süresi sýfýrlanýr
        freezeTimer = 0f;
        Debug.Log("Player uzaklaþtý, duraklama sýfýrlandý.");
    }

    void AttackPlayer()
    {
        // Oyuncu dondurulmuþsa, saldýrýyý baþlat
        if (playerRb != null)
        {
            Debug.Log("Düþman saldýrýyor!");
            // Burada saldýrý iþlemi gerçekleþebilir
        }

        // Saldýrý sonrasý 2 saniye bekle, sonra dondurmayý kaldýr
        StartCoroutine(UnfreezePlayer());
    }

    IEnumerator UnfreezePlayer()
    {
        // 2 saniye sonra oyuncunun dondurulmasýný kaldýr
        yield return new WaitForSeconds(2f);

        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints2D.None; // Dondurmayý kaldýr
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation; // Yalnýzca rotasyonu sabitle
        }
    }

    // Görsel yardým için çizim
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Algýlama alaný
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, freezeDistance); // Donma mesafesi
    }

    private void OnDestroy()
    {
        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints2D.None; // Dondurmayý kaldýr
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation; // Yalnýzca rotasyonu sabitle
        }
    }
}
