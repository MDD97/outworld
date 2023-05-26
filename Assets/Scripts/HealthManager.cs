using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;

    public Image healthBarImage;

    public TextMeshProUGUI healthText;

    //Personnage mort??
    public bool isDead = false;

    //recup game over script
    public GameManager gameManager;

    public void ApplyDammage(float theDammage)
    {
        health = health - theDammage;

        if(health <= 0 && !isDead)
        {
            isDead = true;
            //this.gameObject.SetActive(false);
            gameManager.gameOver(); 
        }

    }

    // Update is called once per frame
    void Update()
    {

        //if(Input.GetKeyUp(KeyCode.K)) {
        //    ApplyDammage(5);
        //} 
        healthBarImage.fillAmount = health / maxHealth;
        healthText.text = health + " % ";
        health = Mathf.Clamp(health, 0f, maxHealth);
    }
}
