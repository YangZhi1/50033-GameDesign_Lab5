using UnityEngine.UI;
using UnityEngine;

public class ScoreMonitor : MonoBehaviour
{
    public IntVariable marioScore;
    public Text text;
    public void UpdateScore()
    {
        text.text = marioScore.Value.ToString();
    }
}