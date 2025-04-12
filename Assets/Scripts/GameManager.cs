using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject obstacle;
    public GameObject endPanel;

    public Text timeTxt;
    float time = 0.0f;

    public Text nowScore;
    public Text bestScore;

    bool isPlay = false;

    string key = "BestScore";

    public Animator anim;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        isPlay = true;
        Time.timeScale = 1.0f;
        InvokeRepeating("MakeObstacle", 0f, 1f);
    }

    private void Update()
    {
        if(isPlay == true)
        {
            time += Time.deltaTime;
            timeTxt.text = time.ToString("N2");
        }
  
    }

    void MakeObstacle()
    {
        Instantiate(obstacle);
    }

    public void GameOver()
    {
        isPlay = false;
        anim.SetBool("isDie", true);
        Invoke("TimeStop", 0.5f);
        nowScore.text = time.ToString("N2");
        if (PlayerPrefs.HasKey(key))
        {
            float best = PlayerPrefs.GetFloat(key);
            if (best < time)
            {
                PlayerPrefs.SetFloat(key, time);
                bestScore.text = time.ToString("N2");
            }
            else
            {
                bestScore.text = best.ToString("N2");

            }
        }
        else
        {
            PlayerPrefs.SetFloat(key, time);
            bestScore.text = time.ToString("N2");

        }


        endPanel.SetActive(true);

    }

    void TimeStop()
    {
        Time.timeScale = 0f;

    }
}
