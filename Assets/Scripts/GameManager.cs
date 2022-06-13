using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public float delay = 2f;
    private static GameManager instance; 
    public Vector2 lastCheckpointPos;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            lastCheckpointPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void GameOver()
    {
        Invoke("Restart",delay);
    }

    void Restart()
    {    
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
