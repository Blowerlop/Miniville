using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KILLER
{
    public class Setting : MonoBehaviour
    {
        public static Setting instance = null;
        public GameObject[] go;
        public bool selected = false,isfull;
        [SerializeField]
        Slider VolM, VolG;
        [SerializeField]
        AudioMixer AudioMaster;
        [SerializeField]
        TMPro.TMP_Text txtM,txtG;
        [SerializeField]
        TMPro.TMP_Dropdown resolutionDrop, qualityDrop;

        public Toggle IsFullToggle;
        Resolution[] Resolutions;

        public UnityEvent myevent;
        UnityEvent myCloseEvent;
        public UnityEvent mySensEvent;

        void Awake()
        {
            //Singleton
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public void Start()
        {
            if (myevent == null)
                myevent = new UnityEvent();
            if (myCloseEvent == null)
                myCloseEvent = new UnityEvent();
            if (mySensEvent == null)
                mySensEvent = new UnityEvent();
            IsFullToggle = GetComponentInChildren<Toggle>();

            AudioMaster.SetFloat("MusicVolume", PlayerPrefs.GetInt("VolM"));
            txtM.text = PlayerPrefs.GetInt("VolM").ToString() + "%";
            AudioMaster.SetFloat("GameVolume", PlayerPrefs.GetInt("VolG"));
            txtG.text = PlayerPrefs.GetInt("VolG").ToString() + "%";
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
            qualityDrop.value = QualitySettings.GetQualityLevel();
            if(PlayerPrefs.GetInt("IsFull") == 1)
            {
                isfull = true;
            }
            else
            {
                isfull = false;
            }
            IsFullToggle.isOn = isfull;
            Screen.SetResolution(PlayerPrefs.GetInt("ResolutionW", 1080), PlayerPrefs.GetInt("ResolutionH", 720), true);
            Resolutions = Screen.resolutions;
            resolutionDrop.ClearOptions();
            List<string> options = new List<string>();
            int tempIndex = 0;
            for(int i = 0; i < Resolutions.Length; i++)
            {
                if(PlayerPrefs.GetInt("ResolutionW")+" x "+ PlayerPrefs.GetInt("ResolutionH") == Resolutions[i].width + " x " + Resolutions[i].height)
                {
                    tempIndex = i;
                }
                string option = Resolutions[i].width + " x " + Resolutions[i].height;
                options.Add(option);
            }
            resolutionDrop.AddOptions(options);
            resolutionDrop.value = tempIndex;
            SetVolumeValue(3);
            mySensEvent.Invoke();
            DissUI(0);
            myCloseEvent.AddListener(delegate { gameObject.SetActive(false); });
            //gameObject.SetActive(false);
        }
        public void SetResolution(int resolutionIndex)
        {

            Resolution resolution = Resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            PlayerPrefs.SetInt("ResolutionW", resolution.width);
            PlayerPrefs.SetInt("ResolutionH", resolution.height);
        }
        public void SetQuality(int quality)
        {
            QualitySettings.SetQualityLevel(quality);
            PlayerPrefs.SetInt("Quality", quality);
        }
        public void FullScreen(bool isFull)
        {
            if (isFull)
            {
                PlayerPrefs.SetInt("IsFull", 1);
            }
            else
            {
                PlayerPrefs.SetInt("IsFull", 0);
            }
            Screen.fullScreen = isFull;
        }
        public void SetVolumeValue(int type)
        {
            switch (type)
            {
                case 1:
                    AudioMaster.SetFloat("MusicVolume", VolM.value - 80);
                    txtM.text = ((int)VolM.value).ToString() + "%";
                    PlayerPrefs.SetInt("VolM", (int)VolM.value - 80);
                    break;
                case 2:
                    AudioMaster.SetFloat("GameVolume", VolG.value - 80);
                    txtG.text = ((int)VolG.value).ToString() + "%";
                    PlayerPrefs.SetInt("VolG", (int)VolG.value - 80);
                    break;
                case 3:
                    float value;
                    AudioMaster.GetFloat("MusicVolume", out value);
                    VolM.value = value + 80;
                    txtM.text = ((int)VolM.value).ToString() + "%";
                    AudioMaster.GetFloat("GameVolume", out value);
                    VolG.value = value + 80;
                    txtG.text = ((int)VolG.value).ToString() + "%";
                    break;

            }
        }

        public void DissUI(int index)
        {
            foreach (GameObject goD in go)
            {
                goD.SetActive(false);
            }
            go[index].SetActive(true);
        }
        public void Close()
        {
            myCloseEvent.Invoke();
        }
        public void Menu()
        {
            PhotonNetwork.Disconnect();
            Destroy(GameObject.Find("Discord"));
            PhotonNetwork.LoadLevel("Menu");
        }
    }
}
