using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    public GameObject textbox;
    float speedX;
    float speedY;
    public float speed;
    Rigidbody2D rb;
    private bool isFacingRight = false;
    public Animator animator;
    public TMP_Text chestText;
    [SerializeField] private ParticleSystem GoodChestParticles;
    [SerializeField] private ParticleSystem BadChestParticles;
    [SerializeField] private TextAsset badChestText;
    [SerializeField] private TextAsset goodChestText;
    private ParticleSystem GoodChestParticlesInstance;
    private ParticleSystem BadChestParticlesInstance;

    private void Awake()
    {
        textbox.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * speed;
        speedY = Input.GetAxisRaw("Vertical") * speed;
        animator.SetFloat("Speed", Mathf.Abs(speedX));

        rb.linearVelocity = new Vector2(speedX, speedY);
        Flip();
    }

    private void Flip()
    {
        if (isFacingRight && speedX > 0f || !isFacingRight && speedX < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("goodChest"))
        {
            SpawnGoodChestParticles();
            textbox.SetActive(true);
            chestText.text = goodChestText.text;
            Invoke("disableTextbox", 5f);
            speed = speed * 2;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("badChest"))
        {
            SpawnBadChestParticles();
            textbox.SetActive(true);
            chestText.text = badChestText.text;
            Invoke("disableTextbox", 5f);
            speed = speed / 2;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Stairs"))
        {
            SceneManager.LoadScene(2);
        }
    }

    private void disableTextbox() 
    { 
        textbox.SetActive(false);
        chestText.text = "";
    }

    private void SpawnGoodChestParticles()
    {
        GoodChestParticlesInstance = Instantiate(GoodChestParticles, transform.position, Quaternion.identity);
    }
    private void SpawnBadChestParticles()
    {
        BadChestParticlesInstance = Instantiate(BadChestParticles, transform.position, Quaternion.identity);
    }
}
