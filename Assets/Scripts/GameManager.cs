using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//using a singleton style class (in the unity way)
public class GameManager : MonoBehaviour
{
    static GameManager _instance=null;

    public Text healthText;
    public GameObject deathPanel;

    void Awake(){
        if(_instance==null){
            _instance=this;
        }
    }

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
