using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float normalPushForce = 200;
    [SerializeField] private float lateralSpeed = 5;
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource crashSound;

    [HideInInspector] public float currentPushForce;

    private Rigidbody playerRb;
    private GameManager gameManager;
    float horizontalInput;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        startPoint = transform.position;
        currentPushForce = normalPushForce;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (transform.position.y < 0)
            gameManager.ResetPosition();
    }

    private void FixedUpdate()
    {
        if (!gameManager.gameStopped)
            Move();
    }

    private void Move()
    {
        playerRb.AddForce(currentPushForce * Time.fixedDeltaTime * Vector3.forward);
        playerRb.velocity = new Vector3(horizontalInput * lateralSpeed, playerRb.velocity.y, playerRb.velocity.z);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Barrier"))
        {
            crashSound.Play();
            gameManager.ResetPosition();
            gameManager.ResetScore();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndTrigger") && !gameManager.gameStopped)
        {
            gameManager.SaveScore();
            winSound.Play();
            Debug.Log("EndTrigger calisti..");
            gameManager.LevelUp();
        }
    }
}
