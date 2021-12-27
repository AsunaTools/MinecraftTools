using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;

namespace MinecraftTools {
    class App {
        private static void Main() {
            Console.Clear();
            Console.WriteLine("==========================================================================");
            Thread.Sleep(100);
            Console.WriteLine("       d8888                                     888b     d888  .d8888b.  ");
            Thread.Sleep(100);
            Console.WriteLine("      d88888                                     8888b   d8888 d88P  Y88b ");
            Thread.Sleep(100);
            Console.WriteLine("     d88P888                                     88888b.d88888 888    888 ");
            Thread.Sleep(100);
            Console.WriteLine("    d88P 888 .d8888b  888  888 88888b.   8888b.  888Y88888P888 888        ");
            Thread.Sleep(100);
            Console.WriteLine("   d88P  888 88K      888  888 888 " + '\u0022' + "88b     " + '\u0022' +  "88b 888 Y888P 888 888        ");
            Thread.Sleep(100);
            Console.WriteLine("  d88P   888 " + '\u0022' + "Y8888b. 888  888 888  888 .d888888 888  Y8P  888 888    888 ");
            Thread.Sleep(100);
            Console.WriteLine(" d8888888888      X88 Y88b 888 888  888 888  888 888   " + '\u0022' + "   888 Y88b  d88P ");
            Thread.Sleep(100);
            Console.WriteLine("d88P     888  88888P   " + '\u0022' + "Y88888 888  888 " + '\u0022' + "Y888888 888       888  " + '\u0022' + "Y8888P" + '\u0022' + "  ");
            Thread.Sleep(100);
            Console.WriteLine("==========================================================================");
            Thread.Sleep(1000);

            if (Directory.Exists(MinecraftDir())) {
                var showMenu = true;
                while (showMenu) { showMenu = Menu(); }
                Console.Clear();
                Console.WriteLine("===================================");
                Console.WriteLine("          Minecraft Tools          ");
                Console.WriteLine("===================================");
                Console.WriteLine();
                Console.WriteLine("¡Hecho! Gracias por usar AsunaTools una vez mas,");
                Console.WriteLine("Escrito por Asuna y Ekardo. (Reescrito en C# por Asuna)");
                Console.WriteLine();
                Console.WriteLine("Presione cualquier tecla para salir.");
                Console.ReadLine();
            } else {
                Console.Clear();
                Console.WriteLine("===================================");
                Console.WriteLine("          Minecraft Tools          ");
                Console.WriteLine("===================================");
                Console.WriteLine();
                Console.WriteLine("¡Minecraft no esta instalado!");
                Console.WriteLine();
                Console.WriteLine("Presione cualquier tecla para salir.");
                Console.ReadLine();
            }
        }

        public static string MinecraftDir() {
            var userName = Environment.UserName;
            var minecraftDir = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                minecraftDir = "C:/Users/" + userName + "/AppData/Roaming/.minecraft";
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                minecraftDir = "/home/" + userName + "/.minecraft";
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                minecraftDir = "/Users/" + userName + "/Library/Application Support/minecraft";
            }
            return minecraftDir;
        }
        
        private static bool Menu() {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("          Minecraft Tools          ");
            Console.WriteLine("===================================");
            Console.WriteLine();
            Console.WriteLine("(1) Descargar Mods");
            Console.WriteLine("(2) Instalar Forge");
            Console.WriteLine("(3) Instalar Optifine");
            Console.WriteLine();
            Console.WriteLine("(4) Salir");
            Console.Write("\r\nSelecciona una opcion: ");
            switch (Console.ReadLine()) {
                case "1":
                    Console.Clear();
                    InstallMods();
                    return true;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Work In Progress");
                    return true;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Work In Progress");
                    return true;
                default:
                    return false;
            }
        }
        private static void InstallMods() {
            Console.WriteLine("===================================");
            Console.WriteLine("          Minecraft Tools          ");
            Console.WriteLine("===================================");
            Console.WriteLine();
            var modsDir = MinecraftDir() + "/mods";
            if (!Directory.Exists(modsDir)) {
                Directory.CreateDirectory(modsDir);
            }
            Console.Write("Clear mods folder: ");
            DirectoryInfo di = new DirectoryInfo(modsDir);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete(); 
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true); 
            }
            Console.WriteLine("Done");
            Console.Write("Download mods.zip: ");
            WebClient Client = new WebClient ();
            Client.DownloadFile(new Uri("https://static.asuna.tools/downloads/asunamc/mods.zip"), @modsDir + "/mods.zip");
            Console.WriteLine("Done");
            Console.Write("Extract mods: ");
            System.IO.Compression.ZipFile.ExtractToDirectory(@modsDir + "/mods.zip", @modsDir);
            Console.WriteLine("Done");
            Console.Write("Delete Temp: ");
            if(File.Exists(@modsDir + "/mods.zip"))
            {
                File.Delete(@modsDir + "/mods.zip");
            }
            Console.WriteLine("Done");
            Thread.Sleep(300);
        }
    }
}
