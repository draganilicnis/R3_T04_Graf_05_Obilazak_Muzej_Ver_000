// R3 T04 Graf 05 Obilazak: Primer: Muzej:  https://petlja.org/sr-Latn-RS/kurs/17918/4/5358
// R3 T04 Graf 05 Obilazak: Primer: Skakac: https://petlja.org/sr-Latn-RS/kurs/17918/4/5356#id11 Implicitna reperzentacija
using System;
// using System.Collections.Generic;

class R3_T04_Graf_05_Obilazak_Z_Muzej_Ver_000
{
    static int[,] Graf_Predstavljanje_Matrica_Ini()
    {
        int[,] A = new int[5, 5]
        {
            { 1, 1, 1, 1, 1 },      // ABCDE
            { 1, 0, 1, 0, 1 },      // F G H
            { 1, 0, 1, 0, 1 },      //   I
            { 1, 0, 1, 0, 1 },      // J K L
            { 1, 1, 1, 1, 1 }       // MNOPQ
        };
        return A;
    }
    static void Main()
    {
        int[,] A = Graf_Predstavljanje_Matrica_Ini();
        int X = 2;      // X start = 2
        int Y = 2;      // Y start = 2
        Graf_Obilazak_DFS_Matrica_Rucno_Ver_000(A, X, Y);
    }

    static void Graf_Obilazak_DFS_Matrica_Rucno_Ver_000(int[,] A, int X, int Y)
    {
        int N = A.GetLength(0);     // Dimenzija matrice A: Broj redova
        int M = A.GetLength(1);     // Dimenzija matrice A: Broj kolona
        int[] DX = new int[4] { +0, -1, +0, +1 };       // Z S I J
        int[] DY = new int[4] { -1, +0, +1, +0 };       // Z S I J

        int smer = 0;
        bool bPovratak = false;
        int x = X + DX[smer];
        int y = Y + DY[smer];

        while (smer < 4)
        {
            if (A[X, Y] == 1)
            {
                Console.WriteLine(X + "," + Y);
                A[X, Y]++;      // A[X, Y] = 2;
            }
            x = X + DX[smer];
            y = Y + DY[smer];
            if (x >= 0 && x < N && y >= 0 && y < M && A[x, y] == 1)
            {
                X = x;
                Y = y;
                smer = 0;
            }
            else smer++;

            if (smer == 4)
            {
                bPovratak = true;
                smer = 0;
                while (smer < 4 && bPovratak)
                {
                    if (A[X, Y] == 2)
                    {
                        Console.WriteLine(X + "," + Y + ": Povratak");
                        A[X, Y]++;      // A[X, Y] = 2;
                    }
                    x = X + DX[smer];
                    y = Y + DY[smer];
                    if (x >= 0 && x < N && y >= 0 && y < M && A[x, y] == 1)
                    {
                        X = x;
                        Y = y;
                        smer = 0;
                        bPovratak = false;
                    }
                    else if (x >= 0 && x < N && y >= 0 && y < M && A[x, y] == 2)
                    {
                        X = x;
                        Y = y;
                        smer = 0;
                    }
                    else smer++;
                }
            }
        }
    }
}
