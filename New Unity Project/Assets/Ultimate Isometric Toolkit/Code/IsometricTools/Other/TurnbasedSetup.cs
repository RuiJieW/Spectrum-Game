using UnityEngine;
using System.Collections;

/// <summary>
/// Small helper class used for initialization in the TurnBasedScene
/// </summary>
public class TurnbasedSetup : Singleton<TurnbasedSetup> {

    public TurnbasedIsoObjectController playerPrototype;

	[SerializeField]
	public GenericGridMap<IsoObject> map;

	public IsoObject[] prototypes;

    void Awake() {
		//create new local stored instance of map
		map = new GenericGridMap<IsoObject>(new Vector3(1,1, .68f), new Vector3(5,5,3));
		//apply function that create an instance relative to its height
		map.applyFunctionToMap((x, y, z) =>
		{
			return GameObject.Instantiate(prototypes[z]) as IsoObject;
		});

		//instantiate player
        var player = instantiatePlayer(new Vector3(0, 0, 3), map.tileSize);

		//init values
        player.init(map, new Vector3(0, 0, 3));
    }

    TurnbasedIsoObjectController instantiatePlayer(Vector3 mapPos, Vector3 tileSize) {
		var player = GameObject.Instantiate(playerPrototype) as TurnbasedIsoObjectController;
        player.IsoObj.Position = Vector3.Scale(mapPos, tileSize);
        var z = tileSize.z / 2;
        player.IsoObj.Position += new Vector3(0, 0, z);

		return player;
    }

    
 
	
}
