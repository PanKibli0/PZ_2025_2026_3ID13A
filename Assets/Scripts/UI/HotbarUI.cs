using UnityEngine;

public class HotbarUI : MonoBehaviour
{
    [SerializeField] private HotbarSlotUI[] slots;

    private int currentIndex = 0;

    private void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetKeyNumber(i + 1);
        }
        RefreshSelection();
    }

    private void Update()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SetSlot(i);
            }
        }
    }

    private void SetSlot(int index)
    {
        currentIndex = index;
        RefreshSelection();
    }

    public void SetSelected(int index)
    {
        currentIndex = index;
        RefreshSelection();
    }

    private void RefreshSelection()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetSelected(i == currentIndex);
        }
    }
}