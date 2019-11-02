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

    private float timeUnit; 

    private float timeLeft;
    private int pointsCollected;
    private int coinsCollected;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = levelTime;
        timeUnit = 160.0f / 400.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime / timeUnit;
        timeLeft -= delta;
        if (timeLeft < 0) {
            timeLeft = 0;
        } 
        timerText.text = string.Format("{0:000}", (int)timeLeft);
        pointsText.text = string.Format("{0:000000}", pointsCollected);
        coinsText.text = string.Format("x{0:00}", coinsCollected);
    }

    public void CoinCollected() {
        pointsCollected += 200;
        coinsCollected += 1;
    }
}
