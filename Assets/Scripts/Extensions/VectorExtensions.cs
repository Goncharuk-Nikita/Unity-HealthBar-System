using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions  
{
	public static Vector3 With(this Vector3 source, float? x, float? y, float? z)
	{
		return new Vector3(
			x ?? source.x,
			y ?? source.y,
			z ?? source.z);
	}
}
