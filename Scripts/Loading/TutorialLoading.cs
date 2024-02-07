using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialLoading : LoadingOperation
{
    public override void Load()
    {
        bool isTutorialPassed = ES3.Load(SaveId.IsTutorialPassed, false);
        if(isTutorialPassed == false)
            ES3.DeleteFile();
        Finish();
    }
}
