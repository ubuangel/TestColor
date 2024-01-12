using UnityEngine;

public class Timer : MonoBehaviour
{
    public const float RoundTime = 0.5f * 6.0f; // Half of the real Round Time to match halfed time pass
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
        {
            CurrTime -= 0.5f * Time.deltaTime;
        }
    }

    public void Reset()
    {
        CurrTime = RoundTime;
    }
}
