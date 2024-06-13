using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Building
{
    bool checker(GameObject gameObject,int produce);
    void inputer(int Produce);
    void enableScript();
    void disableScript();
}
