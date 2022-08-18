using System;
using StormLib;
using Test;

namespace StormLibExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Mpq.ExtractFile("DBFilesClient\\Item-sparse.db2", "Item-sparse.db2", OpenFile.SFILE_OPEN_FROM_MPQ);
            Mpq.ExtractFile("DBFilesClient\\ItemCurrencyCost.db2", "ItemCurrencyCost.db2", OpenFile.SFILE_OPEN_FROM_MPQ);
            
            Mpq.Close();

            Console.ReadKey();
        }
    }
}
