using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

/* INCLUDED IN EVERYTHING SDK @voidtools.com */

namespace SearchEverythingCoreCLI
{
    public class Everything
    {
        private const int EVERYTHING_OK = 0;
        private const int EVERYTHING_ERROR_MEMORY = 1;
        private const int EVERYTHING_ERROR_IPC = 2;
        private const int EVERYTHING_ERROR_REGISTERCLASSEX = 3;
        private const int EVERYTHING_ERROR_CREATEWINDOW = 4;
        private const int EVERYTHING_ERROR_CREATETHREAD = 5;
        private const int EVERYTHING_ERROR_INVALIDINDEX = 6;
        private const int EVERYTHING_ERROR_INVALIDCALL = 7;

        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern int Everything_SetSearchW(string lpSearchString);

        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMatchPath(bool bEnable);

        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMatchCase(bool bEnable);

        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMatchWholeWord(bool bEnable);

        [DllImport("Everything64.dll")]
        public static extern void Everything_SetRegex(bool bEnable);

        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMax(int dwMax);

        [DllImport("Everything64.dll")]
        public static extern void Everything_SetOffset(int dwOffset);

        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetMatchPath();

        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetMatchCase();

        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetMatchWholeWord();

        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetRegex();

        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetMax();

        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetOffset();

        [DllImport("Everything64.dll")]
        public static extern string Everything_GetSearchW();

        [DllImport("Everything64.dll")]
        public static extern int Everything_GetLastError();

        [DllImport("Everything64.dll")]
        public static extern bool Everything_QueryW(bool bWait);

        [DllImport("Everything64.dll")]
        public static extern void Everything_SortResultsByPath();

        [DllImport("Everything64.dll")]
        public static extern int Everything_GetNumFileResults();

        [DllImport("Everything64.dll")]
        public static extern int Everything_GetNumFolderResults();

        [DllImport("Everything64.dll")]
        public static extern int Everything_GetNumResults();

        [DllImport("Everything64.dll")]
        public static extern int Everything_GetTotFileResults();

        [DllImport("Everything64.dll")]
        public static extern int Everything_GetTotFolderResults();

        [DllImport("Everything64.dll")]
        public static extern int Everything_GetTotResults();

        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsVolumeResult(int nIndex);

        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsFolderResult(int nIndex);

        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsFileResult(int nIndex);

        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern void Everything_GetResultFullPathNameW(int nIndex, StringBuilder lpString, int nMaxCount);

        [DllImport("Everything64.dll")]
        public static extern void Everything_Reset();

        public static List<string> Search(string query)
        {
            var results = new List<string>();

            int i;
            const int bufsize = 512;
            var buf = new StringBuilder(bufsize);

            // set the search
            Everything_SetSearchW(query);

            // execute the query
            Everything_QueryW(true);

            // sort by path
            // Everything_SortResultsByPath();

            // set the window title
            var count = Everything_GetNumResults();

            // loop through the results, adding each result to the listbox.
            for (i = 0; i < count; i++)
            {
                // get the result's full path and file name.
                Everything_GetResultFullPathNameW(i, buf, bufsize);

                // add it to the list box				
                results.Add(buf.ToString());
            }

            return results;
        }

    }
}
