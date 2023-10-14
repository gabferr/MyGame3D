using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemSpeed : ClothItemBase
    {
        public float targetspeed = 2f;
        public float time = 1f;
        public override void Collect()
        {
            base.Collect();
            Player.Instance.changeSpeed(targetspeed, duration);
        }
    }

}