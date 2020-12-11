using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    int mMaxHealth = 100;

    [SerializeField]
    int mCurrentHealth;

    GameObject DamagePrompt;

    
    Text DamagePromptText;

    public GameObject EssenceReference;
    public GameObject PowerUpReference;
    public GameObject HealthReference;
    // Start is called before the first frame update
    public GameObject healthBar;
    public GameObject ShipHealthBar;

    // Start is called before the first frame update
    void Start(){
        mCurrentHealth = mMaxHealth;

        DamagePrompt = GameObject.Find("Damage Prompt");
        DamagePromptText = DamagePrompt.GetComponent<Text>();
        healthBar = GameObject.Find("Player_HealthBar");
        ShipHealthBar = GameObject.Find("Ship_HealthBar");


        if(tag == "Player")
        {
            healthBar.GetComponent<HealthBar>().SetMaxHealth(mMaxHealth);
        }

        if(name == "Ship")
        {
            ShipHealthBar.GetComponent<HealthBar>().SetMaxHealth(mMaxHealth);
        }

    }

    void Update()
    {
    }

    public void TakeDamage(int damage){
        mCurrentHealth -= damage;
        print(gameObject.name + " health:" + mCurrentHealth);
        DamagePromptText.text = " - " + damage;
        if(tag == "Player")
        {
            GameManager.OnTakeDamage();
            healthBar.GetComponent<HealthBar>().SetHleath(mCurrentHealth);
        }
        if(name == "Ship")
        {
            ShipHealthBar.GetComponent<HealthBar>().SetHleath(mCurrentHealth);
        }

        //print(gameObject.name + " health:" + mCurrentHealth);
        if(mCurrentHealth <= 0)
        {
                Die();
        }
    }

    private void Die(){
        if(tag == "Player"){

            print("PLayer is dead");
            GameManager.OnPlayerDied();
        }

        if(name =="Ship"){
            GameManager.OnShipDestoryed();
        }

        if(gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            print("enmey died");
            DropResources();
        }
        if(tag == "Scatter")
        {
            DropPrize();
        }
        Destroy(gameObject);
    }

    public void IncreaseHealth(int increase)
    {
        mCurrentHealth += increase;
        if(mCurrentHealth > mMaxHealth)
        {
            mCurrentHealth = mMaxHealth;
        }
    }

    public void ResetHealth()
    {
        mCurrentHealth = mMaxHealth;
    }

    public void SetMaxHealth(int maxHealth)
    {
        mMaxHealth = maxHealth;
    }
    public int getMaxHealth()
    {
        return mMaxHealth;
    }

    public void DropResources()
    {
        float selector = Random.Range(0.0f,10.0f);
        if(selector <= 3.0f)
        {
            DropPowerUp();
        }

        if(selector >3.0f && selector <= 6.0f)
        {
            DropHealth();
        }

        if(selector > 6.0f && selector <=10.0f)
        {
            DropEssence();
        }

    }

    void DropPowerUp()
    {
        Instantiate(PowerUpReference, transform.position, Quaternion.identity);
    }

    void DropHealth()
    {
        Instantiate(HealthReference, transform.position, Quaternion.identity);
    }

    void DropEssence()
    {
        Instantiate(EssenceReference, transform.position + new Vector3(0.0f,2.0f,0.0f), Quaternion.identity);
    }

    void DropPrize()
    {
        GameObject essence = Instantiate(EssenceReference, transform.position, Quaternion.identity);
        essence.GetComponent<Essence>().SetAmount(300);
    }


}
