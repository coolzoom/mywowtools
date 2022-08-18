using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace StormLib
{
    // Flags for SFileOpenArchive
    [Flags]
    public enum OpenArchiveFlags : uint
    {
        //old
        //NO_LISTFILE         = 0x0010,   // Don't load the internal listfile
        //NO_ATTRIBUTES       = 0x0020,   // Don't open the attributes
        //MFORCE_MPQ_V1       = 0x0040,   // Always open the archive as MPQ v 1.00, ignore the "wFormatVersion" variable in the header
        //MCHECK_SECTOR_CRC   = 0x0080,   // On files with MPQ_FILE_SECTOR_CRC, the CRC will be checked when reading file
        //READ_ONLY           = 0x0100,   // Open the archive for read-only access
        //ENCRYPTED           = 0x0200,   // Opens an encrypted MPQ archive (Example: Starcraft II installation)
        BASE_PROVIDER_FILE = 0x00000000,  // Base data source is a file
        BASE_PROVIDER_MAP = 0x00000001,  // Base data source is memory-mapped file
        BASE_PROVIDER_HTTP = 0x00000002,  // Base data source is a file on web server
        BASE_PROVIDER_MASK = 0x0000000F,  // Mask for base provider value
        STREAM_PROVIDER_FLAT = 0x00000000,  // Stream is linear with no offset mapping
        STREAM_PROVIDER_PARTIAL = 0x00000010,  // Stream is partial file (.part)
        STREAM_PROVIDER_MPQE = 0x00000020,  // Stream is an encrypted MPQ
        STREAM_PROVIDER_BLOCK4 = 0x00000030,  // = 0x4000 per block, text MD5 after each block, max = 0x2000 blocks per file
        STREAM_PROVIDER_MASK = 0x000000F0,  // Mask for stream provider value
        STREAM_FLAG_READ_ONLY = 0x00000100,  // Stream is read only
        STREAM_FLAG_WRITE_SHARE = 0x00000200,  // Allow write sharing when open for write
        STREAM_FLAG_USE_BITMAP = 0x00000400,  // If the file has a file bitmap, load it and use it
        STREAM_OPTIONS_MASK = 0x0000FF00,  // Mask for stream options
        STREAM_PROVIDERS_MASK = 0x000000FF,  // Mask to get stream providers
        STREAM_FLAGS_MASK = 0x0000FFFF,  // Mask for all stream flags (providers+options)
        MPQ_OPEN_NO_LISTFILE = 0x00010000,  // Don't load the internal listfile
        MPQ_OPEN_NO_ATTRIBUTES = 0x00020000,  // Don't open the attributes
        MPQ_OPEN_NO_HEADER_SEARCH = 0x00040000,  // Don't search for the MPQ header past the begin of the file
        MPQ_OPEN_FORCE_MPQ_V1 = 0x00080000,  // Always open the archive as MPQ v 1.00, ignore the "wFormatVersion" variable in the header
        MPQ_OPEN_CHECK_SECTOR_CRC = 0x00100000,  // On files with MPQ_FILE_SECTOR_CRC, the CRC will be checked when reading file
        MPQ_OPEN_FORCE_LISTFILE = 0x00400000,  // Force add listfile even if there is none at the moment of opening
        MPQ_OPEN_READ_ONLY = STREAM_FLAG_READ_ONLY
    };

    // Values for SFileExtractFile
    public enum OpenFile : uint
    {
        //FROM_MPQ        = 0x00000000,   // Open the file from the MPQ archive
        //PATCHED_FILE    = 0x00000001,   // Open the file from the MPQ archive
        //BY_INDEX        = 0x00000002,   // The 'szFileName' parameter is actually the file index
        //ANY_LOCALE      = 0xFFFFFFFE,   // Reserved for StormLib internal use
        //LOCAL_FILE      = 0xFFFFFFFF,   // Open the file from the MPQ archive
        SFILE_OPEN_FROM_MPQ = 0x00000000,  // Open the file from the MPQ archive
        SFILE_OPEN_CHECK_EXISTS = 0xFFFFFFFC,  // Only check whether the file exists
        SFILE_OPEN_LOCAL_FILE = 0xFFFFFFFF  // Open a local file
    };

    //public class StormLib
    //{
    //    [DllImport("StormLib.dll")]
    //    public static extern bool SFileOpenArchive(
    //        [MarshalAs(UnmanagedType.LPWStr)] string szMpqName,
    //        uint dwPriority,
    //        [MarshalAs(UnmanagedType.U4)] OpenArchiveFlags dwFlags,
    //        out IntPtr phMpq);

    //    [DllImport("StormLib.dll")]
    //    public static extern bool SFileCloseArchive(IntPtr hMpq);

    //    [DllImport("StormLib.dll")]
    //    public static extern bool SFileExtractFile(
    //        IntPtr hMpq,
    //        [MarshalAs(UnmanagedType.LPStr)] string szToExtract,
    //        [MarshalAs(UnmanagedType.LPWStr)] string szExtracted,
    //        [MarshalAs(UnmanagedType.U4)] OpenFile dwSearchScope);

    //    [DllImport("StormLib.dll")]
    //    public static extern bool SFileOpenPatchArchive(
    //        IntPtr hMpq,
    //        [MarshalAs(UnmanagedType.LPStr)] string szMpqName,
    //        [MarshalAs(UnmanagedType.LPStr)] string szPatchPathPrefix,
    //        uint dwFlags);

    //    [DllImport("StormLib.dll")]
    //    public static extern bool SFileHasFile(IntPtr hMpq,
    //        [MarshalAs(UnmanagedType.LPStr)] string szFileName);
    //}

    internal unsafe class StormDllx64
    {

        [DllImport("MPQ\\StormLib_x64.dll")]
        public static extern bool SFileOpenArchive(
            [MarshalAs(UnmanagedType.LPWStr)] string szMpqName,
            uint dwPriority,
            [MarshalAs(UnmanagedType.U4)] OpenArchiveFlags dwFlags,
            out IntPtr phMpq);

        [DllImport("MPQ\\StormLib_x64.dll")]
        public static extern bool SFileCloseArchive(IntPtr hMpq);

        [DllImport("MPQ\\StormLib_x64.dll")]
        public static extern bool SFileExtractFile(
            IntPtr hMpq,
            [MarshalAs(UnmanagedType.LPStr)] string szToExtract,
            [MarshalAs(UnmanagedType.LPWStr)] string szExtracted,
            [MarshalAs(UnmanagedType.U4)] OpenFile dwSearchScope);

        [DllImport("MPQ\\StormLib_x64.dll")]
        public static extern bool SFileOpenPatchArchive(
            IntPtr hMpq,
            [MarshalAs(UnmanagedType.LPStr)] string szMpqName,
            [MarshalAs(UnmanagedType.LPStr)] string szPatchPathPrefix,
            uint dwFlags);

        [DllImport("MPQ\\StormLib_x64.dll")]
        public static extern bool SFileHasFile(IntPtr hMpq,
            [MarshalAs(UnmanagedType.LPStr)] string szFileName);

    }

    internal unsafe class StormDllx86
    {

        [DllImport("MPQ\\StormLib_x86.dll")]
        public static extern bool SFileOpenArchive(
            [MarshalAs(UnmanagedType.LPWStr)] string szMpqName,
            uint dwPriority,
            [MarshalAs(UnmanagedType.U4)] OpenArchiveFlags dwFlags,
            out IntPtr phMpq);

        [DllImport("MPQ\\StormLib_x86.dll")]
        public static extern bool SFileCloseArchive(IntPtr hMpq);

        [DllImport("MPQ\\StormLib_x86.dll")]
        public static extern bool SFileExtractFile(
            IntPtr hMpq,
            [MarshalAs(UnmanagedType.LPStr)] string szToExtract,
            [MarshalAs(UnmanagedType.LPWStr)] string szExtracted,
            [MarshalAs(UnmanagedType.U4)] OpenFile dwSearchScope);

        [DllImport("MPQ\\StormLib_x86.dll")]
        public static extern bool SFileOpenPatchArchive(
            IntPtr hMpq,
            [MarshalAs(UnmanagedType.LPStr)] string szMpqName,
            [MarshalAs(UnmanagedType.LPStr)] string szPatchPathPrefix,
            uint dwFlags);

        [DllImport("MPQ\\StormLib_x86.dll")]
        public static extern bool SFileHasFile(IntPtr hMpq,
            [MarshalAs(UnmanagedType.LPStr)] string szFileName);

    }


    public class MpqArchiveSet : IDisposable
    {
        private List<MpqArchive> archives = new List<MpqArchive>();
        //private string GameDir = ".\\";

        //public void SetGameDir(string dir)
        //{
        //    GameDir = dir;
        //}

        //public static string GetGameDirFromReg()
        //{
        //    RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Blizzard Entertainment\\World of Warcraft");
        //    if (key == null)
        //        return null;
        //    Object val = key.GetValue("InstallPath");
        //    if (val == null)
        //        return null;
        //    return val.ToString();
        //}
        public bool HasFile(string file)
        {
            foreach (MpqArchive a in archives)
            {
                var r = a.HasFile(file);
                if (r)
                    return true;
            }
            return false;
        }

        public bool AddArchive(string file)
        {
            Console.WriteLine("Adding archive: {0}", file);

            MpqArchive a = new MpqArchive(file,0,
                    OpenArchiveFlags.MPQ_OPEN_NO_LISTFILE |
                    OpenArchiveFlags.MPQ_OPEN_NO_ATTRIBUTES |
                    OpenArchiveFlags.MPQ_OPEN_NO_HEADER_SEARCH |
                    OpenArchiveFlags.MPQ_OPEN_READ_ONLY);
            if (a.IsOpen)
            {
                archives.Add(a);
                Console.WriteLine("Added archive: {0}", file);
                return true;
            }
            Console.WriteLine("Failed to add archive: {0}", file);
            return false;
        }

        public int AddArchives(string[] files)
        {
            int n = 0;
            foreach (string s in files)
                if (AddArchive(s))
                    n++;
            return n;
        }

        public bool ExtractFile(string from, string to, OpenFile dwSearchScope)
        {
            foreach (MpqArchive a in archives)
            {
                var r = a.ExtractFile(from, to, dwSearchScope);
                if (r)
                    return true;
            }
            return false;
        }

        public void Dispose()
        {
            Close();
        }

        public void Close()
        {
            foreach (MpqArchive a in archives)
                a.Close();
            archives.Clear();
        }
    }

    public class MpqLocale
    {
        public static readonly string[] Locales = new string[] {
            "enUS", "koKR", "frFR", "deDE", "zhTW", "esES", "esMX", "ruRU", "enGB", "enTW", "base" };

        public static string GetPrefix(string file)
        {
            foreach (var loc in Locales)
                if (file.Contains(loc))
                    return loc;

            return "base";
        }

        public static string GetPrefixForPatch(string file)
        {
            var dir = Path.GetDirectoryName(file);

            foreach (var loc in Locales)
                if (file.Contains(loc))
                    return String.Empty;

            return "locale";
        }
    }

    public class MpqArchive : IDisposable
    {
        private IntPtr handle = IntPtr.Zero;

        public MpqArchive(string file, uint Prio, OpenArchiveFlags Flags)
        {
            bool r = Open(file, Prio, Flags);
        }

        public bool IsOpen { get { return handle != IntPtr.Zero; } }

        private bool Open(string file, uint Prio, OpenArchiveFlags Flags)
        {
            bool r = Environment.Is64BitProcess
    ? StormDllx64.SFileOpenArchive(file, Prio, Flags, out handle)
    : StormDllx86.SFileOpenArchive(file, Prio, Flags, out handle);

            //if (r)
            //    OpenPatch(file);
            return r;
        }

        //private void OpenPatch(string file)
        //{
        //    var gamedir = MpqArchiveSet.GetGameDirFromReg();

        //    var patches = Directory.GetFiles(gamedir, "Data\\wow-update-*.mpq").ToList();

        //    var prefix = MpqLocale.GetPrefix(file);

        //    if (prefix != "base")
        //    {
        //        patches.RemoveAll(s => s.Contains("base"));

        //        var localePatches = Directory.GetFiles(gamedir, String.Format("Data\\{0}\\wow-update-*.mpq", prefix));

        //        patches.AddRange(localePatches);
        //    }

        //    foreach (var patch in patches)
        //    {
        //        prefix = MpqLocale.GetPrefix(file);
        //        var pref = MpqLocale.GetPrefixForPatch(patch);

        //        if (pref != "locale")
        //            prefix = String.Empty;

        //        Console.WriteLine("Adding patch: {0} with prefix {1}", Path.GetFileName(patch), prefix != String.Empty ? "\"" + prefix + "\"" : "\"\"");
        //        bool r = StormLib.SFileOpenPatchArchive(handle, patch, prefix, 0);
        //        if (!r)
        //            Console.WriteLine("Failed to add patch: {0}", Path.GetFileName(patch));
        //        else
        //            Console.WriteLine("Added patch: {0}", Path.GetFileName(patch));
        //    }
        //}

        public void Dispose()
        {
            Close();
        }

        public bool Close()
        {
            bool r = Environment.Is64BitProcess
                ? StormDllx64.SFileCloseArchive(handle)
                : StormDllx86.SFileCloseArchive(handle);
            if (r)
                handle = IntPtr.Zero;
            return r;
        }

        public bool ExtractFile(string from, string to, OpenFile dwSearchScope)
        {
            var dir = Path.GetDirectoryName(to);

            if (!Directory.Exists(dir) && !String.IsNullOrEmpty(dir))
                Directory.CreateDirectory(dir);

            return Environment.Is64BitProcess
                ? StormDllx64.SFileExtractFile(handle, from, to, dwSearchScope)
                : StormDllx86.SFileExtractFile(handle, from, to, dwSearchScope);
        }

        public bool HasFile(string file)
        {
            return Environment.Is64BitProcess
                ? StormDllx64.SFileHasFile(handle, file)
                : StormDllx86.SFileHasFile(handle, file);
        }

    }
}
