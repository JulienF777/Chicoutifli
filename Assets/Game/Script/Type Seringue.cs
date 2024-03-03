using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeSeringue
{
    public float [] statsSeringueVieNiveau1 = new float[4];
    public float [] statsSeringueVitesseNiveau1 = new float[4];
    public float [] statsSeringueAttaqueNiveau1 = new float[4];

    public float [] statsSeringueVieNiveau2 = new float[4];
    public float [] statsSeringueVitesseNiveau2 = new float[4];
    public float [] statsSeringueAttaqueNiveau2 = new float[4];

    public float [] statsSeringueVieNiveau3 = new float[4];
    public float [] statsSeringueVitesseNiveau3 = new float[4];
    public float [] statsSeringueAttaqueNiveau3 = new float[4];


    public TypeSeringue()
    {
        //------------------Niveau 1------------------
        //------Stats Seringue Vie Niveau 1------
        //pv
        statsSeringueVieNiveau1[0] = Random.Range(50, 101);
        //vitesse
        statsSeringueVieNiveau1[1] = Random.Range(1, 6);
        //attaque
        statsSeringueVieNiveau1[2] = 0;
        //kiting
        statsSeringueVieNiveau1[3] = Random.Range(0.1f, 0.26f);

        //------Stats Seringue Attaque Niveau 1------
        //pv
        statsSeringueAttaqueNiveau1[0] = Random.Range(20, 51);
        //vitesse
        statsSeringueAttaqueNiveau1[1] = Random.Range(5, 11);
        //attaque
        statsSeringueAttaqueNiveau1[2] = Random.Range(0, 3);
        //kiting
        statsSeringueAttaqueNiveau1[3] = Random.Range(0.1f, 0.26f);

        //------Stats Seringue Vitesse Niveau 1------
        //pv
        statsSeringueVitesseNiveau1[0] = Random.Range(20, 51);
        //vitesse
        statsSeringueVitesseNiveau1[1] = Random.Range(1, 6);
        //attaque
        statsSeringueVitesseNiveau1[2] = Random.Range(2, 6);
        //kiting
        statsSeringueVitesseNiveau1[3] = Random.Range(0.25f, 0.51f);

        //------------------Niveau 2------------------

        //------Stats Seringue Vie Niveau 2------
        //pv
        statsSeringueVieNiveau2[0] = Random.Range(100, 151);
        //vitesse
        statsSeringueVieNiveau2[1] = Random.Range(6, 11);
        //attaque
        statsSeringueVieNiveau2[2] = 0;
        //kiting
        statsSeringueVieNiveau2[3] = Random.Range(0.25f, 0.51f);

        //------Stats Seringue Attaque Niveau 2------
        //pv
        statsSeringueAttaqueNiveau2[0] = Random.Range(50, 101);
        //vitesse
        statsSeringueAttaqueNiveau2[1] = Random.Range(10, 16);
        //attaque
        statsSeringueAttaqueNiveau2[2] = Random.Range(2, 9);
        //kiting
        statsSeringueAttaqueNiveau2[3] = Random.Range(0.25f, 0.51f);

        //------Stats Seringue Vitesse Niveau 2------
        //pv
        statsSeringueVitesseNiveau2[0] = 0;
        //vitesse
        statsSeringueVitesseNiveau2[1] = Random.Range(6, 12);
        //attaque
        statsSeringueVitesseNiveau2[2] = Random.Range(8, 11);
        //kiting
        statsSeringueVitesseNiveau2[3] = Random.Range(0.5f, 1.1f);
    }
}
