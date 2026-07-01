using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class MenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Hover Movement")]
    [SerializeField] private Vector2 hoverOffset = new Vector2(35f, 0f);
    [SerializeField] private float moveSpeed = 12f;

    [Header("Hover Sound")]
    [SerializeField] private AudioSource uiAudioSource;
    [SerializeField] private AudioClip hoverSound;

    private RectTransform rectTransform;
    private Vector2 startPosition;
    private Vector2 targetPosition;

    private const string SfxVolumeKey = "SfxVolume";

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
        targetPosition = startPosition;
    }

    private void OnEnable()
    {
        targetPosition = startPosition;

        if (rectTransform != null)
            rectTransform.anchoredPosition = startPosition;
    }

    private void OnDisable()
    {
        if (rectTransform != null)
            rectTransform.anchoredPosition = startPosition;

        targetPosition = startPosition;
    }

    private void Update()
    {
        rectTransform.anchoredPosition = Vector2.Lerp(
            rectTransform.anchoredPosition,
            targetPosition,
            Time.unscaledDeltaTime * moveSpeed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetPosition = startPosition + hoverOffset;
        PlayHoverSound();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetPosition = startPosition;
    }

    private void PlayHoverSound()
    {
        if (uiAudioSource == null || hoverSound == null)
            return;

        uiAudioSource.volume = PlayerPrefs.GetFloat(SfxVolumeKey, 1f);
        uiAudioSource.PlayOneShot(hoverSound);
    }
}