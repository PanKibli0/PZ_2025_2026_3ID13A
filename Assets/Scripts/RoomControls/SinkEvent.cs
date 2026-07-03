using UnityEngine;

public class SinkEvent : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ZoneSpawner zoneSpawner;

    private bool playerInside;
    private bool eventStarted;


    private void Update()
    {
        if (!playerInside)
            return;

        if (eventStarted)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartEvent();
        }
    }

    private void StartEvent()
    {
        eventStarted = true;

        Debug.Log("Sink Event Start");

        zoneSpawner.activateZoneManual();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = false;
    }
}