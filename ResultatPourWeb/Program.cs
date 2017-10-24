using PVModele;
using PVModele.Tables;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Management;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ResultatPourWeb
{
    class Program
    {
        private static bool fait = false;
        private static ManagementEventWatcher watcher = null;
        private static ManagementEventWatcher watcherOut = null;
        private static string nomPat = string.Empty;

        static void Main(string[] args)
        {
            //nomPat = ConfigurationManager.AppSettings["RepSource"];
            //Console.WriteLine(string.Format("Source du fichier .pat = {0}", nomPat));
            //if (!System.IO.File.Exists(nomPat))
            //{
            //    Console.WriteLine("Attention, le fichier n'existe pas, chosissez en un autre");
            //}

            Console.WriteLine();
            Console.WriteLine("Menu : Q = Quitter");
            Console.WriteLine();
            Console.WriteLine("Insérer la clé USB et le fichier y sera lu afin de publier les résultat sur Internet");
            Console.WriteLine("Retirer la clé USB une fois les résultats publiés");

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

            //using (var db = new DBPatinVitesse())
            //{
            //    PatineurCompe.DB = db;
            //    PublicationResultat pr = new PublicationResultat();
            //    pr.InfoCompeVagues(db);
            //}
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
            Console.WriteLine("Insérer la clé USB et le fichier y sera lu afin de publier les résultat sur Internet");
            Console.WriteLine("Menu : Q = Quitter");
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
                    string[] resultat = Directory.GetFiles(x.Name, "*.pat");
                    if (resultat.Length == 0)
                    {
                        Console.WriteLine("Aucun fichier PAT sur la clé USB");
                        SystemSounds.Beep.Play();
                        SystemSounds.Beep.Play();
                        fait = true;
                        watcherOut.EventArrived += watcher_EventArrived2;
                        return;
                    }

                    if (resultat.Length > 1)
                    {
                        Console.WriteLine("La clé USB comporte plus d'un fichier, le premier fichier sera utilisé!");
                    }
                    string fichPat = resultat[0];
                    Console.WriteLine(string.Format("Le fichier {0} sera publié!", fichPat));
                    FileInfo fi = new FileInfo(fichPat);
                    string nomCopy = Path.Combine(ConfigurationManager.AppSettings["pathTravail"], "Publication.pat");
                    Console.WriteLine(string.Format("Avant la copie {0},{1}",fichPat,nomCopy));
                    File.Copy(fichPat, nomCopy, true);
                    Console.WriteLine("Apres la copie");
                    using (var db = new DBPatinVitesse())
                    {
                        PatineurCompe.DB = db;
                        PublicationResultat pr = new PublicationResultat();
                        pr.InfoCompeVagues(db);
                        Console.WriteLine("Mise à jour du site WEB");
                        pr.CopierSiteFTP();
                        Console.WriteLine("Fin de la mise à jour du site WEB");
                    }
                    SystemSounds.Beep.Play();
                    SystemSounds.Beep.Play();
                    Console.WriteLine("Vous pouvez maintenant retirer la clé USB");
                    fait = true;
                    watcherOut.EventArrived += watcher_EventArrived2;
                    return;
                }

                System.Threading.Thread.Sleep(1000);
            }

        }

    }
}

////WebRequest request = WebRequest.Create("http://pastebin.com/api/api_login.php");
//WebRequest request = WebRequest.Create("http://pastebin.com/api/api_post.php");
//request.Method = "POST";
//            StringBuilder postData = new StringBuilder();
//postData.Append("api_dev_key=c4ff71aec004c3020646ab643d24b63f");
//            postData.Append("&api_user_key=eb5d89bdf2a9d790985c0f2e8e6e6cb0");
//            // eb5d89bdf2a9d790985c0f2e8e6e6cb0
//            //postData.Append("&api_user_name=cpvq");
//            //postData.Append("&api_user_password=cpvqQuebec");
//            //            1.api_dev_key - this is your API Developer Key, in your case: c4ff71aec004c3020646ab643d24b63f
//            //2.api_user_name - this is the username of the user you want to login.
//            //3.api_user_password - this is the password of the user you want to login.


//            postData.Append("&api_option=paste");
//            postData.Append("&api_paste_code=var b='BrunoTest12'");
//            //postData.Append("&api_user_name=cpvq;");
//            //postData.Append("&api_user_password=cpvqQuebec;");
//            postData.Append("&api_paste_name=Test CPVQ");
//            postData.Append("&api_paste_format=Javascript");
//            postData.Append("&api_paste_private=2");
//            postData.Append("&api_paste_expire_date=N");

//            byte[] byteArray = Encoding.UTF8.GetBytes(postData.ToString());
//// Set the ContentType property of the WebRequest.  
//request.ContentType = "application/x-www-form-urlencoded";
//            // Set the ContentLength property of the WebRequest.  
//            request.ContentLength = byteArray.Length;
//            // Get the request stream.  
//            Stream dataStream = request.GetRequestStream();
//// Write the data to the request stream.  
//dataStream.Write(byteArray, 0, byteArray.Length);
//            // Close the Stream object.  
//            dataStream.Close();
//            // Get the response.  
//            WebResponse response = request.GetResponse();
//// Display the status.  
//Console.WriteLine(((HttpWebResponse)response).StatusDescription);
//            // Get the stream containing content returned by the server.  
//            dataStream = response.GetResponseStream();
//            // Open the stream using a StreamReader for easy access.  
//            StreamReader reader = new StreamReader(dataStream);
//// Read the content.  
//string responseFromServer = reader.ReadToEnd();
//// Display the content.  
//Console.WriteLine(responseFromServer);
//            // Clean up the streams.  
//            reader.Close();
//            dataStream.Close();
//            response.Close();



//            Pastebin.Pastebin pb = new Pastebin.Pastebin("c4ff71aec004c3020646ab643d24b63f");
//Pastebin.User usager = pb.LogIn("cpvq", "cpvqQuebec");
//var collection = pb.TrendingPastes;
      
//            foreach (Pastebin.Paste paste in collection)
//            {
//                Console.Write(paste.Id);
//                Console.WriteLine(paste.Title);

//            }

//            string resultat = pb.CreatePaste("Test", "Javascript", "var a = 'Bruno';", Pastebin.PasteExposure.Private, Pastebin.PasteExpiration.Never);

//Console.WriteLine(resultat);

//            return;
