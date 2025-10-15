using UnityEngine;
using UnityEngine.Audio;
public class FruitCollected : MonoBehaviour
{
    public AudioSource collectSound;

    [Header("Configuration")]
    public LevelData levelData;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);

           GameManager.Instance.FruitCollected();

            Destroy(gameObject, 0.5f);

            collectSound.Play();
        }
    }


}
