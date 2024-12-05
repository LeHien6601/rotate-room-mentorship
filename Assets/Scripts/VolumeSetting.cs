using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Windows;

public class VolumeSetting : MonoBehaviour, IPointerClickHandler
{
    public List<GameObject> volumeBars = new List<GameObject>();
    [SerializeField] private AudioSource[] sfxAudioSources;
    private List<float> initialVolume = new List<float>();
    [SerializeField] private Sprite volumeBarOff;
    [SerializeField] private Sprite volumeBarOn;
    private int index = -1;

    private void Start()
    {
        UpdateVolume();
    }

    
    public void OnPointerClick(PointerEventData eventData)
    {
        // Lấy RectTransform của GameObject
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Chuyển đổi vị trí nhấn từ Screen Space sang Local Space
        Vector2 localCursor;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localCursor
        );

        // Kiểm tra xem nhấn vào bên trái hay bên phải
        if (localCursor.x < 0) // Nếu x < 0, nhấn vào bên trái
        {
            DecreaseVolume();
        }
        else // Nếu x >= 0, nhấn vào bên phải
        {
            IncreaseVolume();
        }
    }

    private void DecreaseVolume()
    {
        if (-1 < index)
        {
            volumeBars[index--].GetComponent<Image>().sprite = volumeBarOff;
            UpdateVolume();
        }
        else
        {
            Debug.LogWarning("Volume is already at maximum!");
        }
    }
    private void IncreaseVolume()
    {
        if (index < volumeBars.Count -1) 
        {
            ++index;
            volumeBars[index].GetComponent<Image>().sprite = volumeBarOn;
            UpdateVolume();
        }
        else
        {
            Debug.LogWarning("Volume is already at maximum!");
        }
    }

    private void UpdateVolume()
    {
        foreach (var sfxAudioSource in sfxAudioSources)
        {
            float result = (float)(index +1) / 10;
            sfxAudioSource.volume = result;
        }
    }

}