using System;
using UnityEngine;

public class DOTReciever : MonoBehaviour
{
    public void Initialize() {
        //bunu ister health ister np_maincontroller ve p_maincontroller in altina bagla
    }
    public void RecieveDOT(DOTInfo dotInfo) {
        MainManager.Instance.GameManager.DOTManager.Register(this); 
        //buradan gelio bilgiler listeye kayit burasi
        //buraya damager'den ulascaz
    }

    public void RecieveDOTDamage() { 
        //burda hasarlar dagitiliyor
    }
    public void ClearDOT() {
        //listeyi temizleme
    }
}
