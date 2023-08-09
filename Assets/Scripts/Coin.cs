using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int scoreValue=10;
    private GameManager gameManager;

    public AudioSource coinSound;
    private void Start()
    {
        gameManager= FindObjectOfType<GameManager>();
    }

    void Update()
    {
        transform.Rotate(0,0,180f*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Para toplandi");
            coinSound.Play();
            gameManager.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}