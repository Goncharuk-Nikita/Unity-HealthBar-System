using System;
using UnityEngine;

public class Health : MonoBehaviour
{
	private const float MinHealth = 0.0f;
	private const float DefaultMaxHealth = 100;
	private const float DeadPoint = 0.5f;

	public static event Action<Health> HealthAdded =
		health => { };
	public static event Action<Health> HealthRemoved =
		health => { };
	
	
	public float maxHealth = DefaultMaxHealth;
	
	[HideInInspector]
	public float startHealth;

	private float _currentHealth;
	public float CurrentHealth
	{
		get {return _currentHealth;} 
		private set
		{
			if (_currentHealth.Equals(value))
			{
				return;
			}

			_currentHealth = value;
			OnHealthChanged(_currentHealth / maxHealth);
		}
	}

	public bool IsAlive => 
		CurrentHealth > DeadPoint;

	public Vector3 TargetPosition => 
		transform.position;
	
	
	public event Action<float> HealthChanged = 
		health => { };


	private void Start()
	{
		CurrentHealth = startHealth;
	}
	
	private void OnEnable()
	{
		OnHealthAdded(this);
	}

	private void OnDisable()
	{
		OnHealthRemoved(this);
	}


	public void TakeDamage(float count)
	{		
		ModifyHealth(-count);
	}

	public void Heal(float count)
	{
		ModifyHealth(count);
	}

	public void ModifyHealth(float count)
	{
		var newHealth = Mathf.Clamp(
			value: _currentHealth + count, 
			min: MinHealth, 
			max: maxHealth);
		
		CurrentHealth = newHealth;
	}
	
	
	private static void OnHealthAdded(Health health)
	{
		HealthAdded(health);
	}

	private static void OnHealthRemoved(Health health)
	{
		HealthRemoved(health);
	}

	protected virtual void OnHealthChanged(float count)
	{
		HealthChanged.Invoke(count);
	}

	
	
	public void Init()
	{ }

	public void Spawned()
	{ }

	public void Despawned()
	{ }
}
