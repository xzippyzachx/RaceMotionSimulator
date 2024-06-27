using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _singleton;
    public static UIManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
            {
                _singleton = value;
            }
            else if (_singleton != value)
            {
                Debug.Log("UIManager instance already exists, destroying object!");
                Destroy(value);
            }
        }
    }

    void Awake()
    {
        Singleton = this;
    }

    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text longGText;
    [SerializeField] private TMP_Text latGText;

    private float speed = 0f;
    private float longG = 0f;
    private float latG = 0f;
    

    void Update()
    {
        speedText.text = $"{speed} km/h";
        longGText.text = $"z {longG:0.00} g";
        latGText.text = $"x {latG:0.00} g";
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetLongG(float g)
    {
        longG = g;
    }

    public void SetLatG(float g)
    {
        latG = g;
    }

}
