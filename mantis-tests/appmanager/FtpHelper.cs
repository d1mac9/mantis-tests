﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.FtpClient;

namespace mantis_tests
{
    public class FtpHelper : HelperBase
    {
        public FtpHelper(ApplicationManager manager) : base(manager)
        {
            client = new FtpClient();
            client.Host = "localhost";
            client.Credentials = new System.Net.NetworkCredential("mantis", "mantis");
            client.Connect();
        }

        private FtpClient client;

        public void BackupFile(String path)//Сущестсвующий конфиг забэкапить
        {
            String backupPath = path + ".bak";
            if (client.FileExists(backupPath))
            {
                return;
            }
            client.Rename(path, backupPath);
        }


        public void RestoreBackupFile(String path)//Восстанивить файл из резервной копии
        {
            String backupPath = path + ".bak";
            if (! client.FileExists(backupPath))
            {
                return;
            }
            if (client.FileExists(path))
            {
                client.DeleteFile(path);
            }

            client.Rename(backupPath, path);

        }

        public void Upload(String path, Stream localfile)
        {
            if (client.FileExists(path))
            {
                client.DeleteFile(path);
            }


            using (Stream ftpStream = client.OpenWrite(path))
            {
                byte[] buffer = new byte[8 * 1024];
                int count = localfile.Read(buffer, 0, buffer.Length);
                while (count >0)
                {
                    ftpStream.Write(buffer, 0, count);
                    count = localfile.Read(buffer, 0, buffer.Length);
                }


            }
        }

    }
}
