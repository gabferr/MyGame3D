using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemStrong : ClothItemBase
    {
        public float damageMultiply  = 2f;
        public new float duration = 1f;
        public override void Collect()
        {
            base.Collect();
            Player.Instance.healthBase.ChangeDamageMuliply(damageMultiply, duration);
        }
    }

}