// R3 T04 Graf 05 Obilazak: Primer: Muzej:  https://petlja.org/sr-Latn-RS/kurs/17918/4/5358#id11 Implicitna reperzentacija
using System;
// using System.Collections.Generic;

class R3_T04_Graf_05_Obilazak_Muzej_Matrica_2_Ver_002
{
    static int[,] Graf_Matrica_A_Ini()          // Matrica (graf) ulazni primer ORIGINAL
    {
        int[,] A = new int[,]
        {
            { 11, 12, 13, 14, 15 },      // ABCDE
            { 21, -1, 23, -1, 25 },      // F G H
            { -1, -1, 33, -1, -1 },      // X I Y
            { 41, -1, 43, -1, 45 },      // J K L
            { 51, 52, 53, 54, 55 }       // MNOPQ
        };
        return A;
    }
    static int[,] Graf_Matrica_G_Ini(int[,] A)  // Matrica pomocna (graf) za obelezavanje posecenih cvorova (0: prepreka, 1: neposecen cvor, 2: posecen prvi put, 3: povratak
    {
        int N = A.GetLength(0);                 // Dimenzija matrice A: Broj redova
        int M = A.GetLength(1);                 // Dimenzija matrice A: Broj kolona
        int[,] B = new int[N, M];
        for (int i = 0; i < N; i++) for (int j = 0; j < M; j++) B[i, j] = (A[i, j] > 0) ? 1 : 0;
        return B;
    }
    static void Main()
    {
        int[,] A = Graf_Matrica_A_Ini();        // Matrica (graf) ORIGINAL
        int[,] G = Graf_Matrica_G_Ini(A);       // Matrica (graf) pomocna obelezavanje posecenih cvorova
        int X = 0;      // X start = 2 : Pocetna pozicija
        int Y = 0;      // Y start = 2 : Pocetna pozicija
        Graf_Obilazak_DFS_Matrica_Rucno_Ver_002(X, Y, G, A);
    }

    static void Graf_Obilazak_DFS_Matrica_Rucno_Ver_002(int X, int Y, int[,] G, int[,] A)
    {
        int smer = 0;
        int brojac_posecenih_cvorova_ulaz = 0;          // Brojac cvorova koji su prvi put poseceni
        int brojac_posecenih_cvorova_izlaz = 0;         // Brojac cvorova koji su poslednji put poseceni

        bool bObilazak_Kraj = false;                    // Obilazak vise nije moguc
        while (!bObilazak_Kraj)
        {
            if (G[X, Y] == 1) DFS_Poseti_cvor_XY_i_Obelezi_da_je_posecen(X, Y, G, A, ref brojac_posecenih_cvorova_ulaz);

            bool bPostoji_Cvor_susedni_koji_nije_posecen = DFS_Cvor_susedni_koji_nije_posecen_Postoji(ref X, ref Y, G, smer);
            if (!bPostoji_Cvor_susedni_koji_nije_posecen)
            {
                if (G[X, Y] == 2) DFS_Poseti_cvor_XY_i_Obelezi_da_je_posecen(X, Y, G, A, ref brojac_posecenih_cvorova_izlaz, true);
                bool bPostoji_Cvor_susedni_Stanje_2 = DFS_Cvor_susedni_koji_nije_posecen_Postoji(ref X, ref Y, G, smer, 2);
                if (!bPostoji_Cvor_susedni_Stanje_2) bObilazak_Kraj = true;
            }
        }
    }
    static void DFS_Poseti_cvor_XY_i_Obelezi_da_je_posecen(int X, int Y, int[,] G, int[,] A, ref int brojac_posecenih_cvorova, bool bIzlazna_obrada = false)
    {
        string sKomentar_Povratak = (bIzlazna_obrada) ? " : Povratak" : "";
        Console.WriteLine(A[X, Y] + " " + brojac_posecenih_cvorova + "" + sKomentar_Povratak);
        G[X, Y]++;      // G[X, Y] = 1;
        brojac_posecenih_cvorova++;
    }

    static bool Graf_Matrica_Provera_da_li_je_cvor_XY_unutar_matrice(int x, int y, int[,] G)
    {
        int N = G.GetLength(0);                 // Dimenzija matrice A: Broj redova
        int M = G.GetLength(1);                 // Dimenzija matrice A: Broj kolona
        return x >= 0 && x < N && y >= 0 && y < M;
    }
    static bool DFS_Cvor_susedni_koji_nije_posecen_Postoji(ref int X, ref int Y, int[,] G, int smer, int VREDNOST = 1, bool bSmer_Reset = false)
    {
        int Smer_Mx = 4;                            // Ukupan broj smerova
        int[] DX = new int[] { -1, +0, +1, +0 };    // S Z J I  // TLBR: Gore, Levo, Dole, Desno
        int[] DY = new int[] { +0, -1, +0, +1 };    // S Z J I  // TLBR: Gore, Levo, Dole, Desno

        if (bSmer_Reset) smer = 0;
        bool bCvor_Susedni_koji_nije_posecen_Postoji = false;
        for (int cvor_susedni = 0; cvor_susedni < Smer_Mx; cvor_susedni++)    // Obilazak svih susednih cvorova
        {
            int x = X + DX[smer];
            int y = Y + DY[smer];
            if (Graf_Matrica_Provera_da_li_je_cvor_XY_unutar_matrice(x, y, G) && G[x, y] == VREDNOST)
            {
                X = x;
                Y = y;
                bCvor_Susedni_koji_nije_posecen_Postoji = true;
                break;
            }
            smer = (smer + 1) % 4;
        }
        return bCvor_Susedni_koji_nije_posecen_Postoji;
    }
}

// https://codeblog.rs/clanci/bfs_i_dfs#uvod
// https://www.youtube.com/watch?v=84jNzUOY78c
// https://petlja.org/sr-Latn-RS/biblioteka/r/Zbirka2/broj_belih_oblasti slican zadatak Broj belih oblasti
