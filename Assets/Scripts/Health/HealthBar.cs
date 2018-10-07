using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IPoolableComponent
{
	public float updateSpeedSeconds = 0.2f;
	
	[SerializeField] public Vector3 offset;
	
	[Header("Dependencies")]
	[SerializeField] private Image healthImage;

	private Health _health;
	

	// Poolable handlers
	
	public void Init()
	{ }

	public void Spawned()
	{ }

	public void Despawned()
	{
		_health = null;
	}

	
	private void OnDisable()
	{
		if (_health != null)
		{
			_health.HealthChanged -= SetLives;
		}
	}


	public void SetHealth(Health health)
	{
		_health = health;
		
		_health.HealthChanged += SetLives;
	}
	
	private void SetLives(float fill)
	{
		healthImage.DOFillAmount(fill, updateSpeedSeconds);
	}


	
	public void FollowTarget()
	{
		transform.position = 
			_health.TargetPosition + offset;
	}
	
	public void LookAt(Transform target)
	{
		transform.LookAt(target);
	}
}
