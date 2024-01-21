using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

 
namespace MainMenu
{
    public class UIInstructions : MonoBehaviour, IPointerEnterHandler
    {
     
       // [SerializeField] private RectTransform background;
        
        [SerializeField] private RectTransform logo;
        [SerializeField] private RectTransform playButtonEasy;
        [SerializeField] private RectTransform playButtonMedium;
        [SerializeField] private RectTransform playButtonHard;
        [SerializeField] private RectTransform exitButton;
        [SerializeField] private RectTransform instructionsPanel;

        [SerializeField] private Button instructionsButton;
        [SerializeField] private RectTransform creditsButton;
      
       
        [SerializeField] private float transitionOffset;
        

        private float _logoXPosA;
        private float _logoXPosB;


        private float _playButtonEasyXPosA;
        private float _playButtonEasyXPosB;
        private float _playButtonMediumXPosA;
        private float _playButtonMediumXPosB;
        private float _playButtonHardXPosA;
        private float _playButtonHardXPosB;
        

    

        private float _exitButtonXPosA;
        private float _exitButtonXPosB;

        private float _instructionPanelXPosA;
        private float _instructionPanelXPosB;

        private Vector2 _bgPosA;
        private Vector2 _bgPosB;

        private Vector2 _bgSizeA;
        private Vector2 _bgSizeB;

        private bool _open;

        private void Start()
        { 
            
            instructionsButton.onClick.AddListener(() => Audio.Instance.sfxSource.PlayOneShot(AudioClips.Instance.buttonClick));
            
 

            _logoXPosA = logo.anchoredPosition.x;
            _logoXPosB = _logoXPosA - transitionOffset;

            _playButtonEasyXPosA = playButtonEasy.anchoredPosition.x;
            _playButtonEasyXPosB = _playButtonEasyXPosA - transitionOffset;

            _playButtonMediumXPosA = playButtonMedium.anchoredPosition.x;
            _playButtonMediumXPosB = _playButtonMediumXPosA - transitionOffset;

            _playButtonHardXPosA = playButtonHard.anchoredPosition.x;
            _playButtonHardXPosB = _playButtonHardXPosA - transitionOffset;

            _exitButtonXPosA = exitButton.anchoredPosition.x;
            _exitButtonXPosB = _exitButtonXPosA - transitionOffset;

            _instructionPanelXPosA = instructionsPanel.anchoredPosition.x;
            _instructionPanelXPosB = 0.0f;


        }

        public void ToggleInstructionsWrapper()
        {
            StopAllCoroutines();
            _open = !_open;
            StartCoroutine(_open ? OpenInstructions() : CloseInstructions());
        }

        private IEnumerator OpenInstructions()
        {
           
            StartCoroutine(TranslateX(logo, _logoXPosA, _logoXPosB, 0.2f, true));

            yield return new WaitForSeconds(0.15f);
            StartCoroutine(TranslateX(creditsButton, creditsButton.anchoredPosition.x, 2000, 0.2f, true));
            yield return new WaitForSeconds(0.15f);



            StartCoroutine(TranslateX(playButtonEasy, _playButtonEasyXPosA, _playButtonEasyXPosB, 0.2f, true));
            StartCoroutine(TranslateX(playButtonMedium, _playButtonMediumXPosA, _playButtonMediumXPosB, 0.2f, true));
            StartCoroutine(TranslateX(playButtonHard, _playButtonHardXPosA, _playButtonHardXPosB, 0.2f, true));
            
          

            yield return new WaitForSeconds(0.15f);
            
            StartCoroutine(TranslateX(exitButton, exitButton.anchoredPosition.x, 2000, 0.2f, true));
            
            yield return new WaitForSeconds(0.1f);
            
            //StartCoroutine(TranslateX(playButton, playButton.anchoredPosition.x, 2000, 0.2f, true));
          //  StartCoroutine(TranslateX(instructionsPanel, instructionsPanel.anchoredPosition.x, 0, 0.5f, false));
            StartCoroutine(TranslateX(instructionsPanel, _instructionPanelXPosA, _instructionPanelXPosB, 0.2f, true));
            
          //  yield return new WaitForSeconds(0.2f);
            
            //StartCoroutine(Transform(0.5f, _bgPosA, _bgPosB, _bgSizeA, _bgSizeB));
        }

        private IEnumerator CloseInstructions()
        {
            StartCoroutine(TranslateX(instructionsPanel, _instructionPanelXPosB, _instructionPanelXPosA, 0.2f, true));
           // StartCoroutine(Transform(0.5f, _bgPosB, _bgPosA, _bgSizeB, _bgSizeA));
            
            yield return new WaitForSeconds(0.15f);
            
            StartCoroutine(TranslateX(exitButton, _exitButtonXPosB, _exitButtonXPosA, 0.2f, true));

            //StartCoroutine(TranslateX(playButton, playButton.anchoredPosition.x, -400, 0.4f, false));
             yield return new WaitForSeconds(0.1f);
            StartCoroutine(TranslateX(playButtonHard, _playButtonHardXPosB, _playButtonHardXPosA, 0.2f, true));
            StartCoroutine(TranslateX(playButtonMedium, _playButtonMediumXPosB, _playButtonMediumXPosA, 0.2f, true));
            StartCoroutine(TranslateX(playButtonEasy, _playButtonEasyXPosB, _playButtonEasyXPosA, 0.2f, true));


            yield return new WaitForSeconds(0.15f);
            
            StartCoroutine(TranslateX(logo, _logoXPosB, _logoXPosA, 0.2f, true));
            yield return new WaitForSeconds(0.15f);
           
            StartCoroutine(TranslateX(creditsButton, creditsButton.anchoredPosition.x, -100, 0.2f, false));

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
