using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class MainMenuBackground : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private float swapColorRate;

        private float _time;

        private void Start()
        {
            background.color = RoundProp.RandomColor().Color * 0.85f;
        }

        private void Update()
        {
            _time += Time.deltaTime;
            if (_time >= swapColorRate)
            {
                _time = 0.0f;
                SwitchColor();
            }
        }

        private void SwitchColor()
        {
            StopAllCoroutines();
            StartCoroutine(FadeColor(background.color, RoundProp.RandomColor().Color * 0.85f));
        }

        private IEnumerator FadeColor(Color init, Color final)
        {
            const float duration = 1f;
            var elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                var temp = Color.Lerp(init, final, elapsed / duration);
                background.color = temp;

                yield return null;
            }

            background.color = final;
        }
    }
}
