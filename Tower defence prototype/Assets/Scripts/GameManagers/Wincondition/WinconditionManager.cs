using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinconditionManager : MonoBehaviour
{
   public static WinconditionManager instance;
   public EnemyCounter counter;

   private void Awake()
   {
      instance = this;
   }
}
