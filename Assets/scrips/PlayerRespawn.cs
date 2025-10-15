using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerRespawn : MonoBehaviour
{
    
    private float checkpointPositionX, checkpointPositionY;

    public Animator animator;

    void Start()
    {
        if (PlayerPrefs.GetFloat("checkpointPositionX") != 0)
        {
            transform.position = new Vector2 (PlayerPrefs.GetFloat("checkpointPositionX"), PlayerPrefs.GetFloat("checkpointPositionY"));
        }
    }

    public void ReachedCheckPoint(float x, float y)
    {
        PlayerPrefs.SetFloat("checkpointPositionX", x);
        PlayerPrefs.SetFloat("checkpointPositionY", y);
    }

    public void PlayerDamaged()
    {
        animator.Play("PlayerDie");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
