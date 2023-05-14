using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button sellButton;
    [SerializeField] TextMeshProUGUI towerLevelUI;
    
    public int damage;
    public float attackSpeed;
    public float range;
    
    private float baseAttackSpeed;
    private float baseRange;
    
    public int towerCost;
    private int upgradeCost;
    private int towerLevel;

    public GameObject projectilePrefab;
    public Transform firingPoint;
    
    public LayerMask enemyMask;
    
    private float rotationSpeed = 400f;
    public Transform rotationPoint;
    
    protected Transform target;
    protected float timeUntilFire;
    
    public int GetTowerCost()
    {
        return towerCost;
    }

    public void SetTowerCost(int cost)
    {
        towerCost = cost;
    }
    
    private void OnGUI()
    {
        towerLevelUI.text = towerLevel.ToString();
    }

    private void Start()
    {
        baseAttackSpeed = attackSpeed;
        baseRange = range;
        upgradeCost = towerCost;
        towerLevel = 1;
        timeUntilFire = 1f / attackSpeed; // Make the first shot instant
    
        upgradeButton.onClick.AddListener(Upgrade);
        sellButton.onClick.AddListener(Sell);
    }
    
    protected virtual void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            // shoot
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / attackSpeed)
            {
                Shoot();
            }
        }
    }

    protected virtual void Shoot()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
        Projectile projectileScript = projectileObject.GetComponent<Projectile>();
        projectileScript.SetTarget(target);
        projectileScript.SetDamage(damage);
        timeUntilFire = 0f; // Reset the time until the next shot
    }
    
    private void FindTarget()
    {
        RaycastHit2D[] hits =
            Physics2D.CircleCastAll(transform.position, range, (Vector2) transform.position, 0f, enemyMask);
    
        if (hits.Length > 0)
        {
            target = hits[0].transform;
            timeUntilFire = 1f / attackSpeed; // Reset the time until the first shot for the new target
        }
    }
    
    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) *
                      Mathf.Rad2Deg + 180f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        rotationPoint.rotation = Quaternion.RotateTowards(rotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= range;
    }

    protected virtual void Upgrade()
    {
        if (upgradeCost > CurrencyManager.Instance.currency) return;
        CurrencyManager.Instance.SpendCurrency(upgradeCost);
        towerLevel += 1;
        upgradeCost *= 2;
        damage += 1;
        attackSpeed = upgradeAttackSpeed();
        range = upgradeRange();
        CloseUpgradeUI();
        Debug.Log("new tower level: " + towerLevel);
        Debug.Log("new tower upgrade cost: " + upgradeCost);
        Debug.Log("new tower damage: " + damage);
        Debug.Log("new tower attackSpeed: " + attackSpeed);
        Debug.Log("new tower range: " + range);
    }
    
    private void Sell()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Building)
        {
            return;
        }
        // Add half of the tower cost to the player's currency
        int sellValue = towerCost / 2;
        CurrencyManager.Instance.IncreaseCurrency(sellValue);

        // Get the Plot script of the parent game object
        Plot plotScript = GetComponentInParent<Plot>();
        if (plotScript != null)
        {
            Debug.Log("removing tower");
            plotScript.RemoveTower();
        }

        // Destroy the tower game object
        Destroy(gameObject);
        UIManager.Instance.SetHoveringState(false);
    }

    
    private float upgradeAttackSpeed()
    {
        return baseAttackSpeed * Mathf.Pow(towerLevel, 0.5f);
    }

    private float upgradeRange()
    {
        return baseRange * Mathf.Pow(towerLevel, 0.2f);
    }

    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }
    
    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.Instance.SetHoveringState(false);
    }

}