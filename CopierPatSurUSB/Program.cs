using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Management;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopierPatSurUSB
{
    class Program
    {
        private static bool fait = false;
        private static ManagementEventWatcher watcher = null;
        private static ManagementEventWatcher watcherOut = null;
        private static string nomPat = string.Empty;

        public static void Main(string[] args)
        {
            nomPat = ConfigurationManager.AppSettings["RepSource"];
            Console.WriteLine(string.Format("Source du fichier .pat = {0}", nomPat));
            if (!System.IO.File.Exists(nomPat))
            {
                Console.WriteLine("Attention, le fichier n'existe pas, chosissez en un autre");
            }

            Console.WriteLine();
            Console.WriteLine("Menu : Q = Quitter, F = Choisir fichier");
            Console.WriteLine();
            Console.WriteLine("Insérer la clé USB et le fichier y sera copié");
            Console.WriteLine("Retirer la clé USB une fois la copie effectuée");

            watcher = new ManagementEventWatcher();
            var query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
            watcher.EventArrived += watcher_EventArrived;
            watcher.Query = query;
            watcher.Start();

            watcherOut = new ManagementEventWatcher();
            query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 3");
            watcherOut.EventArrived += watcher_EventArrived2;
            watcherOut.Query = query;
            watcherOut.Start();

            while (true)
            {
                string rep = Console.ReadLine();
                if (!string.IsNullOrEmpty(rep) && rep.ToUpper().StartsWith("F"))
                {
                    OpenFileDialog fld = new OpenFileDialog();
                    fld.AddExtension = true;
                    fld.CheckFileExists = true; 
                    fld.DefaultExt = "*.pat";
                    fld.Filter = "fichier pat files (*.pat)|*.pat|All files (*.*)|*.*";
                    if (fld.ShowDialog() == DialogResult.OK)
                    {
                        if (System.IO.File.Exists(fld.FileName))
                        {
                            Console.WriteLine("Le fichier {0} sera celui copié à partir de maintenant");
                            nomPat = fld.FileName;
                        }
                    }

                }
                if (!string.IsNullOrEmpty(rep) && rep.ToUpper().StartsWith("Q"))
                {
                    watcher.Stop();
                    watcher.Dispose();
                    watcher = null;

                    watcherOut.Stop();
                    watcherOut.Dispose();
                    watcherOut = null;
                    break; 
                }
            }
        }

        private static void watcher_EventArrived2(object sender, EventArrivedEventArgs e)
        {
            if (!fait)
            {
                return;
            }

            fait = false;
            watcherOut.EventArrived -= watcher_EventArrived2;
            Console.WriteLine("La clé USB a été retirée");
            Console.WriteLine("Insérer la clé USB et le fichier y sera copié");
            Console.WriteLine("Menu : Q = Quitter, F = Choisir fichier");
            watcher.EventArrived += watcher_EventArrived;
            
        }

        private static void watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            if (fait)
            {
                return;
            }

            watcher.EventArrived -= watcher_EventArrived;
            Console.WriteLine("La clé USB a été insérée");
            SystemSounds.Beep.Play();
            
            while (true)
            {
                var availableDrives = DriveInfo.GetDrives()
           .Where(d => d.IsReady && d.DriveType == DriveType.Removable);
                foreach (var x in availableDrives)
                {
                    
                    System.IO.FileInfo fi = new FileInfo(nomPat);
                    string surUsb = Path.Combine(x.Name, fi.Name);
                    if (File.Exists(surUsb))
                    {
                        File.Delete(surUsb);
                    }

                    Console.WriteLine(string.Format("Copie du fichier {0} dans, {1}", nomPat, x.Name));
                    System.IO.File.Copy(nomPat, surUsb);
                    Console.WriteLine("Retirer la clé USB");
                    SystemSounds.Beep.Play();
                    SystemSounds.Beep.Play();
                    fait = true;
                    watcherOut.EventArrived += watcher_EventArrived2;
                    return;
                }

                System.Threading.Thread.Sleep(1000);
            }

        }
    }
}
