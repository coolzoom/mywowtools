using System;
using System.IO;
using StormLib;

namespace Test
{
    static class Mpq
    {
        static readonly string[] archiveNames = {
                    "Expansion3.mpq",
                    "Expansion2.mpq",
                    "Expansion1.mpq",
                    "world.mpq",
                    "art.mpq",
                    "enGB\\locale-enGB.MPQ" };

        static readonly MpqArchiveSet archive = new MpqArchiveSet();
        //static readonly string regGameDir = MpqArchiveSet.GetGameDirFromReg();

        static Mpq()
        {
            //var dir = Path.Combine(regGameDir, "Data\\");
            //archive.SetGameDir(dir);

            //Console.WriteLine("Game dir is {0}", dir);


            //archive.AddArchives(archiveNames);

            //archive.AddArchive("F:\\WOWServer\\Source\\WowClassicGrindBot-Sync\\Json\\MPQ\\common-2.MPQ");
            //archive.AddArchive("G:\\client_3.3.5\\lichking.MPQ");
            archive.AddArchive("G:\\client_3.3.5\\common-2.MPQ");
            archive.AddArchive("G:\\client_3.3.5\\expansion.MPQ");
            archive.AddArchive("G:\\client_3.3.5\\lichking.MPQ");
            bool hasaz = archive.HasFile("World\\Maps\\Azeroth\\Azeroth.wdt");
            archive.ExtractFile("World\\Maps\\Azeroth\\Azeroth.wdt", "F:\\Azeroth.wdt", OpenFile.SFILE_OPEN_FROM_MPQ);
            //World\maps\Northrend\Northrend.wdt
            bool haswlk = archive.HasFile("World\\Maps\\Northrend\\Northrend.wdt");
            bool wlksuccess = archive.ExtractFile("World\\Maps\\Northrend\\Northrend.wdt", "F:\\0.tmp", OpenFile.SFILE_OPEN_FROM_MPQ);

            //maybe becuase of this?
			//https://github.com/ladislav-zezula/StormLib/issues/221
        }

        public static bool ExtractFile(string from, string to, OpenFile dwSearchScope)
        {
            return archive.ExtractFile(from, to, dwSearchScope);
        }

        public static void Close()
        {
            archive.Close();
        }
    }
}
