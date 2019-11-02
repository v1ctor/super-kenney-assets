using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public int levelTime;
    public TMP_Text pointsText;
    public TMP_Text coinsText;
    public TMP_Text timerText;
    

    private float elapsed;
    private int timeLeft;
    private int pointsCollected;
    private int coinsCollected;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = levelTime;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        timeLeft = levelTime - (int)(elapsed % 60);
        if (timeLeft < 0) {
            timeLeft = 0;
        } 
        timerText.text = string.Format("{0:000}", timeLeft);
        pointsText.text = string.Format("{0:000000}", pointsCollected);
        coinsText.text = string.Format("x{0:00}", coinsCollected);
    }

    public void CoinCollected() {
        pointsCollected += 200;
        coinsCollected += 1;
    }
}
