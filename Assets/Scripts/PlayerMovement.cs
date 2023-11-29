using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    private Vector2 movementInput;
    private Rigidbody rb;

    //Player Properties
    public float speed = 0;
    public float maxHealth = 50f;
    public float currentHealth;

    //UI
    public Slider healthbar;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        currentHealth = maxHealth; // Set current health to max health initially
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    void Walk()
    {
        Vector3 playerVelocity = new Vector3(movementInput.x * speed, rb.velocity.y, movementInput.y * speed);

        rb.velocity = transform.TransformDirection(playerVelocity);
    }

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();
        
        if (currentHealth <= 0)
        {
            Die();
        }
        
    }

    private void Die()
    {
        SceneManager.LoadScene(0);
    }

    void UpdateHealthBar()
    {

        healthbar.value = currentHealth; // Update the slider value to reflect the current health
        
    }
}
