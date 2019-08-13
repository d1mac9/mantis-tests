﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.FtpClient;
namespace mantis_tests
{
    public class FTPHelper : HelperBase
    {
        private FtpClient client;
        public FTPHelper(ApplicationManager manager) : base(manager)
        {
            client = new FtpClient();
            client.Host = "localhost";
            client.Credentials = new System.Net.NetworkCredential("user1", "user1");
            client.Connect();
        }

        public void BackupFile(String path)
        {
            String backupPath = path + ".bak";
            if (client.FileExists(backupPath))
            {
                return;
            }
            client.Rename(path, backupPath);
        }
        public void RestoreBackupFile(String path)
        {
            String backupPath = path + ".bak";
            if (!client.FileExists(backupPath))
            {
                return;
            }
            if (client.FileExists(path))
            {
                client.DeleteFile(path);
            }
            client.Rename(backupPath, path);
        }
        public void UploadFile(String path, Stream localFile)
        {
            if (client.FileExists(path))
            {
                client.DeleteFile(path);
            }
            using (Stream ftpSream = client.OpenWrite(path))
            {
                byte[] buffer = new byte[8 * 1024];
                int count = localFile.Read(buffer, 0, buffer.Length);
                while (count > 0)
                {
                    ftpSream.Write(buffer, 0, count);
                    count = localFile.Read(buffer, 0, buffer.Length);
                }
            }
        }
    }
}