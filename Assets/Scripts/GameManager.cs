using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        //the game is currently over
        if(deathPanel.activeSelf){
            if(Input.GetKeyDown(KeyCode.Space)){
                //reset game
                deathPanelSwich(false);
                player.reset();
                spawner.reset();
            }
        }
    }
}
