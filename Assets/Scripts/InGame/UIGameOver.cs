using TMPro;
using UnityEngine;

namespace InGame
{
    public class UIGameOver : MonoBehaviour
    {
        [SerializeField] private GameObject mainCanvas;
        [SerializeField] private GameObject gameOverCanvas;
        [SerializeField] private GameObject pauseCanvas;

        [SerializeField] private TMP_Text colorWord;
    
        private void Start()
        {
            Rcw.Instance.GameLost += OnGameLost;
        }

        private void OnGameLost()
        {
            mainCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
        
            Destroy(mainCanvas);
            Destroy(pauseCanvas);
        }
    }
}
