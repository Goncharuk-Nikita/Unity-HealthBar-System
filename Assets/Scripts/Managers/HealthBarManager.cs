using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
	public Camera targetCamera;

	[SerializeField] private PrefabData _healthBarPrefab;
	
	private Dictionary<Health, HealthBar> _healthBars;

	private void Awake()
	{
		_healthBars = new Dictionary<Health, HealthBar>();

		_healthBarPrefab.Install();
		
		Health.HealthAdded += AddHealth;
		Health.HealthRemoved += RemoveHealth;
	}

	private void OnDestroy()
	{
		Health.HealthAdded -= AddHealth;
		Health.HealthRemoved -= RemoveHealth;
	}
	

	
	private void AddHealth(Health health)
	{
		var healthBar = CreateHealthBar(health);
		
		_healthBars.Add(health, healthBar);
	}

	private HealthBar CreateHealthBar(Health health)
	{
		var poolablePrefabData = _healthBarPrefab.Spawn();

		var healthBar = (HealthBar)poolablePrefabData.poolableComponent;

		healthBar.SetHealth(health);

		return healthBar;
	}

	private void RemoveHealth(Health health)
	{
		if (!_healthBars.TryGetValue(health, out HealthBar healthBar))
		{
#if UNITY_EDITOR
			Debug.LogError("Health not found in HealthBars dictionary!", this);
#endif
			return;
		}

		_healthBars.Remove(health);
		PrefabPoolingSystem.Despawn(healthBar.gameObject);
	}

	
	
	private void LateUpdate()
	{
		foreach (var keyValue in _healthBars)
		{
			var healthBar = keyValue.Value;
			
			healthBar.FollowTarget();
			healthBar.LookAt(targetCamera.transform);
		}
	}
}
