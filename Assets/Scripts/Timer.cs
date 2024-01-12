using UnityEngine;

public class Timer : MonoBehaviour
{
    public const float RoundTime = 6.0f; // ETODO: Aca cambias el tiempo pe (TOTAL! => es el tiempo de pre-juego (esperar)
    public float CurrTime { get; private set; } = RoundTime;

    private bool _paused = true;
    
    private void Start()
    {
        Rcw.Instance.GameStart += () => _paused = false;
        Rcw.Instance.GameLost += () => _paused = true;
    }

    private void Update()
    {
        if (!_paused)
        {//TIME
            CurrTime -= Time.deltaTime/2;
        }
    }

    public void Reset()
    {
        // adding round time is more accurate over time than resetting it
        if (CurrTime + RoundTime > RoundTime)
        {
            CurrTime = RoundTime;
        }
        else
        {
            CurrTime += RoundTime;
        }
    }
}
