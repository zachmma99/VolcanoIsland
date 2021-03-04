using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
    }

    public static GameManager instance()
    {
        return _instance;
    }

    Player player;
    Spawner spawner;

    public Text healthText;
    public Text ammoText;
    public GameObject DeathPanel;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();

        deathPanelSwitch(false);
        player.reset();
        spawner.reset();
    }

    public void updateHealthText(int health)
    {
        healthText.text = "x" + health;
    }

    public void updateAmmoText(int ammo)
    {
        ammoText.text = "x" + ammo;
    }

    public void deathPanelSwitch(bool state)
    {
        DeathPanel.SetActive(state);
    }

    public Player playerTag()
    {
        return player;
    }

    public bool isPlayerActive()
    {
        return player.gameObject.activeSelf;
    }

    public Transform playerPosition()
    {
        return player.gameObject.transform;
    }

    public void onRestartClick()
    {
        if(DeathPanel.activeSelf)
        {
            deathPanelSwitch(false);
            player.reset();
            spawner.reset();
        }
    }

    public void onMenuCLick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
