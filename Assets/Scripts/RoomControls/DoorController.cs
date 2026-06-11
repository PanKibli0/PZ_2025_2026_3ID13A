using UnityEngine;

public class DoorController : MonoBehaviour
{
    //Mock - na razie zmieniamy kolor kwadratu - drzwi
    [SerializeField] private SpriteRenderer doorRenderer;
    [SerializeField] private Collider2D doorCollider;
    [SerializeField] private Collider2D transitionTrigger;

    [SerializeField] private Color closedDoorColor = Color.red;
    [SerializeField] private Color openDoorColor = Color.green;


    public void CloseDoor()
    {
        //Mock - prze³¹czenie koloru, póŸniej trzeba bêdzie podpi¹æ drzwi
        doorRenderer.color = closedDoorColor;
        doorCollider.enabled = true;
        transitionTrigger.enabled = false;
    }

    public void OpenDoor()
    {
        doorRenderer.color = openDoorColor;
        doorCollider.enabled = false;
        transitionTrigger.enabled = true;
    }
}
