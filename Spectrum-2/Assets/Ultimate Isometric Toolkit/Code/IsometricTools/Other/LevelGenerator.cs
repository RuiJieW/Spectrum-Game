using UnityEngine;
using System.Collections;

/// <summary>
/// Generates procedual levels during runtime
/// </summary>
public class LevelGenerator : MonoBehaviour {
	public Vector3 size = new Vector3(10,10,15);
	public Vector3 tileSize = new Vector3(1,1,.68f);

	public int seed = 1;
	public float ruffness = 1f;
	public float amplitude = 1f;

	/// <summary>
	/// Datastructure to store the IsoObjects in
	/// </summary>
	/// 
	[SerializeField]
	public GenericGridMap<IsoObject> map;

	//prefab to spawn
	public IsoObject prefab;

	public void instantiate() {
		if(map != null) {	
			clear();
			map = new GenericGridMap<IsoObject>(tileSize, size);
			map.applyFunctionToMap((x, y, z) => mapToTile(x, y, z));
		}
		else {	
			map = new GenericGridMap<IsoObject>(tileSize, size);
			map.applyFunctionToMap((x, y, z) => mapToTile(x, y, z));
		}
	}

	/// <summary>
	/// Wraps GenericGridMap<T>.clear() for the custom editor
	/// </summary>
	public void clear()
	{
		map.clear();
		map = null;
	}

	/// <summary>
	/// Returns an instance of prefab or null at a given position (x,y,z)
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="z"></param>
	/// <returns></returns>
	public IsoObject mapToTile(int x, int y, int z) {
		Vector2 vec = new Vector2(x,y) * ruffness + new Vector2(seed, seed);
		float height = Mathf.PerlinNoise(vec.x/size.x, vec.y/size.y);

		if( z <= height * amplitude) {
			//create instance rather than returning the bluepring/prefab
			return GameObject.Instantiate(prefab) as IsoObject;
		} else {
			return null;
		}
	}


	public void setSize(float value) {
		size = new Vector3((int)value,(int)value,(int)value);
	}

	public void setRuffness(float value) {
		ruffness = value;
	}

	public void setAmplitude(float value) {
		amplitude = value;
	}
}
