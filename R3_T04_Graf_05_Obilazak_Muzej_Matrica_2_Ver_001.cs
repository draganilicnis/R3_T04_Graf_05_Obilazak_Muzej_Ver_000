// R3 T04 Graf 05 Obilazak: Primer: Muzej:  https://petlja.org/sr-Latn-RS/kurs/17918/4/5358#id11 Implicitna reperzentacija
using System;
// using System.Collections.Generic;

class R3_T04_Graf_05_Obilazak_Muzej_Matrica_2_Ver_001
{
    static int[,] Graf_Predstavljanje_Matrica_A_Ini()
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
    static int[,] Graf_Predstavljanje_Matrica_B_Ini(int[,] A)
    {
        int[,] B = new int[5, 5];
        for (int i = 0; i < 5; i++) for (int j = 0; j < 5; j++) B[i, j] = (A[i, j] > 0) ? 1 : 0;
        return B;
    }
    static void Main()
    {
        int[,] A = Graf_Predstavljanje_Matrica_A_Ini();
        int[,] B = Graf_Predstavljanje_Matrica_B_Ini(A);
        int X = 2;      // X start = 2
        int Y = 2;      // Y start = 2
        Graf_Obilazak_DFS_Matrica_Rucno_Ver_001(A, B, X, Y);
    }

    static void Graf_Obilazak_DFS_Poseti_cvor_Zapamti_da_si_posetio_cvor_XY_ulazna_obrada(int[,] A, int[,] G, int X, int Y, ref int brojac_cvorova_poseta_ulaz)
    {
        Console.WriteLine(A[X, Y] + " " + brojac_cvorova_poseta_ulaz);
        G[X, Y]++;      // G[X, Y] = 1;
        brojac_cvorova_poseta_ulaz++;
    }
    static void Graf_Obilazak_DFS_Poseti_cvor_Zapamti_da_si_posetio_cvor_XY_izlazna_obrada(int[,] A, int[,] G, int X, int Y, ref int brojac_cvorova_poseta_izlaz)
    {
        Console.WriteLine(A[X, Y] + " " + brojac_cvorova_poseta_izlaz + " : Povratak");
        G[X, Y]++;      // G[X, Y] = 2;
        brojac_cvorova_poseta_izlaz++;
    }
    static void Graf_Obilazak_DFS_Matrica_Rucno_Ver_001(int[,] A, int[,] G, int X, int Y)
    {
        int N = A.GetLength(0);                         // Dimenzija matrice A: Broj redova
        int M = A.GetLength(1);                         // Dimenzija matrice A: Broj kolona
        int[] DX = new int[4] { -1, +0, +1, +0 };       // S Z J I  // TLBR: Gore, Levo, Dole, Desno
        int[] DY = new int[4] { +0, -1, +0, +1 };       // S Z J I  // TLBR: Gore, Levo, Dole, Desno
        int smer = 0;

        int brojac_cvorova_poseta_ulaz = 0;             // Brojac cvorova koji su prvi put poseceni
        int brojac_cvorova_poseta_izlaz = 0;            // Brojac cvorova koji su poslednji put poseceni
        int susednih_1 = 4;                             // Brojac mogucih susednih cvorova koji imaju 1
        int susednih_2 = 4;                             // Brojac mogucih susednih cvorova koji imaju 2
        bool bObilazak_Kraj = false;                    // Obilazak vise nije moguc

        while (!bObilazak_Kraj)
        {
            if (G[X, Y] == 1)
            {
                Graf_Obilazak_DFS_Poseti_cvor_Zapamti_da_si_posetio_cvor_XY_ulazna_obrada(A, G, X, Y, ref brojac_cvorova_poseta_ulaz);
                // susednih_1 = 4;
                // susednih_2 = 4;
            }
            int x = X + DX[smer];
            int y = Y + DY[smer];
            if (x >= 0 && x < N && y >= 0 && y < M && G[x, y] == 1)
            {
                X = x;
                Y = y;
                susednih_1 = 4;
            }
            else
            {
                susednih_1--;
                if (susednih_1 >= 0) smer = (smer + 1) % 4;
            }

            if (susednih_1 <= 0)
            {
                if (G[X, Y] == 2)
                {
                    Graf_Obilazak_DFS_Poseti_cvor_Zapamti_da_si_posetio_cvor_XY_izlazna_obrada(A, G, X, Y, ref brojac_cvorova_poseta_izlaz);
                    susednih_1 = 4;
                    susednih_2 = 4;
                }
                x = X + DX[smer];
                y = Y + DY[smer];
                if (x >= 0 && x < N && y >= 0 && y < M && G[x, y] == 1)
                {
                    X = x;
                    Y = y;
                    susednih_1 = 4;
                }
                else if (x >= 0 && x < N && y >= 0 && y < M && G[x, y] == 2)
                {
                    X = x;
                    Y = y;
                    susednih_2 = 4;
                }
                else
                {
                    susednih_2--;
                    smer = (smer + 1) % 4;
                    if (susednih_2 <= 0) bObilazak_Kraj = true;
                }
            }
        }
    }
}

// https://codeblog.rs/clanci/bfs_i_dfs#uvod
// https://www.youtube.com/watch?v=84jNzUOY78c
