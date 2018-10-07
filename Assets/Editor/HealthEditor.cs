using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace

[CustomEditor(typeof(Health))]
public class HealthEditor : Editor
{
	private Health _health;
	
	
	private void OnEnable()
	{
		_health = (Health) target;

		Health.HealthAdded += health => { };
		Health.HealthRemoved += health => { };
	}

	
	
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		AddStartHealthSlider();
	}


	private void AddStartHealthSlider()
	{
		_health.startHealth = EditorGUILayout.Slider(
			"Start Health",
			value: _health.startHealth, 
			leftValue: 0f, 				
			rightValue: _health.maxHealth);
	}
}
