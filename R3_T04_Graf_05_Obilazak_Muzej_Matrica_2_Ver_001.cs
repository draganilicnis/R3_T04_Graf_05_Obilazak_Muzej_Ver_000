// R3 T04 Graf 05 Obilazak: Primer: Muzej:  https://petlja.org/sr-Latn-RS/kurs/17918/4/5358#id11 Implicitna reperzentacija
using System;
// using System.Collections.Generic;

class R3_T04_Graf_05_Obilazak_Muzej_Matrica_2_Ver_001
{
    static int[,] Graf_Matrica_A_Ini()
    {
        int[,] A = new int[5, 5]
        {
            { 11, 12, 13, 14, 15 },      // ABCDE
            { 21, -1, 23, -1, 25 },      // F G H
            { -1, -1, 33, -1, -1 },      // X I Y
            { 41, -1, 43, -1, 45 },      // J K L
            { 51, 52, 53, 54, 55 }       // MNOPQ
        };
        return A;
    }
    static int[,] Graf_Matrica_B_Ini(int[,] A)
    {
        int[,] B = new int[5, 5];
        for (int i = 0; i < 5; i++) for (int j = 0; j < 5; j++) B[i, j] = (A[i, j] > 0) ? 1 : 0;
        return B;
    }
    static void Main()
    {
        int[,] A = Graf_Matrica_A_Ini();
        int[,] B = Graf_Matrica_B_Ini(A);
        int X = 2;      // X start = 2 : Pocetna pozicija
        int Y = 2;      // Y start = 2 : Pocetna pozicija
        DFS_Matrica_Rucno_Ver_002(A, B, X, Y);
    }
    static void DFS_Matrica_Rucno_Ver_002(int[,] A, int[,] G, int X, int Y)
    {
        int smer = 0;
        int brojac_posecenih_cvorova_ulaz = 0;          // Brojac cvorova koji su prvi put poseceni
        int brojac_posecenih_cvorova_izlaz = 0;         // Brojac cvorova koji su poslednji put poseceni

        bool bObilazak_Kraj = false;                    // Obilazak vise nije moguc
        while (!bObilazak_Kraj)
        {
            if (G[X, Y] == 1) DFS_Poseti_cvor_XY_i_Zapamti_da_si_ga_posetio(A, G, X, Y, ref brojac_posecenih_cvorova_ulaz);

            bool bPostoji_Susedni_Cvor_koji_jos_uvek_nije_posecen = DFS_Postoji_Susedni_Cvor_koji_jos_uvek_nije_posecen(ref X, ref Y, G, smer);
            if (!bPostoji_Susedni_Cvor_koji_jos_uvek_nije_posecen)
            {
                if (G[X, Y] == 2) DFS_Poseti_cvor_XY_i_Zapamti_da_si_ga_posetio(A, G, X, Y, ref brojac_posecenih_cvorova_izlaz, true);
                bool bPostoji_Susedni_Cvor_stanje_2 = DFS_Postoji_Susedni_Cvor_koji_jos_uvek_nije_posecen(ref X, ref Y, G, smer, 2);
                if (!bPostoji_Susedni_Cvor_stanje_2) bObilazak_Kraj = true;
            }
        }
    }
    static void DFS_Poseti_cvor_XY_i_Zapamti_da_si_ga_posetio(int[,] A, int[,] G, int X, int Y, ref int brojac_posecenih_cvorova, bool bIzlazna_obrada = false)
    {
        string sKomentar_Povratak = (!bIzlazna_obrada) ? "" : " : Povratak";
        Console.WriteLine(A[X, Y] + " " + brojac_posecenih_cvorova + " " + sKomentar_Povratak);
        G[X, Y]++;      // G[X, Y] = 1;
        brojac_posecenih_cvorova++;
    }

    static bool DFS_Postoji_Susedni_Cvor_koji_jos_uvek_nije_posecen(ref int X, ref int Y, int[,] G, int smer, int VREDNOST = 1)
    {
        int N = G.GetLength(0);                         // Dimenzija matrice G: Broj redova
        int M = G.GetLength(1);                         // Dimenzija matrice G: Broj kolona
        int[] DX = new int[4] { -1, +0, +1, +0 };       // S Z J I  // TLBR: Gore, Levo, Dole, Desno
        int[] DY = new int[4] { +0, -1, +0, +1 };       // S Z J I  // TLBR: Gore, Levo, Dole, Desno

        bool bSusedni_Cvor_koji_jos_uvek_nije_posecen_Postoji = false;
        for (int cvor_susedni = 0; cvor_susedni < 4; cvor_susedni++)
        {
            int x = X + DX[smer];
            int y = Y + DY[smer];
            if (x >= 0 && x < N && y >= 0 && y < M && G[x, y] == VREDNOST)
            {
                X = x;
                Y = y;
                bSusedni_Cvor_koji_jos_uvek_nije_posecen_Postoji = true;
                break;
            }
            smer = (smer + 1) % 4;
        }
        return bSusedni_Cvor_koji_jos_uvek_nije_posecen_Postoji;
    }
}

// https://codeblog.rs/clanci/bfs_i_dfs#uvod
// https://www.youtube.com/watch?v=84jNzUOY78c
