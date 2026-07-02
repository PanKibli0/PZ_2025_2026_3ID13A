using TMPro;
using UnityEngine;
using System.Collections;

public class NotificationUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private float duration = 3f;

    private Coroutine currentCoroutine;

    private void Awake()
    {
        panel.SetActive(false);
    }

    public void Show(string title, string description)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(ShowRoutine(title, description));
    }

    private IEnumerator ShowRoutine(string title, string description)
    {
        titleText.text = title;
        descriptionText.text = description;

        panel.SetActive(true);

        yield return new WaitForSecondsRealtime(duration);

        panel.SetActive(false);
    }
}