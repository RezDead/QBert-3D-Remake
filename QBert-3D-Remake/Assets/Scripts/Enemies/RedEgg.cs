using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEgg : BaseEnemy
{
    protected override void AfterDescend()
    {
        Destroy(this.gameObject);
    }
}
