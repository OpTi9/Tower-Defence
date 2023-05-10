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
    
    private float rotationSpeed = 200f;
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
        
        upgradeButton.onClick.AddListener(Upgrade);
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
        timeUntilFire = 0f;
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits =
            Physics2D.CircleCastAll(transform.position, range, (Vector2) transform.position, 0f, enemyMask);
        
        if (hits.Length > 0)
        {
            target = hits[0].transform;
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

    public void Upgrade()
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