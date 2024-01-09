using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MainMenu
{
    public class UIExitGameBtn : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private Button exitButton;

        private void Start()
        {
            exitButton.onClick.AddListener(() => Audio.Instance.sfxSource.PlayOneShot(AudioClips.Instance.buttonClick));
            exitButton.onClick.AddListener(ButtonUtils.ExitGame);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Audio.Instance.sfxSource.PlayOneShot(AudioClips.Instance.buttonHover);
        }
    }
}
