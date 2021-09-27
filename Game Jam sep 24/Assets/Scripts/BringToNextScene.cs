using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BringToNextScene : MonoBehaviour
{
    public string nameTXT;

    public Sprite[] loseScreens = new Sprite[7];

    public static BringToNextScene Instance { get; private set; }

    Image image;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();

        if(scene.name == "LoseScreen")
        {
            image = GameObject.FindGameObjectWithTag("LoseScreen").GetComponent<Image>();

            for (int i = 0; i < loseScreens.Length; i++)
                if (loseScreens[i].name == nameTXT)
                    image.sprite = loseScreens[i];
        }
    }
}
