using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningSumArray
{
    public class FileSystem
    {

        public class File
        {
            public bool isfile = false;
            public Dictionary<string, File> files = new Dictionary<string, File>();
            public string content = "";
        }

        File root;

        public FileSystem()
        {
            root = new File();
        }

        public IList<string> Ls(string path)
        {
            File t = root;
            List<string> files = new List<string>();
            if (!path.Equals("/"))
            {
                string[] d = path.Split('/');
                for (int i = 1; i < d.Length; i++)
                {
                    t = t.files[d[i]];
                }
                if (t.isfile)
                {
                    files.Add(d[d.Length - 1]);
                    return files;
                }
            }

            List<string> res_files = new List<string>(t.files.Keys);
            res_files.Sort();
            return res_files;
        }

        public void Mkdir(string path)
        {
            File t = root;
            string[] d = path.Split('/');
            for (int i = 1; i < d.Length; i++)
            {
                if (!t.files.ContainsKey(d[i]))
                {
                    t.files.Add(d[i], new File());
                }

                t = t.files[d[i]];
            }
        }

        public void AddContentToFile(string filePath, string content)
        {
            File t = root;
            string[] d = filePath.Split('/');
            for (int i = 1; i < d.Length - 1; i++)
            {
                t = t.files[d[i]];
            }

            if (!t.files.ContainsKey(d[d.Length - 1]))
                t.files.Add(d[d.Length - 1], new File());

            t = t.files[d[d.Length - 1]];
            t.isfile = true;
            t.content = t.content + content;
        }

        public string ReadContentFromFile(string filePath)
        {

            File t = root;
            string[] d = filePath.Split('/');
            for (int i = 1; i < d.Length - 1; i++)
            {
                t = t.files[d[i]];
            }
            return t.files[d[d.Length - 1]].content;
        }
    }

    /**
     * Your FileSystem object will be instantiated and called as such:
     * FileSystem obj = new FileSystem();
     * IList<string> param_1 = obj.Ls(path);
     * obj.Mkdir(path);
     * obj.AddContentToFile(filePath,content);
     * string param_4 = obj.ReadContentFromFile(filePath);
     */
}
