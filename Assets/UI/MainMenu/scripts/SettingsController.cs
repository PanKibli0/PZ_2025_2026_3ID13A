using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [Header("Audio Sliders")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Audio Value Texts")]
    [SerializeField] private TMP_Text masterVolumeText;
    [SerializeField] private TMP_Text musicText;
    [SerializeField] private TMP_Text sfxText;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;

    [Header("Graphics")]
    [SerializeField] private Toggle fullscreenToggle;

    [Header("Game Settings")]
    [SerializeField] private TMP_Dropdown characterDropdown;

    private const string MasterVolumeKey = "MasterVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private const string SfxVolumeKey = "SfxVolume";
    private const string FullscreenKey = "Fullscreen";
    private const string CharacterKey = "SelectedCharacter"; 

    private void Start()
    {
        LoadSettings();
        RegisterListeners();
    }

    private void LoadSettings()
    {
        float masterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 1f);
        float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SfxVolumeKey, 1f);
        bool fullscreen = PlayerPrefs.GetInt(FullscreenKey, 1) == 1;

        int selectedCharacter = PlayerPrefs.GetInt(CharacterKey, 0);

        if (masterVolumeSlider != null) masterVolumeSlider.value = masterVolume;
        if (musicSlider != null) musicSlider.value = musicVolume;
        if (sfxSlider != null) sfxSlider.value = sfxVolume;
        if (fullscreenToggle != null) fullscreenToggle.isOn = fullscreen;

        if (characterDropdown != null) characterDropdown.value = selectedCharacter;

        AudioListener.volume = masterVolume;
        if (musicSource != null) musicSource.volume = musicVolume;
        Screen.fullScreen = fullscreen;

        UpdateVolumeTexts();
    }

    private void RegisterListeners()
    {
        if (masterVolumeSlider != null) masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        if (musicSlider != null) musicSlider.onValueChanged.AddListener(SetMusicVolume);
        if (sfxSlider != null) sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        if (fullscreenToggle != null) fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        if (characterDropdown != null) characterDropdown.onValueChanged.AddListener(SetCharacter);
    }

    public void SetCharacter(int index)
    {
        PlayerPrefs.SetInt(CharacterKey, index);
        PlayerPrefs.Save();
        Debug.Log("Zapisano postaæ z indeksem: " + index);
    }

    public void SetMasterVolume(float value)
    {
        PlayerPrefs.SetFloat(MasterVolumeKey, value);
        AudioListener.volume = value;
        UpdateVolumeTexts();
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, value);
        if (musicSource != null) musicSource.volume = value;
        UpdateVolumeTexts();
        PlayerPrefs.Save();
    }

    public void SetSfxVolume(float value)
    {
        PlayerPrefs.SetFloat(SfxVolumeKey, value);
        UpdateVolumeTexts();
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        PlayerPrefs.SetInt(FullscreenKey, isFullscreen ? 1 : 0);
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.Save();
    }

    public void ResetSettings()
    {
        if (masterVolumeSlider != null) masterVolumeSlider.value = 1f;
        if (musicSlider != null) musicSlider.value = 1f;
        if (sfxSlider != null) sfxSlider.value = 1f;
        if (fullscreenToggle != null) fullscreenToggle.isOn = true;
        if (characterDropdown != null) characterDropdown.value = 0; 

        AudioListener.volume = 1f;
        if (musicSource != null) musicSource.volume = 1f;
        Screen.fullScreen = true;

        PlayerPrefs.SetFloat(MasterVolumeKey, 1f);
        PlayerPrefs.SetFloat(MusicVolumeKey, 1f);
        PlayerPrefs.SetFloat(SfxVolumeKey, 1f);
        PlayerPrefs.SetInt(FullscreenKey, 1);
        PlayerPrefs.SetInt(CharacterKey, 0); 

        UpdateVolumeTexts();
        PlayerPrefs.Save();
    }

    private void UpdateVolumeTexts()
    {
        if (masterVolumeText != null && masterVolumeSlider != null)
            masterVolumeText.text = ToPercent(masterVolumeSlider.value);

        if (musicText != null && musicSlider != null)
            musicText.text = ToPercent(musicSlider.value);

        if (sfxText != null && sfxSlider != null)
            sfxText.text = ToPercent(sfxSlider.value);
    }

    private string ToPercent(float value)
    {
        return Mathf.RoundToInt(value * 100f) + "%";
    }
}