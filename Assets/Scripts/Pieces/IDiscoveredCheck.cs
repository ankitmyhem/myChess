using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDiscoveredCheck
{

    public bool DiscoveredCheck(out RaycastHit pinnedPiece, out bool isDiscovered);

}
