using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCondition : MonoBehaviour
{
    public TMP_Text timerText;
    void Update()
    {
           if (timerText.text == "You Lose!!!")
        {
            SceneManager.LoadScene(3);
        }
    }
}
