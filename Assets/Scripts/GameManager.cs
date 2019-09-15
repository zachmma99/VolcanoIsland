using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//using a singleton style class (in the unity way)
public class GameManager : MonoBehaviour
{
    static GameManager _instance=null;

    void Awake(){
        if(_instance==null){
            _instance=this;
        }
    }

    public Text healthText;
    public GameObject deathPanel;
    Player player;
    Spawner spawner;

    public static GameManager instance(){
        return _instance;
    }

    public void updateHealthText(int health){
        healthText.text="x"+health;
    }

    public void deathPanelSwich(bool state){
        deathPanel.SetActive(state);
    }

    // Start is called before the first frame update
    void Start()
    {
        deathPanelSwich(false);
        player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        player.reset();
        spawner.reset();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onRestartClick(){
        if(deathPanel.activeSelf){
            deathPanelSwich(false);
            player.reset();
            spawner.reset();
        }
    }

    public void onMenuClick(){
        if(deathPanel.activeSelf){
            SceneManager.LoadScene("MainMenu");

        }
    }
}
