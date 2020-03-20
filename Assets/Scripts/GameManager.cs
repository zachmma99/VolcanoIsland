/*
Copyright 2020 Micah Schuster

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
this list of conditions and the following disclaimer in the documentation and/or
other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE
OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages game state and UI
/// </summary>
public class GameManager : MonoBehaviour
{
    //Simple singleton for the GameManager
    //Our game is simple enough that a singleton to manage state is useful.
    //Don't overuse this design pattern!
    static GameManager _instance=null;

    void Awake(){
        if(_instance==null){
            _instance=this;
        }
    }

    public static GameManager instance(){
        return _instance;
    }
    /// <summary>
    /// Text object that represents the Health of the player. Displayed to UI.
    /// </summary>
    public Text healthText;

    /// <summary>
    /// UI panel that is shown then the Player loses all health.
    /// </summary>
    public GameObject deathPanel;

    /// <summary>
    /// Reference to the Player object.
    /// </summary>
    Player player;

    /// <summary>
    /// Reference to the Spawner object.
    /// </summary>
    Spawner spawner;

    /// <summary>
    /// Function for updating the Health UI text during gameplay.
    /// </summary>
    /// <param name="health">The Health value to display in the UI</param>
    public void updateHealthText(int health){
        healthText.text="x"+health;
    }

    /// <summary>
    /// Function for switching the GameOver UI on and off.
    /// </summary>
    /// <param name="state">State to change the UI panel to.</param>
    /// <remark>
    /// state = True will set the UI panel active, False will set to inactive.
    /// </remark>
    public void deathPanelSwitch(bool state){
        deathPanel.SetActive(state);
    }

    /// <summary>
    /// Gets references to player and spawner and resets the game to an initial
    /// state.
    /// </summary>
    void Start()
    {
        deathPanelSwitch(false);
        player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        player.reset();
        spawner.reset();
    }

    /// <summary>
    /// Resets the game to a playable state after clicking on the Restart button
    /// on the game over UI.
    /// </summary>
    public void onRestartClick(){
        if(deathPanel.activeSelf){
            deathPanelSwitch(false);
            player.reset();
            spawner.reset();
        }
    }

    /// <summary>
    /// Function called when clicking on the Menu button after Game Over.
    /// </summary>
    public void onMenuClick(){
        if(deathPanel.activeSelf){
            SceneManager.LoadScene("MainMenu");

        }
    }
}
