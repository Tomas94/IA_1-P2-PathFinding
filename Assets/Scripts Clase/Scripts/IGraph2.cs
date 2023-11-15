using System.Collections.Generic;
using UnityEngine;

public interface IGraph2
{
   public List<GameObject> GetNeighbors(GameObject current);
}
