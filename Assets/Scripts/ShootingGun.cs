using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShootingGun : MonoBehaviour
{
    public static ShootingGun instance; // Singleton instance for easy access
    
    // The amount of damage each shot inflicts and the range of shooting
    public float damage = 10f;
    public float range = 100f;
    
    // Force and rate of fire for the gun
    public float impactForce = 30f;
    public float fireRate = 15f;

    public Camera fpsCam; // Camera used for raycasting
    public ParticleSystem gunFlash; // Visual effect for shooting

    private float nextTimeToFire = 0f; // Control for fire rate
    
    // UI elements
    public int killCount = 0; // Counter for kills
    public TextMeshProUGUI killCountUI; // UI Text displaying the kill count
    public Image aim; // UI Image representing the aiming reticle

    private bool isEnemyInRange = false; // Flag to track if enemy is in range

    private void Awake()
    {
        instance = this; // Setting the instance of the ShootingGun class
    }

    private void Update()
    {
        IsEnemyInRange(); // Check if enemy is in range
        if (isEnemyInRange && Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate; // Set next allowed firing time
            Shoot(); // Fire the gun
        }
    }
    
    // Method to change the color of the aim UI
    void ChangeAimColor(Color newColor)
    {
        if (aim != null)
        {
            aim.color = newColor; // Change the color of the aim UI
        }
    }

    // Method to check if an enemy is within shooting range and change the aim color accordingly
    public void IsEnemyInRange()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                isEnemyInRange = true; // Set flag as enemy is in range
                ChangeAimColor(Color.green); // Change the aim UI color to green to indicate the player can shoot
                return;
            }
        }
        
        // If no valid enemy is within range, reset UI color to red
        isEnemyInRange = false;
        if (aim != null)
        {
            aim.color = Color.red; // Reset the UI color to red
        }
    }

    // Method to execute the shooting action
    public void Shoot()
    {
        // Play the gun flash particle effect when shooting
        gunFlash.Play();
      
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
         
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Inflict damage on the enemy if hit
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce); // Apply force to hit rigidbodies
            }
        }
    }
    
    // Method to increase the kill count by a specified amount
    public void IncreaseKillCount(int amount)
    {
        killCount += amount; // Increment the kill count
        
        if (killCountUI != null)
        {
            killCountUI.text = "Kill Count: " + killCount; // Update the UI with the new kill count
        }
    }
}
