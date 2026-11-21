using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class contains the transitions to the ending scenes and is triggered by 
/// the fadeout animation reaching its end
/// </summary>
public class GameOver : MonoBehaviour
{
    public void Victory()
    {
        SceneManager.LoadScene("Victory");
    }

    public void Loss()
    {
        if (GameManager.Instance.vampConfidence <=0)
        {
            SceneManager.LoadScene("Ragequit");
        }   else
        {
            SceneManager.LoadScene("Death");
        }
    }
}
