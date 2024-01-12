using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MainMenu
{
    public class UICredits : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private RectTransform logo;
        [SerializeField] private RectTransform playButton;
        [SerializeField] private RectTransform exitButton;
        [SerializeField] private RectTransform creditsPanel;
        
        [SerializeField] private Button creditsButton;

        [SerializeField] private float transitionOffset;

        private float _logoXPosA;
        private float _logoXPosB;
        private float _playButtonXPosA;
        private float _playButtonXPosB;
        private float _exitButtonXPosA;
        private float _exitButtonXPosB;
        private float _creditsPanelXPosA;
        private float _creditsPanelXPosB;

        private bool _open;

        private void Start()
        { 
            creditsButton.onClick.AddListener(() => Audio.Instance.sfxSource.PlayOneShot(AudioClips.Instance.buttonClick));

            _logoXPosA = logo.anchoredPosition.x;
            _logoXPosB = _logoXPosA - transitionOffset;

            _playButtonXPosA = playButton.anchoredPosition.x;
            _playButtonXPosB = _playButtonXPosA - transitionOffset;

            _exitButtonXPosA = exitButton.anchoredPosition.x;
            _exitButtonXPosB = _exitButtonXPosA - transitionOffset;

            _creditsPanelXPosA = creditsPanel.anchoredPosition.x;
            _creditsPanelXPosB = 0.0f;
        }

        public void ToggleCreditsWrapper()
        {
            StopAllCoroutines();
            _open = !_open;
            StartCoroutine(_open ? OpenCredits() : CloseCredits());
        }

        private IEnumerator OpenCredits()
        {
            StartCoroutine(TranslateX(logo, _logoXPosA, _logoXPosB, 0.2f, true));

            yield return new WaitForSeconds(0.15f);

            StartCoroutine(TranslateX(playButton, _playButtonXPosA, _playButtonXPosB, 0.2f, true));

            yield return new WaitForSeconds(0.1f);

            StartCoroutine(TranslateX(exitButton, _exitButtonXPosA, _exitButtonXPosB, 0.2f, true));

            yield return new WaitForSeconds(0.05f);

            StartCoroutine(TranslateX(creditsPanel, _creditsPanelXPosA, _creditsPanelXPosB, 0.2f, true));
        }

        private IEnumerator CloseCredits()
        {
            StartCoroutine(TranslateX(creditsPanel, _creditsPanelXPosB, _creditsPanelXPosA, 0.2f, true));

            yield return new WaitForSeconds(0.15f);

            StartCoroutine(TranslateX(exitButton, _exitButtonXPosB, _exitButtonXPosA, 0.2f, true));

            yield return new WaitForSeconds(0.1f);

            StartCoroutine(TranslateX(playButton, _playButtonXPosB, _playButtonXPosA, 0.2f, true));

            yield return new WaitForSeconds(0.05f);

            StartCoroutine(TranslateX(logo, _logoXPosB, _logoXPosA, 0.2f, true));
        }

        private static IEnumerator TranslateX(RectTransform element, float initX, float finalX, float duration, bool easeIn)
        {
            var elapsed = 0f;
            var y = element.anchoredPosition.y;
        
            var initial = new Vector2(initX, y);
            var final = new Vector2(finalX, y);

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                var ratio = elapsed / duration;
                element.anchoredPosition = Vector2.Lerp(
                    initial, final, 
                    easeIn ? Easings.EaseInQuint(ratio) : Easings.EaseOutQuint(ratio)
                );

                yield return null;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Audio.Instance.sfxSource.PlayOneShot(AudioClips.Instance.buttonHover);
        }
    }
}
